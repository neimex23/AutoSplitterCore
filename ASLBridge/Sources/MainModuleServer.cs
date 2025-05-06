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
using System.Threading;
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



            DebugLog.LogMessage("NamedPipe server started: pipe name = ASLBridge");

        }

        private static StreamWriter _writer;
        private static async Task StartNamedPipeServer()
        {
            while (serverRunning)
            {
                using (var pipeServer = new NamedPipeServerStream("ASLBridge", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous))
                {
                    DebugLog.LogMessage("[PIPE] Waiting Connection");
                    await pipeServer.WaitForConnectionAsync();

                    DebugLog.LogMessage("[PIPE] Client Connected");

                    using (var reader = new StreamReader(pipeServer))
                    using (_writer = new StreamWriter(pipeServer) { AutoFlush = true })
                    {
                        while (serverRunning && pipeServer.IsConnected)
                        {
                            try
                            {
                                var line = await reader.ReadLineAsync();
                                if (line == null)
                                {
                                    DebugLog.LogMessage("[PIPE] Client disconnected.");
                                    break;
                                }

                                DebugLog.LogMessage($"[PIPE] Command received: {line}");
                                var response = HandleCommand(line);
                                if (response != null)
                                {
                                    await SendResponseAsync(response);
                                }
                            }
                            catch (IOException ex)
                            {
                                DebugLog.LogMessage($"[PIPE] IOException: {ex.Message}");
                                break;
                            }
                            catch (Exception ex)
                            {
                                DebugLog.LogMessage($"[PIPE] General Exception: {ex.Message}");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static async Task SendResponseAsync(string message)
        {
            if (_writer != null)
            {
                await _writeLock.WaitAsync();
                try
                {
                    await _writer.WriteLineAsync(message);
                    DebugLog.LogMessage($"[PIPE] Message sent: {message}");
                }
                catch (IOException ex)
                {
                    DebugLog.LogMessage($"[PIPE] Send Error: {ex.Message}");
                }
                finally
                {
                    _writeLock.Release();
                }
            }
        }

        private static StreamWriter _writerIgt;
        private static SemaphoreSlim _writeLock = new SemaphoreSlim(1, 1);
        private static async Task StartNamedPipeServerIGT()
        {
            while (serverRunning)
            {
                using (var pipeServer = new NamedPipeServerStream("ASLBridge_IGT", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous))
                {
                    DebugLog.LogMessage("[PIPEIGT] Waiting Connection");
                    await pipeServer.WaitForConnectionAsync();

                    DebugLog.LogMessage("[PIPEIGT] Client Connected");

                    using (var reader = new StreamReader(pipeServer))
                    using (_writerIgt = new StreamWriter(pipeServer) { AutoFlush = true })
                    {
                        while (serverRunning && pipeServer.IsConnected)
                        {
                            try
                            {
                                var line = await reader.ReadLineAsync();
                                if (line == null)
                                {
                                    DebugLog.LogMessage("[PIPEIGT] Client disconnected.");
                                    break;
                                }

                                DebugLog.LogMessage($"[PIPEIGT] Command received: {line}");
                                var response = HandleCommand(line);
                                if (response != null)
                                {
                                    await SendResponseIgtAsync(response);
                                }
                            }
                            catch (IOException ex)
                            {
                                DebugLog.LogMessage($"[PIPEIGT] IOException: {ex.Message}");
                                break;
                            }
                            catch (Exception ex)
                            {
                                DebugLog.LogMessage($"[PIPEIGT] General Exception: {ex.Message}");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static SemaphoreSlim _writeLockIgt = new SemaphoreSlim(1, 1);

        private static async Task SendResponseIgtAsync(string message)
        {
            if (_writerIgt != null)
            {
                await _writeLockIgt.WaitAsync();
                try
                {
                    await _writerIgt.WriteLineAsync(message);
                    DebugLog.LogMessage($"[PIPEIGT] Message sent: {message}");
                }
                catch (IOException ex)
                {
                    DebugLog.LogMessage($"[PIPEIGT] Send Error: {ex.Message}");
                }
                finally
                {
                    _writeLockIgt.Release();
                }
            }
        }


        public static async Task BroadcastEvent(string eventMessage)
        {
            DebugLog.LogMessage($"[PIPE-EVENT] {eventMessage}");
            if (_writer != null)
            {
                await _writeLock.WaitAsync();
                try
                {
                    await _writer.WriteLineAsync(eventMessage);
                }
                catch (IOException ex)
                {
                    DebugLog.LogMessage($"[PIPE] Error to send Event: {ex.Message}");
                }
                finally
                {
                    _writeLock.Release();
                }
            }
        }



        private static string HandleCommand(string command)
        {
            switch (command.ToLower())
            {
                case "ping":
                    return "pong";
                case "alive":
                    return string.Empty;
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
                    _writer?.Dispose();
                    DebugLog.Close();
                    ASLFormServer.GetIntance().CloseForm();
                    Application.Exit();
                    return null;
                default:
                    return "Unknown command";
            }
        }

        private static void ShowForm(object sender, EventArgs e) => ASLFormServer.GetIntance().ShowForm();

        public static void InternalExitCommand()
        {
            SaveModule.SaveASLSettings();
            serverRunning = false;
            _writer?.Dispose();
            DebugLog.Close();
            Application.Exit();
        }

    }
}
