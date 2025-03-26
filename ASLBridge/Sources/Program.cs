//MIT License

//Copyright (c) 2022-2025 Ezequiel Medina

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;
using System.Net.Sockets;

namespace ASLBridge
{
    public class Program
    {
        public static ASLSplitterServer Splitter { get; private set; } = ASLSplitterServer.GetInstance();
        private static SaveModuleServer SaveModule = new SaveModuleServer();
        private static ReaLTaiizor.Forms.MaterialForm form = new ASLFormServer();
        private static bool process = true;


        public static void OpenWithBrowser(Uri uri) => Process.Start(new ProcessStartInfo("cmd", $"/c start {uri.OriginalString.Replace("&", "^&")}") { CreateNoWindow = true, UseShellExecute = true });

        public static async Task Main(string[] args)
        {
            Splitter.ASCOnSplitHandler += async (s, e) => await BroadcastEvent("event:split");
            Splitter.ASCOnStartHandler += async (s, e) => await BroadcastEvent("event:start");
            Splitter.ASCOnResetHandler += async (s, e) => await BroadcastEvent("event:reset");
            SaveModule.LoadASLSettings();
            await Task.Run(async() => { await startServer(); });
        }

        private static async Task startServer()
        {
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:5000/ws/");
            httpListener.Start();
            Console.WriteLine("WebSocket server started at ws://localhost:5000/ws/");

            while (process)
            {
                HttpListenerContext context = await httpListener.GetContextAsync();

                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext wsContext = await context.AcceptWebSocketAsync(null);
                    _ = HandleConnection(wsContext.WebSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }

            foreach (var client in connectedClients)
            {
                try { await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server shutting down", CancellationToken.None); }
                catch { }
            }
            connectedClients.Clear();
            SaveModule.SaveASLSettings();
        }

        private static async Task HandleConnection(WebSocket socket)
        {
            byte[] buffer = new byte[1024];
            connectedClients.Add(socket);
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
                    break;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                if (message.StartsWith("id:"))
                {
                    var separatorIndex = message.IndexOf('|');
                    if (separatorIndex > 3)
                    {
                        string id = message.Substring(3, separatorIndex - 3);
                        string command = message.Substring(separatorIndex + 1);

                        string response = HandleCommand(command);
                        string taggedResponse = $"id:{id}|{response}";

                        var responseBytes = Encoding.UTF8.GetBytes(taggedResponse);
                        await socket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
        }

        private static List<WebSocket> connectedClients = new List<WebSocket>();

        private static async Task BroadcastEvent(string eventMessage)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(eventMessage);
            var segment = new ArraySegment<byte>(messageBytes);

            List<WebSocket> closedSockets = new List<WebSocket>();

            foreach (var client in connectedClients)
            {
                if (client.State == WebSocketState.Open)
                {
                    try
                    {
                        await client.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    catch
                    {
                        closedSockets.Add(client);
                    }
                }
                else
                {
                    closedSockets.Add(client);
                }
            }

            // Limpiar clientes desconectados
            foreach (var socket in closedSockets)
                connectedClients.Remove(socket);
        }

        private static string HandleCommand(string command)
        {
            switch (command.ToLower())
            {
                case "status":
                    return Splitter.GetStatusGame() ? "Attached" : "Not attached";
                case "igt":
                    return $"{Splitter.GetIngameTime()}";
                case "openform":
                    form.ShowDialog();
                    return "Opened Form";
                case "exit":
                    process = false;
                    return "Finished Process";
                default:
                    return "Unknown command";
            }
        }
    }
}
