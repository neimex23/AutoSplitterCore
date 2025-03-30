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
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASLBridge
{
    public class MainModuleServer
    {
        public static ASLSplitterServer Splitter { get; private set; } = ASLSplitterServer.GetInstance();
        private static SaveModuleServer SaveModule = SaveModuleServer.GetIntance();
        private static event EventHandler OpenForm;

        public static void OpenWithBrowser(Uri uri) => Process.Start(new ProcessStartInfo("cmd", $"/c start {uri.OriginalString.Replace("&", "^&")}") { CreateNoWindow = true, UseShellExecute = true });



        private static bool serverRunning = true;
        public static void LoadProcess()
        {
            Splitter.ASCOnSplitHandler += (s, e) => BroadcastEvent("event:split");
            Splitter.ASCOnStartHandler += (s, e) => BroadcastEvent("event:start");
            Splitter.ASCOnResetHandler += (s, e) => BroadcastEvent("event:reset");

            OpenForm += new EventHandler(ShowForm);

            Task.Run(() => StartNamedPipeServer());
            Task.Run(() => StartNamedPipeServerIGT());



            Console.WriteLine("NamedPipe server started: pipe name = ASLBridge");

        }

        private static StreamWriter _writer;
        private static void StartNamedPipeServer()
        {
            while (serverRunning)
            {
                using (var pipeServer = new NamedPipeServerStream("ASLBridge", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous))
                {
                    try
                    {
                        Console.WriteLine("[PIPE] Esperando conexión...");
                        pipeServer.WaitForConnection();

                        Console.WriteLine("[PIPE] Cliente conectado");

                        using (var reader = new StreamReader(pipeServer))
                        using (_writer = new StreamWriter(pipeServer) { AutoFlush = true })
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                Console.WriteLine($"[PIPE] Comando recibido: {line}");
                                string response = HandleCommand(line);

                                if (response != null)
                                {
                                    _writer.WriteLine(response);
                                    Console.WriteLine($"[PIPE] Comando Enviado: {response}");
                                }


                                if (line.Trim().ToLower() == "exit")
                                {
                                    serverRunning = false;
                                    break;
                                }
                            }

                            if (line == null)
                            {
                                Console.WriteLine("[PIPE] Cliente se desconectó. Cerrando servidor...");
                                HandleCommand("exit");
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"[PIPE] Desconexión inesperada del cliente: {ex.Message}");
                        HandleCommand("exit");
                    }
                }
            }
        }

        private static StreamWriter _writerIgt;
        private static void StartNamedPipeServerIGT()
        {
            while (serverRunning)
            {
                using (var pipeServer = new NamedPipeServerStream("ASLBridge_IGT", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous))
                {
                    try
                    {
                        pipeServer.WaitForConnection();

                        using (var reader = new StreamReader(pipeServer))
                        using (_writerIgt = new StreamWriter(pipeServer) { AutoFlush = true })
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string response = HandleCommand(line);

                                if (response != null)
                                {
                                    _writerIgt.WriteLine(response);
                                }
                            }

                            if (line == null)
                            {
                                Console.WriteLine("[PIPE] Cliente se desconectó. Cerrando servidor...");
                                HandleCommand("exit");
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"[PIPE] Desconexión inesperada del cliente: {ex.Message}");
                        HandleCommand("exit");
                    }
                }
            }
        }

        public static void BroadcastEvent(string eventMessage)
        {
            Console.WriteLine($"[PIPE-EVENT] {eventMessage}");
            if (_writer != null)
            {
                try
                {
                    _writer.WriteLine(eventMessage);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"[PIPE] Error al enviar evento: {ex.Message}");
                }
            }
        }


        private static string HandleCommand(string command)
        {
            switch (command.ToLower())
            {
                case "enableigt":
                    ASLFormServer.GetIntance().SetIgt(true);
                    return "Igt Enabled";
                case "status":
                    return Splitter.GetStatusGame() ? "Attached" : "Not attached";
                case "igt":
                    return $"{Splitter.GetIngameTime()}";
                case "openform":
                    OpenForm?.Invoke(null, EventArgs.Empty);
                    return "Opened Form";
                case "exit":
                    SaveModule.SaveASLSettings();
                    serverRunning = false;
                    _writer.Dispose();
                    ASLFormServer.GetIntance().CloseForm();
                    Application.Exit();
                    return null;
                default:
                    return "Unknown command";
            }
        }

        private static void ShowForm(object sender, EventArgs e) => ASLFormServer.GetIntance().ShowForm();


    }
}
