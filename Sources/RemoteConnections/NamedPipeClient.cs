using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public class NamedPipeClient
    {
        private NamedPipeClientStream _pipe;
        private StreamWriter _writer;
        private StreamReader _reader;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public event EventHandler OnSplit;
        public event EventHandler OnStart;
        public event EventHandler OnReset;

        public bool PipeOnline => _pipe.IsConnected;

        public async Task ConnectAsync()
        {
            while (true)
            {
                try
                {
                    _pipe = new NamedPipeClientStream(".", "ASLBridge", PipeDirection.InOut, PipeOptions.Asynchronous);
                    await _pipe.ConnectAsync(2000);
                    _writer = new StreamWriter(_pipe) { AutoFlush = true };
                    _reader = new StreamReader(_pipe);

                    _ = ListenAsync(_cts.Token);
                    DebugLog.LogMessage("Connected Named Pipe ASLBridge");
                    break;
                }
                catch (Exception ex)
                {
                    DebugLog.LogMessage($"Error Connecting Named Pipe: {ex.Message}. Retring...");
                    await Task.Delay(2000);
                }
            }
        }

        public async Task<string> SendCommand(string command, bool waitForResponse = false)
        {
            if (_pipe == null || !_pipe.IsConnected) return null;

            await _writer.WriteLineAsync(command);

            if (waitForResponse)
            {
                var response = await _reader.ReadLineAsync();
                return response;
            }

            return null;
        }

        private async Task ListenAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested && _pipe.IsConnected)
                {
                    string message = await _reader.ReadLineAsync();
                    if (message == null) break;

                    DebugLog.LogMessage("[PIPE Message] " + message);

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
                    {
                        ASLSplitter.GetInstance().setedBridge = true;
                        ASLSplitter.GetInstance().IGTEnable = false;
                        ASLSplitter.GetInstance().setedBridge = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"Error Listen Named Pipe: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            _cts.Cancel();
            _pipe?.Dispose();
        }
    }
}
