using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public class WebSockets
    {
        public static SaveModule saveModule { get; set; }
        private ISplitterControl control = SplitterControl.GetControl();

        private HttpListener _listener;
        private readonly ConcurrentBag<WebSocket> _clients = new ConcurrentBag<WebSocket>();
        private CancellationTokenSource _cts;
        private Task _listeningTask;

        public bool HasConnections => !_clients.IsEmpty;
        public bool IsRunning { get; private set; } = false;

        #region Singleton
        private WebSockets()
        {
            CreateListener(saveModule.generalAS.WebSocketSettings.Url);
        }

        ~WebSockets()
        {
            Stop();
        }

        private static WebSockets _instance;

        public static WebSockets Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WebSockets();
                }
                return _instance;
            }
        }
        #endregion

        private void CreateListener(string url)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(url);
        }

        public async void ReConnect()
        {
            Stop();
            await StartAsync();
        }

        public async Task StartAsync()
        {
            if (IsRunning || !saveModule.generalAS.EnableWebSocket) return;

            if (_listener == null)
                CreateListener(saveModule.generalAS.WebSocketSettings.Url);


            try
            {
                _listener.Start();
                _cts = new CancellationTokenSource();
                IsRunning = true;
                DebugLog.LogMessage($"WebSocket server started on {saveModule.generalAS.WebSocketSettings.Url}");

                _listeningTask = Task.Run(async () =>
                {
                    while (!_cts.Token.IsCancellationRequested)
                    {
                        HttpListenerContext context;
                        try
                        {
                            context = await _listener.GetContextAsync();
                        }
                        catch (HttpListenerException) { break; }

                        if (context.Request.IsWebSocketRequest)
                        {
                            var wsContext = await context.AcceptWebSocketAsync(null);
                            var socket = wsContext.WebSocket;
                            _clients.Add(socket);
                            DebugLog.LogMessage("New WebSocket client connected.");
                            _ = HandleClient(socket);
                        }
                        else
                        {
                            context.Response.StatusCode = 400;
                            context.Response.Close();
                        }
                    }
                }, _cts.Token);
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"WebSocket failed to start: {ex.Message}");
            }
        }

        private async Task HandleClient(WebSocket socket)
        {
            var buffer = new byte[1024];
            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
                        break;
                    }

                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    DebugLog.LogMessage($"[WS Received] {message}");


                    if (message.Contains(saveModule.generalAS.WebSocketSettings.Split.Message))
                    {
                        control?.SplitCheck("[WS] split command received");
                    }else
                    if (message.Contains(saveModule.generalAS.WebSocketSettings.Hit.Message))
                    {
                        control?.HitCheck("[WS] hit command received");
                    }
                    else if (message.Contains(saveModule.generalAS.WebSocketSettings.Start.Message))
                    {
                        control?.StartStopTimer(true);
                    }
                    else if (message.Contains(saveModule.generalAS.WebSocketSettings.Reset.Message))
                    {
                        control?.ProfileReset();
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"WebSocket client error: {ex.Message}");
            }
        }

        public async Task BroadcastAsync(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            foreach (var socket in _clients)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

        public async Task SendToClient(WebSocket socket, string message)
        {
            if (socket.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public void Stop()
        {
            try
            {
                if (_listener != null && _listener.IsListening)
                {
                    _cts?.Cancel();
                    _listener.Stop();
                    _listener.Close();
                    DebugLog.LogMessage("WebSocket listener stopped.");
                }

                foreach (var socket in _clients)
                {
                    try
                    {
                        if (socket.State == WebSocketState.Open)
                            socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server stopped", CancellationToken.None).Wait();
                        else
                            socket?.Abort();
                    }
                    catch (Exception ex)
                    {
                        DebugLog.LogMessage($"Error closing socket: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"Error stopping WebSocket: {ex.Message}");
            }
            finally
            {
                _listener = null;
                _cts = null;
                IsRunning = false;
            }
        }
    }
}
