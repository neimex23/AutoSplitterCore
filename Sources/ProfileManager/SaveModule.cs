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
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using HitCounterManager;
using ReaLTaiizor.Extension;

namespace AutoSplitterCore
{
    public enum StyleMode { Default, Light ,Dark};
    /// <summary>
    /// Classes Contains All Settings of AutoSplitterCore
    /// </summary>
    #region DataAutoSplitter


    /// <summary>
    /// Class Contains All Splits Configurations of AutoSplitterCore
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(DTSekiro))]
    [XmlInclude(typeof(DTHollow))]
    [XmlInclude(typeof(DTElden))]
    [XmlInclude(typeof(DTDs3))]
    [XmlInclude(typeof(DTDs2))]
    [XmlInclude(typeof(DTDs1))]
    [XmlInclude(typeof(DTCeleste))]
    [XmlInclude(typeof(DTCuphead))]
    [XmlInclude(typeof(DTDishonored))]
    public class DataAutoSplitter
    {
        //Profile
        public string ProfileName = "Default";
        public string Author = "Owner";
        public string Description = "Default Profile";

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

    /// <summary>
    /// Class Contains General User Settings of AutoSplitterCore
    /// </summary>
    [Serializable]
    public class GeneralAutoSplitter
    {
        //Settings
        public string saveProfilePath = Path.GetFullPath("./AutoSplitterProfiles");
        public bool CheckUpdatesOnStartup = true;
        public bool PracticeMode = false;
        public bool AutoResetSplit = false;
        public bool ResetProfile = false;
        public StyleMode StyleMode = StyleMode.Default;

        public List<ProfileLink> profileLinks = new List<ProfileLink>();
    }

    /// <summary>
    /// Represent a ProfileLink
    /// </summary>
    [Serializable]
    public class ProfileLink
    {
        public string profileHCM = string.Empty;
        public string profileASC = string.Empty;

        public ProfileLink() { }
        public ProfileLink(string profileHCM, string profileASC) { this.profileHCM = profileHCM; this.profileASC = profileASC; }
    }

    /// <summary>
    /// Represent Profile of HitCounterManager
    /// </summary>
    [Serializable]
    public class ProfileHCM
    {
        public string ProfileName = "Default";
        public string Author = "Owner";
        public string Description = "Default Profile";
        
        public List<string> Splits = new List<string>();
    }

    #endregion
    #region SaveModule
    /// <summary>
    /// Class Save and Load All Settings of AutoSplitterCore
    /// </summary>
    public class SaveModule
    {
        public DataAutoSplitter dataAS = new DataAutoSplitter();
        public GeneralAutoSplitter generalAS = new GeneralAutoSplitter();
        public bool _PracticeMode = false;
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
            dataAS.DataSekiro = sekiroSplitter.GetDataSekiro();
            dataAS.DataHollow = hollowSplitter.GetDataHollow();
            dataAS.DataElden = eldenSplitter.GetDataElden();
            dataAS.DataDs3 = ds3Splitter.GetDataDs3();
            dataAS.DataDs2 = ds2Splitter.GetDataDs2();
            dataAS.DataDs1 = ds1Splitter.GetDataDs1();
            dataAS.DataCeleste = celesteSplitter.GetDataCeleste();
            dataAS.DataCuphead = cupSplitter.GetDataCuphead();
            dataAS.DataDishonored = dishonoredSplitter.GetDataDishonored();
            generalAS.PracticeMode = _PracticeMode;
            generalAS.CheckUpdatesOnStartup = updateModule.CheckUpdatesOnStartup;
        }

        /// <summary>
        /// Stores user data in new XML for AutoSplitter
        /// </summary>
        public void SaveAutoSplitterSettings()
        {
            UpdateAutoSplitterData();

            //DataAutoSplitter
            bool newSave = false;
            string savePath = Path.GetFullPath("SaveDataAutoSplitter.xml");
            string saveBakPath = Path.GetFullPath("SaveDataAutoSplitter.xml.bak");
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
            Stream myStream = new FileStream("SaveDataAutoSplitter.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer formatter = new XmlSerializer(typeof(DataAutoSplitter));
            formatter.Serialize(myStream, dataAS);
            myStream.Close();

            //GeneralAutoSplitter
            newSave = false;
            savePath = Path.GetFullPath("SaveGeneralAutoSplitter.xml");
            savePath = Path.GetFullPath("SaveGeneralAutoSplitter.xml.bak");

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
            myStream = new FileStream("SaveGeneralAutoSplitter.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter = new XmlSerializer(typeof(GeneralAutoSplitter));
            formatter.Serialize(myStream, generalAS);
            myStream.Close();
        }


        /// <summary>
        /// Load user data in XML for AutoSplitter
        /// </summary>
        public void LoadAutoSplitterSettings()
        {
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
                Stream myStream = new FileStream("SaveDataAutoSplitter.xml", FileMode.Open, FileAccess.Read, FileShare.None);
                XmlSerializer formatter = new XmlSerializer(typeof(DataAutoSplitter));
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

                myStream = new FileStream("SaveGeneralAutoSplitter.xml", FileMode.Open, FileAccess.Read, FileShare.None);
                formatter = new XmlSerializer(typeof(DataAutoSplitter));
                generalAS = (GeneralAutoSplitter)formatter.Deserialize(myStream);
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

            _PracticeMode = generalAS.PracticeMode;
            updateModule.CheckUpdatesOnStartup = generalAS.CheckUpdatesOnStartup;
            sekiroSplitter.SetDataSekiro(dataSekiro);
            hollowSplitter.SetDataHollow(dataHollow);
            eldenSplitter.SetDataElden(dataElden);
            ds3Splitter.SetDataDs3(dataDs3);
            ds2Splitter.SetDataDs2(dataDs2);
            ds1Splitter.SetDataDs1(dataDs1);
            celesteSplitter.SetDataCeleste(dataCeleste);
            cupSplitter.SetDataCuphead(dataCuphead);
            dishonoredSplitter.SetDataDishonored(dataDishonored);
          
        }

        /// <summary>
        /// Management Profiles
        /// </summary>
        #region ProfileManager
        public void ReLoadAutoSplitterSettings()
        {
            try
            {
                LoadAutoSplitterSettings();
            }catch (Exception e)
            {
                MessageBox.Show("Error to load Profile, file corrupt or not compatible\r\nLoaded Default Settings\r\nError: "+e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Delete(Path.GetFullPath("SaveDataAutoSplitter.xml"));
                LoadAutoSplitterSettings();
            }
            
            if (!SplitterControl.GetControl().GetDebug())
            {
                SplitterControl.GetControl().SetActiveGameIndex(mainModule.GetSplitterEnable());
                SplitterControl.GetControl().SetPracticeMode(mainModule.GetPracticeMode());
            }
            else
            {
                #if !HCMv2
                    mainModule.debugForm.UpdateBoxes();
                #endif
            }
        }

        public string GetProfileName() => dataAS.ProfileName;

        public string GetAuthor() => dataAS.Author;

        public void SetProfileName(string Name)  => dataAS.ProfileName = Name;

        public void SetAuthor(string Name) => dataAS.Author = Name;

        public void SetDescription(string Description) => dataAS.Description = Description;

        public bool GetPracticeMode() => mainModule.GetPracticeMode();

        public string GetGameSelected()
        {
            int game = mainModule.GetSplitterEnable();
            return GameConstruction.GameList[game];
        }

        public bool GetResetNewGame() => generalAS.AutoResetSplit;

        public void ResetFlags() => mainModule.ResetSplitterFlags();

        public string GetStyle() => generalAS.StyleMode.ToString();

        public string GetDescription() => dataAS.Description;

        public void AddProfileLink(string hcmProfile, string ascProfile)
        {
            generalAS.profileLinks.Add(new ProfileLink(hcmProfile, ascProfile));
        }

        public void RemoveProfileLink(int index)
        {
            generalAS.profileLinks.RemoveAt(index);
        }

        public bool ProfileLinkReady() => generalAS.profileLinks.Count > 0;


        public AutoSplitterMainModule MainModule { get { return mainModule; } set { mainModule = value; } }

        #endregion
    }
    #endregion
}
