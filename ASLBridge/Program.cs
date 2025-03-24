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

namespace ASLBridge
{
    public class Program
    {
        public static ASLSplitter Splitter { get; private set; } = ASLSplitter.GetInstance();
        private static bool run = true;

        public static async Task Main(string[] args)
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
            if (command == "get xml")
            {
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
            }

            switch (command.ToLower())
            {
                case "status":
                    return Splitter.GetStatusGame() ? "Attached" : "Not attached";
                case "igt":
                    return $"IGT: {Splitter.GetIngameTime()} ms";
                case "exit":
                    run = false;
                    return "Finished Process";
                default:
                    return "Unknown command";
            }
        }
    }
}
