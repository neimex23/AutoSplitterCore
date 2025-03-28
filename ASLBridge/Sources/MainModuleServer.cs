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
using Fleck;
using System.Windows.Forms;
using ReaLTaiizor.Forms;
using System.Runtime.CompilerServices;

namespace ASLBridge
{
    public class MainModuleServer
    {
        public static ASLSplitterServer Splitter { get; private set; } = ASLSplitterServer.GetInstance();
        private static SaveModuleServer SaveModule = new SaveModuleServer();
        private static event EventHandler OpenForm;
        private static WebSocketServer server;

        public static void OpenWithBrowser(Uri uri) => Process.Start(new ProcessStartInfo("cmd", $"/c start {uri.OriginalString.Replace("&", "^&")}") { CreateNoWindow = true, UseShellExecute = true });


        private static List<IWebSocketConnection> connectedClients = new List<IWebSocketConnection>();

        public static void LoadProcess()
        {
            Splitter.ASCOnSplitHandler += (s, e) => BroadcastEvent("event:split");
            Splitter.ASCOnStartHandler += (s, e) => BroadcastEvent("event:start");
            Splitter.ASCOnResetHandler += (s, e) => BroadcastEvent("event:reset");
            OpenForm += new EventHandler(ShowForm);

            SaveModule.LoadASLSettings();

            server = new WebSocketServer("ws://0.0.0.0:9000");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Client connected");
                    connectedClients.Add(socket);
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine("Client disconnected");
                    connectedClients.Remove(socket);
                };

                socket.OnMessage = message =>
                {
                    Console.WriteLine($"Message: {message}");
                    if (message.StartsWith("id:"))
                    {
                        var separatorIndex = message.IndexOf('|');
                        if (separatorIndex > 3)
                        {
                            string id = message.Substring(3, separatorIndex - 3);
                            string command = message.Substring(separatorIndex + 1);
                            string response = HandleCommand(command);
                            socket.Send($"id:{id}|{response}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Unknown Message");
                    }
                };
            });

            Console.WriteLine("Fleck WebSocket server started at ws://localhost:9000");
         
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;

                string result = HandleCommand(input.Trim());

                if (result != null)
                {
                    Console.WriteLine($"[Response] {result}");
                }

                if (input.Trim().ToLower() == "exit")
                    break;
            }
        }

        public static void BroadcastEvent(string eventMessage)
        {
            foreach (var client in connectedClients.ToArray())
            {
                if (client.IsAvailable)
                    client.Send(eventMessage);
            }
        }

        private static string HandleCommand(string command)
        {
            switch (command.ToLower())
            {
                case "enableIGT":
                    ASLFormServer.IGTActive = true;
                    return "Igt Enabled";
                case "status":
                    return Splitter.GetStatusGame() ? "Attached" : "Not attached";
                case "igt":
                    return $"{Splitter.GetIngameTime()}";
                case "openform":
                    OpenForm?.Invoke(null, EventArgs.Empty);
                    return "Opened Form";         
                case "exit":
                    foreach (var client in connectedClients.ToArray())
                    {
                        try { client.Close(); } catch { }
                    }
                    connectedClients.Clear();
                    server.Dispose();
                    SaveModule.SaveASLSettings();
                    Environment.Exit(0);
                    return null;
                default:
                    return "Unknown command";
            }
        }

        static Form aslForm = null;
        private static void ShowForm(object sender, EventArgs e)
        {
            //Encontrar una forma de abrir aslform en un proceso distinto
            // Aplication.run causa que al buscar un script este no haga nada
            // showdialog directamente funciona pero se tranca si es llamado por openform desde el cliente
            //SynchronizationContext no hace nada
        }


    }
}
