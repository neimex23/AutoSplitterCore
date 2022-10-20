//MIT License

//Copyright (c) 2022 Ezequiel Medina

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
using System.Xml.Serialization;
using HitCounterManager;

namespace AutoSplitterCore
{
    #region DataAutoSplitter
    /// <summary>
    /// Class Contains All Settings of AutoSplitterCore
    /// </summary>
    [Serializable]
    public class DataAutoSplitter
    {
        //Settings
        public bool CheckUpdatesOnStartup = true;
        public bool PracticeMode = false;
        public bool ASLMethod = false;
        //AutoSplitters Config
        public DTSekiro DataSekiro;
        public DTHollow DataHollow;
        public DTElden DataElden;
        public DTDs3 DataDs3;
        public DTDs2 DataDs2;
        public DTDs1 DataDs1;
        public DTCeleste DataCeleste;
        public DTCuphead DataCuphead;
    }
    #endregion
    #region SaveModule
    /// <summary>
    /// Class Save and Load All Settings of AutoSplitterCore
    /// </summary>
    public class SaveModule
    {
        public DataAutoSplitter dataAS = new DataAutoSplitter();
        public bool _PracticeMode = false;
        private SekiroSplitter sekiroSplitter = null;
        private Ds1Splitter ds1Splitter = null;
        private Ds2Splitter ds2Splitter = null;
        private Ds3Splitter ds3Splitter = null;
        private EldenSplitter eldenSplitter = null;
        private HollowSplitter hollowSplitter = null;
        private CelesteSplitter celesteSplitter = null;
        private CupheadSplitter cupSplitter = null;
        private AslSplitter aslSplitter = null;
        private UpdateModule updateModule = null;

        public void SetPointers(SekiroSplitter sekiroSplitter,Ds1Splitter ds1Splitter,Ds2Splitter ds2Splitter,Ds3Splitter ds3Splitter,EldenSplitter eldenSplitter,HollowSplitter hollowSplitter,CelesteSplitter celesteSplitter, CupheadSplitter cupheadSplitter, AslSplitter aslSplitter, UpdateModule updateModule)
        {
            this.sekiroSplitter = sekiroSplitter;
            this.ds1Splitter = ds1Splitter;
            this.ds2Splitter = ds2Splitter;
            this.ds3Splitter = ds3Splitter;
            this.eldenSplitter = eldenSplitter;
            this.hollowSplitter = hollowSplitter;
            this.celesteSplitter = celesteSplitter;
            this.cupSplitter = cupheadSplitter;
            this.aslSplitter = aslSplitter;
            this.updateModule = updateModule;
        }

