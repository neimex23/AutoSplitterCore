using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public class WebSocketClient
    {
        private ClientWebSocket _socket = new ClientWebSocket();
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
        private TaskCompletionSource<string> _responseTcs;

        public async Task ConnectAsync(int retryDelayMs = 2000)
        {
            while (_socket.State != WebSocketState.Open)
            {
                try
                {
                    await _socket.ConnectAsync(new Uri("ws://localhost:9000/ws/"), CancellationToken.None);
                    DebugLog.LogMessage("Connected WebSocket");
                    //await SendCommand("id:test123|status");
                    _ = ReceiveLoop(); 
                    break;
                }
                catch (Exception ex)
                {
                    DebugLog.LogMessage($"Connection Socket Error: {ex.Message}. Retring en {retryDelayMs}ms...");
                    await Task.Delay(retryDelayMs);
                }
            }
        }

        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingResponses = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
        public async Task<string> SendCommand(string command, bool waitForResponse = false)
        {
            string id = Guid.NewGuid().ToString(); // Generar ID único
            string taggedCommand = $"id:{id}|{command}";

            if (waitForResponse)
            {
                var tcs = new TaskCompletionSource<string>();
                _pendingResponses[id] = tcs;

                await SendRaw(taggedCommand);
                return await tcs.Task;
            }
            else
            {
                await SendRaw(taggedCommand);
                return null;
            }
        }

        // Método interno que hace el SendAsync puro
        private async Task SendRaw(string message)
        {
            await _sendLock.WaitAsync();
            try
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await _socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            finally
            {
                _sendLock.Release();
            }
        }    


        private async Task ReceiveLoop()
        {
            var buffer = new byte[2048];
            while (_socket.State == WebSocketState.Open)
            {
                var result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"message server: {message}");

                    if (message.StartsWith("event:split"))
                        OnSplit?.Invoke(this, EventArgs.Empty);
                    else if (message.StartsWith("event:start"))
                        OnStart?.Invoke(this, EventArgs.Empty);
                    else if (message.StartsWith("event:reset"))
                        OnReset?.Invoke(this, EventArgs.Empty);
                    else if (message.StartsWith("event:enableigt"))
                    {
                        ASLSplitter.GetInstance().setedBridge = true;
                        ASLSplitter.GetInstance().IGTEnable = true;
                        ASLSplitter.GetInstance().setedBridge = false;
                    }
                    else if (message.StartsWith("event:disableigt"))
                        ASLSplitter.GetInstance().IGTEnable = false;
                    else if (message.StartsWith("id:"))
                    {
                        var separatorIndex = message.IndexOf('|');
                        if (separatorIndex > 3)
                        {
                            string id = message.Substring(3, separatorIndex - 3);
                            string response = message.Substring(separatorIndex + 1);
                            if (_pendingResponses.TryRemove(id, out var tcs))
                                tcs.TrySetResult(response);
                        }
                    }
                }
            }
        }

        public event EventHandler OnSplit;
        public event EventHandler OnStart;
        public event EventHandler OnReset;
    }
}
