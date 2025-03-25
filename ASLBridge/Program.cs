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
        public static ASLSplitter Splitter { get; private set; } = ASLSplitter.GetInstance();
        private static ReaLTaiizor.Forms.MaterialForm form = new ASLForm();
        private static bool run = true;


        public static void OpenWithBrowser(Uri uri) => Process.Start(new ProcessStartInfo("cmd", $"/c start {uri.OriginalString.Replace("&", "^&")}") { CreateNoWindow = true, UseShellExecute = true });

        public static async Task Main(string[] args)
        {
            Splitter.ASCOnSplitHandler += async (s, e) => await BroadcastEvent("event:split");
            Splitter.ASCOnStartHandler += async (s, e) => await BroadcastEvent("event:start");
            Splitter.ASCOnResetHandler += async (s, e) => await BroadcastEvent("event:reset");
            await Task.Run(async() => { await startServer(); }); //Form puede ser abierto por lo que el hilo principal no deberia acabar pero tampoco interruptir si abre form para que siga mandando mensajes
        }

        private static async Task startServer()
        {
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:5000/ws/");
            httpListener.Start();
            Console.WriteLine("WebSocket server started at ws://localhost:5000/ws/");

            while (run)
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
                string response = HandleCommand(message.Trim());
                var bytes = Encoding.UTF8.GetBytes(response);
                await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
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
            if (command.StartsWith("set xml:"))
            {
                string xmlContent = command.Substring("set xml:".Length);
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlContent);
                    Splitter.setData(doc.DocumentElement);
                    return "XML settings loaded";
                }
                catch (Exception ex)
                {
                    return $"Error parsing XML: {ex.Message}";
                }
            }

            switch (command.ToLower())
            {
                case "get xml":
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        XmlNode node = Splitter.getData(doc);
                        doc.AppendChild(doc.ImportNode(node, true));
                        return doc.OuterXml;
                    }
                    catch (Exception ex)
                    {
                        return $"Error generating XML: {ex.Message}";
                    }
                case "status":
                    return Splitter.GetStatusGame() ? "Attached" : "Not attached";
                case "igt":
                    return $"{Splitter.GetIngameTime()}";
                case "openform":
                    form.ShowDialog();
                    return "Opened Form";
                case "exit":
                    run = false;
                    return "Finished Process";
                default:
                    return "Unknown command";
            }
        }
    }
}
