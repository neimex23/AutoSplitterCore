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
        private readonly ClientWebSocket _socket = new ClientWebSocket();
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
        private TaskCompletionSource<string> _responseTcs;

        public async Task ConnectAsync()
        {
            await _socket.ConnectAsync(new Uri("ws://localhost:5000/ws/"), CancellationToken.None);
            Console.WriteLine("Conectado a WebSocket");

            _ = ReceiveLoop();
        }

        public async Task SendCommand(string command)
        {
            await _sendLock.WaitAsync();
            try
            {
                var buffer = Encoding.UTF8.GetBytes(command);
                await _socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            finally
            {
                _sendLock.Release();
            }
        }

        // Nueva función: espera una respuesta textual del servidor
        public async Task<string> SendCommandWithResponse(string command)
        {
            _responseTcs = new TaskCompletionSource<string>();
            await SendCommand(command);
            return await _responseTcs.Task;
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
                    Console.WriteLine($"Mensaje del servidor: {message}");

                    if (message.StartsWith("event:split"))
                        OnSplit?.Invoke(this, EventArgs.Empty);
                    else if (message.StartsWith("event:start"))
                        OnStart?.Invoke(this, EventArgs.Empty);
                    else if (message.StartsWith("event:reset"))
                        OnReset?.Invoke(this, EventArgs.Empty);
                    else
                        _responseTcs?.TrySetResult(message); // Respuesta normal
                }
            }
        }

        public event EventHandler OnSplit;
        public event EventHandler OnStart;
        public event EventHandler OnReset;
    }
}
