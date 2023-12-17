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
using System.Windows.Forms;
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
        //Profile
        public string ProfileName = "Default";
        public string Author = "Owner";
        //Settings
        public bool CheckUpdatesOnStartup = true;
        public bool PracticeMode = false;
        public bool ASLMethod = false;
        public bool AutoResetSplit = false;
        //AutoSplitters Config
        public DTSekiro DataSekiro;
        public DTHollow DataHollow;
        public DTElden DataElden;
        public DTDs3 DataDs3;
        public DTDs2 DataDs2;
        public DTDs1 DataDs1;
        public DTCeleste DataCeleste;
        public DTCuphead DataCuphead;
        public DTDishonored DataDishonored;
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
        public bool _DebugMode = false;
        private SekiroSplitter sekiroSplitter = null;
        private Ds1Splitter ds1Splitter = null;
        private Ds2Splitter ds2Splitter = null;
        private Ds3Splitter ds3Splitter = null;
        private EldenSplitter eldenSplitter = null;
        private HollowSplitter hollowSplitter = null;
        private CelesteSplitter celesteSplitter = null;
        private CupheadSplitter cupSplitter = null;
        private DishonoredSplitter dishonoredSplitter = null;
        private UpdateModule updateModule = null;
        private ProfilesControl prfCtl = null;
        private AutoSplitterMainModule mainModule = null;

        public void SetPointers(SekiroSplitter sekiroSplitter,Ds1Splitter ds1Splitter,Ds2Splitter ds2Splitter,Ds3Splitter ds3Splitter,EldenSplitter eldenSplitter,HollowSplitter hollowSplitter,CelesteSplitter celesteSplitter, CupheadSplitter cupheadSplitter, DishonoredSplitter dishonoredSplitter, UpdateModule updateModule, AutoSplitterMainModule mainModule)
        {
            this.sekiroSplitter = sekiroSplitter;
            this.ds1Splitter = ds1Splitter;
            this.ds2Splitter = ds2Splitter;
            this.ds3Splitter = ds3Splitter;
            this.eldenSplitter = eldenSplitter;
            this.hollowSplitter = hollowSplitter;
            this.celesteSplitter = celesteSplitter;
            this.cupSplitter = cupheadSplitter;
            this.dishonoredSplitter = dishonoredSplitter;
            this.updateModule = updateModule;
            this.mainModule = mainModule;
        }

        /// <summary>
        /// Update Values of DataAutoSplitter
        /// </summary>
        public void UpdateAutoSplitterData()
        {
            dataAS.DataSekiro = sekiroSplitter.getDataSekiro();
            dataAS.DataHollow = hollowSplitter.getDataHollow();
            dataAS.DataElden = eldenSplitter.getDataElden();
            dataAS.DataDs3 = ds3Splitter.getDataDs3();
            dataAS.DataDs2 = ds2Splitter.getDataDs2();
            dataAS.DataDs1 = ds1Splitter.getDataDs1();
            dataAS.DataCeleste = celesteSplitter.getDataCeleste();
            dataAS.DataCuphead = cupSplitter.getDataCuphead();
            dataAS.DataDishonored = dishonoredSplitter.getDataDishonored();
            dataAS.PracticeMode = _PracticeMode;
            dataAS.CheckUpdatesOnStartup = updateModule.CheckUpdatesOnStartup;
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
            XmlSerializer formatter = new XmlSerializer(typeof(DataAutoSplitter), new Type[] { typeof(DTSekiro), typeof(DTHollow), typeof(DTElden), typeof(DTDs3), typeof(DTDs2), typeof(DTDs1), typeof(DTCeleste), typeof(DTCuphead),typeof(DTDishonored) });
            UpdateAutoSplitterData();
            formatter.Serialize(myStream, dataAS);
            myStream.Close();
        }


        /// <summary>
        /// Load user data in XML for AutoSplitter
        /// </summary>
        public void LoadAutoSplitterSettings(ProfilesControl profiles)
        {
            prfCtl = profiles;
            DTSekiro dataSekiro = null;
            DTHollow dataHollow = null;
            DTElden dataElden = null;
            DTDs3 dataDs3 = null;
            DTDs2 dataDs2 = null;
            DTDs1 dataDs1 = null;
            DTCeleste dataCeleste = null;
            DTCuphead dataCuphead = null;
            DTDishonored dataDishonored = null;

            try
            {
                Stream myStream = new FileStream("HitCounterManagerSaveAutoSplitter.xml", FileMode.Open, FileAccess.Read, FileShare.None);
                XmlSerializer formatter = new XmlSerializer(typeof(DataAutoSplitter), new Type[] { typeof(DTSekiro), typeof(DTHollow), typeof(DTElden), typeof(DTDs3), typeof(DTDs2), typeof(DTDs1), typeof(DTCeleste), typeof(DTCuphead), typeof(DTDishonored) });
                dataAS = (DataAutoSplitter)formatter.Deserialize(myStream);
                dataSekiro = dataAS.DataSekiro;
                dataHollow = dataAS.DataHollow;
                dataElden = dataAS.DataElden;
                dataDs3 = dataAS.DataDs3;
                dataDs2 = dataAS.DataDs2;
                dataDs1 = dataAS.DataDs1;
                dataCeleste = dataAS.DataCeleste;
                dataCuphead = dataAS.DataCuphead;
                dataDishonored = dataAS.DataDishonored;
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
            if (dataDishonored == null) { dataDishonored = new DTDishonored(); }

            _PracticeMode = dataAS.PracticeMode;
            updateModule.CheckUpdatesOnStartup = dataAS.CheckUpdatesOnStartup;
            sekiroSplitter.setDataSekiro(dataSekiro, profiles);
            hollowSplitter.setDataHollow(dataHollow, profiles);
            eldenSplitter.setDataElden(dataElden, profiles);
            ds3Splitter.setDataDs3(dataDs3, profiles);
            ds2Splitter.setDataDs2(dataDs2, profiles);
            ds1Splitter.setDataDs1(dataDs1, profiles);
            celesteSplitter.setDataCeleste(dataCeleste, profiles);
            cupSplitter.setDataCuphead(dataCuphead, profiles);
            dishonoredSplitter.setDataDishonored(dataDishonored, profiles);
          
        }

        /// <summary>
        /// Management Profiles
        /// </summary>
        #region ProfileManager
        public void ReLoadAutoSplitterSettings()
        {
            try
            {
                LoadAutoSplitterSettings(prfCtl);
            }catch (Exception e)
            {
                MessageBox.Show("Error to load Profile, file corrupt or not compatible\r\nLoaded Default Settings\r\nError: "+e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Delete(Path.GetFullPath("HitCounterManagerSaveAutoSplitter.xml"));
                LoadAutoSplitterSettings(prfCtl);
            }
            
            if (!_DebugMode)
            {
                mainModule.main.SetComboBoxGameIndex(mainModule.GetSplitterEnable());
                mainModule.main.SetPractice(mainModule.GetPracticeMode());
            }
            else
            {
                mainModule.debugForm.UpdateBoxes();
            }
        }

        public string GetProfileName()
        {
            return dataAS.ProfileName;
        }

        public string GetAuthor()
        {
            return dataAS.Author;
        }

        public void SetProfileName(string Name)
        {
            dataAS.ProfileName = Name;
        }

        public void SetAuthor(string Name)
        {
            dataAS.Author = Name;
        }

        public bool GetPracticeMode()
        {
            return mainModule.GetPracticeMode();
        }

        public string GetGameSelected()
        {
            int game = mainModule.GetSplitterEnable();
            switch (game)
            {
                case GameConstruction.SekiroSplitterIndex:
                    return "Sekiro";
                case GameConstruction.Ds1SplitterIndex:
                    return "Dark Souls 1";
                case GameConstruction.Ds2SplitterIndex:
                    return "Dark Souls 2";
                case GameConstruction.Ds3SplitterIndex:
                    return "Dark Souls 3";
                case GameConstruction.EldenSplitterIndex:
                    return "Elden Ring";
                case GameConstruction.HollowSplitterIndex:
                    return "Hollow Knight";
                case GameConstruction.CelesteSplitterIndex:
                    return "Celeste";
                case GameConstruction.DishonoredSplitterIndex:
                    return "Dishonored";
                case GameConstruction.CupheadSplitterIndex:
                    return "Cuphead";
                case GameConstruction.NoneSplitterIndex:
                default: return "None";
            }
        }

        public bool GetResetNewGame()
        {
            return dataAS.AutoResetSplit;
        }

        public void ResetFlags()
        {
            mainModule.ResetSplitterFlags();
        }

        #endregion
    }
    #endregion
}
