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
using System.IO;
using System.Xml;

namespace ASLBridge
{
    public class SaveModuleServer
    {
        private SaveModuleServer() { }
        private static readonly SaveModuleServer Instance = new SaveModuleServer();

        public static SaveModuleServer GetIntance() => Instance;

        private ASLSplitterServer aslSplitter = ASLSplitterServer.GetInstance();

        public void SaveASLSettings() => SaveXmlData("SaveGeneralAutoSplitter.xml", "DataASL", aslSplitter.getData);
        public void LoadASLSettings() => LoadXmlData("SaveGeneralAutoSplitter.xml", "DataASL", aslSplitter.setData);



        void SaveXmlData(string filePath, string nodeName, Func<XmlDocument, XmlNode> getDataFunc)
        {
            if (getDataFunc != null)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();

                    if (File.Exists(filePath))
                    {
                        doc.Load(filePath);
                    }
                    else
                    {
                        XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        doc.AppendChild(xmlDeclaration);
                    }

                    if (doc.DocumentElement == null)
                    {
                        XmlElement root = doc.CreateElement("Root");
                        doc.AppendChild(root);
                    }

                    XmlNode newNode = doc.CreateElement(nodeName);
                    XmlNode dataNode = getDataFunc(doc);

                    if (dataNode != null)
                    {
                        newNode.AppendChild(dataNode);
                    }
                    else
                    {
                        DebugLog.LogMessage($"Warning: {nodeName}.getData(doc) is null.");
                    }
                    doc.DocumentElement.AppendChild(newNode);
                    doc.Save(filePath);
                }
                catch (Exception ex)
                {
                    DebugLog.LogMessage($"Error processing XML for {nodeName}: {ex.Message}");
                }
            }
        }

        void LoadXmlData(string filePath, string nodeName, Action<XmlNode> setDataAction)
        {
            try
            {
                string savePath = Path.GetFullPath(filePath);
                XmlDocument doc = new XmlDocument();
                doc.Load(savePath);

                XmlNode dataNode = doc.DocumentElement?.SelectSingleNode($"//{nodeName}");

                if (dataNode == null)
                    throw new Exception($"Node {nodeName} does not exist.");

                if (!dataNode.HasChildNodes)
                    throw new Exception($"Node {nodeName} is empty.");

                setDataAction(dataNode.FirstChild);
            }
            catch (FileNotFoundException)
            {
                setDataAction(null);
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"Error loading XML Node for {nodeName}: {ex.Message}\n{ex.StackTrace}");
                setDataAction(null);
            }
        }
    }
}
