using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ASLBridge
{
    public class Program
    {
        private static ASLSplitter splitter = ASLSplitter.GetInstance();
        private static NamedPipeServerStream pipeServer;
        private static Thread pipeThread;

        public static void InitializePipe()
        {
            pipeServer = new NamedPipeServerStream("ASLServerPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            pipeThread = new Thread(PipeListener);
            pipeThread.Start();
        }

        private static void PipeListener()
        {
            while (true)
            {
                pipeServer.WaitForConnection();
                Console.WriteLine("Cliente connected.");

                while (pipeServer.IsConnected)
                {
                    try
                    {
                        // Lee el mensaje del cliente
                        byte[] buffer = new byte[256];
                        int bytesRead = pipeServer.Read(buffer, 0, buffer.Length);
                        string clientMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        Console.WriteLine($"Command Recived by Client: {clientMessage}");

                        // Procesar el mensaje recibido
                        ProcessMessage(clientMessage);

                        // Enviar una respuesta al cliente
                        string response = "Command Recived Successfully.";
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        pipeServer.Write(responseBytes, 0, responseBytes.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error on comunication: {ex.Message}");
                        break;
                    }
                }
            }
        }

        private static void ProcessMessage(string message)
        {
            // Aquí procesamos los comandos
            if (message == "GetStatusGame")
            {
                bool status = splitter.GetStatusGame();
                SendMessageToClient($"Status: {status}");
            }
            else if (message == "GetIngameTime")
            {
                long time = splitter.GetIngameTime();
                SendMessageToClient($"IngameTime: {time}");
            }
            else if (message == "PracticeActive")
            {
                splitter.PracticeMode = true;
            }
            else if (message == "PracticeDisable")
            {
                splitter.PracticeMode = false;
            }else
            if (message.StartsWith("setData"))
            {
                // Recibe el XML desde el cliente
                string xmlData = message.Substring(8); // Eliminar el "setData " del principio
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlData);
                splitter.setData(xmlDoc);
                SendMessageToClient("Data received and processed.");
            }
            else if (message == "getData")
            {
                //send xmlnode
            }
            else
            {
                SendMessageToClient("Unknow Command.");
            }
        }

        private static void SendMessageToClient(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            pipeServer.Write(buffer, 0, buffer.Length);
        }

        public static void Main(string[] args)
        {
            InitializePipe();

        }
    }
}