        /// <summary>
        /// Stores user data in new XML for AutoSplitter
        /// </summary>
        public void SaveAutoSplitterSettings()
        {
            bool newSave = false;
            string savePath = Path.GetFullPath("HitCounterManagerSaveAutoSplitter.xml");
            string saveBakPath = Path.GetFullPath("HitCounterManagerSaveAutoSplitter.xml.bak");
            if (!File.Exists(savePath))
            {
                newSave = true;
            }

            if (File.Exists(saveBakPath))
            {
                File.Delete(saveBakPath);
            }

            if (!newSave) { File.Move(savePath, saveBakPath); }
            File.Delete(savePath);
            Stream myStream = new FileStream("HitCounterManagerSaveAutoSplitter.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer formatter = new XmlSerializer(typeof(DataAutoSplitter), new Type[] { typeof(DTSekiro), typeof(DTHollow), typeof(DTElden), typeof(DTDs3), typeof(DTDs2), typeof(DTDs1), typeof(DTCeleste), typeof(DTCuphead) });
            dataAS.DataSekiro = sekiroSplitter.getDataSekiro();
            dataAS.DataHollow = hollowSplitter.getDataHollow();
            dataAS.DataElden = eldenSplitter.getDataElden();
            dataAS.DataDs3 = ds3Splitter.getDataDs3();
            dataAS.DataDs2 = ds2Splitter.getDataDs2();
            dataAS.DataDs1 = ds1Splitter.getDataDs1();
            dataAS.DataCeleste = celesteSplitter.getDataCeleste();
            dataAS.DataCuphead = cupSplitter.getDataCuphead();
            dataAS.ASLMethod = aslSplitter.enableSplitting;
            dataAS.PracticeMode = _PracticeMode;
            dataAS.CheckUpdatesOnStartup = updateModule.CheckUpdatesOnStartup;
            formatter.Serialize(myStream, dataAS);
            myStream.Close();
            XmlDocument Save = new XmlDocument();
            Save.Load(savePath);
            XmlNode Asl = Save.CreateElement("DataASL");
            XmlNode AslData = aslSplitter.getData(Save);
            Asl.AppendChild(AslData);
            Save.DocumentElement.AppendChild(Asl);
            Save.Save(savePath);
        }

        /// <summary>
        /// Load user data in XML for AutoSplitter
        /// </summary>
        public void LoadAutoSplitterSettings(ProfilesControl profiles)
        {
            DTSekiro dataSekiro = null;
            DTHollow dataHollow = null;
            DTElden dataElden = null;
            DTDs3 dataDs3 = null;
            DTDs2 dataDs2 = null;
            DTDs1 dataDs1 = null;
            DTCeleste dataCeleste = null;
            DTCuphead dataCuphead = null;

            try
            {
                Stream myStream = new FileStream("HitCounterManagerSaveAutoSplitter.xml", FileMode.Open, FileAccess.Read, FileShare.None);
                XmlSerializer formatter = new XmlSerializer(typeof(DataAutoSplitter), new Type[] { typeof(DTSekiro), typeof(DTHollow), typeof(DTElden), typeof(DTDs3), typeof(DTDs2), typeof(DTDs1), typeof(DTCeleste), typeof(DTCuphead) });
                dataAS = (DataAutoSplitter)formatter.Deserialize(myStream);
                dataSekiro = dataAS.DataSekiro;
                dataHollow = dataAS.DataHollow;
                dataElden = dataAS.DataElden;
                dataDs3 = dataAS.DataDs3;
                dataDs2 = dataAS.DataDs2;
                dataDs1 = dataAS.DataDs1;
                dataCeleste = dataAS.DataCeleste;
                dataCuphead = dataAS.DataCuphead;
                myStream.Close();
            }
            catch (Exception) { }

            //Case Old Savefile or New file;
            if (dataSekiro == null) { dataSekiro = new DTSekiro(); }
            if (dataHollow == null) { dataHollow = new DTHollow(); }
            if (dataElden == null) { dataElden = new DTElden(); }
            if (dataDs3 == null) { dataDs3 = new DTDs3(); }
            if (dataDs2 == null) { dataDs2 = new DTDs2(); }
            if (dataDs1 == null) { dataDs1 = new DTDs1(); }
            if (dataCeleste == null) { dataCeleste = new DTCeleste(); }
            if (dataCuphead == null) { dataCuphead = new DTCuphead(); }

            _PracticeMode = dataAS.PracticeMode;
            updateModule.CheckUpdatesOnStartup = dataAS.CheckUpdatesOnStartup;
            aslSplitter.enableSplitting = dataAS.ASLMethod;
            sekiroSplitter.setDataSekiro(dataSekiro, profiles);
            hollowSplitter.setDataHollow(dataHollow, profiles);
            eldenSplitter.setDataElden(dataElden, profiles);
            ds3Splitter.setDataDs3(dataDs3, profiles);
            ds2Splitter.setDataDs2(dataDs2, profiles);
            ds1Splitter.setDataDs1(dataDs1, profiles);
            celesteSplitter.setDataCeleste(dataCeleste, profiles);
            cupSplitter.setDataCuphead(dataCuphead, profiles);
            try
            {
                string savePath = Path.GetFullPath("HitCounterManagerSaveAutoSplitter.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(savePath);

                XmlElement docElements = doc.DocumentElement;
                XmlNodeList nodeList = docElements.SelectNodes("//DataASL");

                foreach (XmlNode node in nodeList)
                {
                    aslSplitter.setData(node.FirstChild, profiles);
                }
            }
            catch (Exception) { aslSplitter.setData(null, profiles); }
        }
    }
    #endregion
}
