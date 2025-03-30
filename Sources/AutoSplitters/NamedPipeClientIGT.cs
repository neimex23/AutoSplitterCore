using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore.Sources.AutoSplitters
{
    public class NamedPipeClientIGT
    {
        private NamedPipeClientStream _pipe;
        private StreamWriter _writer;
        private StreamReader _reader;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private Task _igtPollingTask;
        private long _lastIgt = -1;

        public long LastIGT => _lastIgt;
        public bool PipeOnline => _pipe.IsConnected;

        public async Task ConnectAsync()
        {
            _pipe = new NamedPipeClientStream(".", "ASLBridge_IGT", PipeDirection.InOut, PipeOptions.Asynchronous);
            await _pipe.ConnectAsync(2000);
            _writer = new StreamWriter(_pipe) { AutoFlush = true };
            _reader = new StreamReader(_pipe);

            StartIGTPolling();
            DebugLog.LogMessage("IGT Pipe Connected");
        }

        private void StartIGTPolling()
        {
            _igtPollingTask = Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        await _writer.WriteLineAsync("igt");
                        var response = await _reader.ReadLineAsync();

                        if (long.TryParse(response, out var parsed))
                            _lastIgt = parsed;
                    }
                    catch { _lastIgt = -1; }

                    await Task.Delay(1000); 
                }
            });
        }

        public void Disconnect()
        {
            _cts.Cancel();
            _pipe?.Dispose();
        }
    }

}
