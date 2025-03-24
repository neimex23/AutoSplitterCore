using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public class WebSocketClient
    {
        private readonly ClientWebSocket _socket = new ClientWebSocket();

        public async Task ConnectAsync()
        {
            await _socket.ConnectAsync(new Uri("ws://localhost:5000/ws/"), CancellationToken.None);
            Console.WriteLine("Conectado a WebSocket");

            _ = ReceiveLoop();
        }

        public async Task SendCommand(string command)
        {
            var buffer = Encoding.UTF8.GetBytes(command);
            await _socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
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
                    if (message.StartsWith("event:start"))
                        OnStart?.Invoke(this, EventArgs.Empty);
                    if (message.StartsWith("event:reset"))
                        OnReset?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Eventos públicos para que el cliente los use
        public event EventHandler OnSplit;
        public event EventHandler OnStart;
        public event EventHandler OnReset;
    }
}
