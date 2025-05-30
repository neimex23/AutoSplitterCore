﻿//MIT License

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

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using HeyRed.Mime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AutoSplitterCore
{
    public partial class GoogleAuth : ReaLTaiizor.Forms.LostForm
    {
        static string[] Scopes = { "https://www.googleapis.com/auth/userinfo.email" };
        static string ApplicationName = "autosplittercore";

        readonly string machineName = Environment.MachineName;
        DriveService driveService;
        Oauth2Service oauth2Service;
        UserCredential credential;

        SaveModule saveModule;

        string EmailLoged = null;

        static readonly string folderASCId = "1S1dSAHxxap3dzl1Y9tgrTUVxpiVmOXEJ";
        static readonly string folderHCMId = "1hYzCVP8GutPLnDwmsBvjui-dxQJoXOSj";


        public GoogleAuth(SaveModule saveModule)
        {
            InitializeComponent();
            this.saveModule = saveModule;
        }

        private void GoogleAuth_Load(object sender, EventArgs e)
        {
            textBoxWarning.Text = Properties.Resources.ProfileWarning;
            checkedListBoxGames.Items.Clear();
            foreach (var games in GameConstruction.GameList)
            {
                if (!games.Contains("None"))
                {
                    checkedListBoxGames.Items.Add(games);
                    checkedListBoxGamesSearch.Items.Add(games);
                }
            }
        }

        private object DeserializeXmlFile(string filePath, Type targetType)
        {
            using (Stream myStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                XmlSerializer formatter = new XmlSerializer(targetType);
                try
                {
                    return formatter.Deserialize(myStream);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deserializing XML file: {ex.Message}");
                }
            }
        }

        #region AuthProcess
        private void btnLogin_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "You must select a Google account and enable all the required permissions for the drive folder where the files will be downloaded.\n\n" +
                "The program will generate an encrypted key in the AutoSplitterCore_Tokens folder.\n" +
                "Do not disclose or transfer this key under any circumstances.\n\n" +
                "By continuing, you accept our Privacy Policy and Terms & Conditions.",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation
            );
            Task.Run(() => Auth());
        }

        private async Task Auth()
        {
            try
            {
                string googleCredentialsJson = await GetGoogleCredentials();
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(googleCredentialsJson)))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        machineName,
                        CancellationToken.None,
                        new FileDataStore("AutoSplitterCore_Tokens", true),
                        new CrossPlatformLocalServerCodeReceiver()
                    );


                    DebugLog.LogMessage("Authentication completed for ´user´ machine: " + machineName);
                    CreateService();
                }
            }
            catch (Exception e)
            {
                DebugLog.LogMessage("Error on Authentification process: " + e.Message);
            }
            Cursor = Cursors.Default;
        }

        //Dev should create a appsettings.json on root source code and using "Incrusting resource method" with url of AWS_ApiGateWay with name GetGoogleCredentials_ApiGateWay = api.url
        // For more info read my investigation: https://www.notion.so/Manejo-de-Secretos-c781ca2f65c449f4b9a6aa82fef3ab0a?pvs=4 (web on Spanish language)
        public static string GetAPIUrl()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("AutoSplitterCore.appsettings.json"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonContent = reader.ReadToEnd();
                    var jsonObject = JObject.Parse(jsonContent);
                    return jsonObject["ApiSettings"]["GetGoogleCredentials_ApiGateWay"].ToString();
                }
            }
            catch (Exception ex)
            {
                string Message = $"Error on Incrusted Resource APIUrl GoogleAuth: {ex.Message}";
                DebugLog.LogMessage(Message);
                MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        public static string GetIAMGoogleKey()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("AutoSplitterCore.appsettings.json"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonContent = reader.ReadToEnd();
                    var jsonObject = JObject.Parse(jsonContent);
                    return jsonObject["IAMJson"]["Google"].ToString();
                }
            }
            catch (Exception ex)
            {
                string Message = $"Error on Incrusted Resource IAMGoogle GoogleAuth: {ex.Message}";
                DebugLog.LogMessage(Message);
                MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        public async Task<string> GetGoogleCredentials()
        {
            // API Gateway URL
            string url = GetAPIUrl();

            try
            {
                // Create content for the POST request
                var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                // Perform the POST request to the API Gateway
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Validate that the response is successful
                response.EnsureSuccessStatusCode();

                // Get the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Validate if the response contains the expected format
                const string prefix = "{\"AutoSplitterCoreAuth\":";
                if (responseBody.StartsWith(prefix))
                {
                    // Extract the value after the prefix
                    string jsonFragment = responseBody.Substring(prefix.Length).Trim();

                    // Remove the last character '}' if present
                    if (jsonFragment.EndsWith("}"))
                    {
                        jsonFragment = jsonFragment.Substring(0, jsonFragment.Length - 1).Trim();
                    }

                    // Replace escaped quotes
                    jsonFragment = jsonFragment.Replace("\\\"", "\"");

                    // Remove surrounding quotes and whitespace
                    return jsonFragment.Trim('\"').Trim();
                }

                // If it doesn't have the expected format, return the full response
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                DebugLog.LogMessage("Error making the request: " + e.Message);
                throw;
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage("Unexpected error: " + ex.Message);
                throw;
            }
        }


        private static readonly HttpClient client = new HttpClient();

        private void CreateService()
        {
            // Oauth
            oauth2Service = new Oauth2Service(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Drive
            GoogleCredential IAMcredential = GoogleCredential.FromJson(GetIAMGoogleKey())
                .CreateScoped(new[] { DriveService.Scope.Drive });

            driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = IAMcredential,
                ApplicationName = ApplicationName
            });

            //FireStore
            Task.Run(async () => await FirestoreRestClient.InitializeFirestoreAsync());

            Userinfo userInfo = oauth2Service.Userinfo.Get().Execute();
            EmailLoged = userInfo.Email;

            groupBoxManagment.Invoke(new Action(() =>
            {
                AfterLoginEvents();

                linkLabel1.Text = EmailLoged;
                groupBoxManagment.Enabled = true;
                groupBoxManagment.BackColor = System.Drawing.Color.Transparent;
            }));

        }


        private void AfterLoginEvents()
        {
            btnLogin.BackColor = System.Drawing.Color.Gray;
            btnLogin.Enabled = false;
            groupBoxLogin.BackGColor = System.Drawing.Color.Gray;

            LoadFilesFromPublicFolder(folderASCId);

            textBoxCurrrentProfile.Text = saveModule.GetProfileName();
            textBoxAuthor.Text = saveModule.GetAuthor();
            TextboxDescription.Text = saveModule.GetDescription();
            textBoxDate.Text = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }


        #endregion
        #region LoadFilesOnDrive

        List<ListViewItem> allFiles = new List<ListViewItem>();
        private void LoadFilesFromPublicFolder(string folderId)
        {
            var request = driveService.Files.List();
            request.Q = $"'{folderId}' in parents and trashed=false";
            request.Fields = "files(id, name, description, properties, createdTime, mimeType, parents)";
            request.IncludeItemsFromAllDrives = true;
            request.SupportsAllDrives = true;
            request.Corpora = "allDrives";

            try
            {
                var result = request.Execute();

                listViewFilesASC.View = View.Details;
                listViewFilesASC.FullRowSelect = true;
                listViewFilesASC.GridLines = true;

                if (listViewFilesASC.Columns.Count == 0)
                {
                    listViewFilesASC.Columns.Add("Name", 150);
                    listViewFilesASC.Columns.Add("Author", 100);
                    listViewFilesASC.Columns.Add("Description", 200);
                    listViewFilesASC.Columns.Add("Games", 100);
                    listViewFilesASC.Columns.Add("Date", 100);
                    listViewFilesASC.Columns.Add("ID", 100);
                }

                listViewFilesASC.Items.Clear(); // Clean Elements

                // Process and add files to ListView
                foreach (var file in result.Files)
                {
                    var item = new ListViewItem(file.Name); // FileName

                    item.SubItems.Add(file.Properties != null && file.Properties.ContainsKey("author")
                       ? file.Properties["author"]
                       : "Unknown"); // Author
                    item.SubItems.Add(file.Description ?? "No description"); // Description

                    string games = file.Properties != null && file.Properties.ContainsKey("games")
                   ? file.Properties["games"]
                   : "None";
                    item.SubItems.Add(games); // Games                  
                    item.SubItems.Add(file.CreatedTimeDateTimeOffset?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Unknown"); // Date of Creation
                    item.SubItems.Add(file.Id); // DriveID

                    listViewFilesASC.Items.Add(item);
                }

                listViewFilesASC.Refresh();
                allFiles = new List<ListViewItem>(listViewFilesASC.Items.Cast<ListViewItem>());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region UploadProfile

        private void btnUploadProfile_Click(object sender, EventArgs e)
        {
            string profileName = textBoxCurrrentProfile.Text;
            string author = textBoxAuthor.Text;
            string description = TextboxDescription.Text;

            // Profile validation
            if (profileName == "Default" ||
                author == "Owner" ||
                description == "Default Profile")
            {
                MessageBox.Show("You must use a profile name, author, and description different from default.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Game Validation
            if (checkedListBoxGames.CheckedItems.Count == 0) { MessageBox.Show("You must select games for the profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            //Concentitation check
            if (MessageBox.Show("Your email is registered in our database to respect the terms and conditions when uploading this profile", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) return;


            //Email Validation
            var isEmailBanned = Task.Run(() => IsEmailBannedAsync(EmailLoged)).GetAwaiter().GetResult();
            if (isEmailBanned)
            {
                MessageBox.Show(
                    "You are banned from uploading profiles.\nContact the administrator to appeal.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                );
                return;
            }

            Cursor = Cursors.WaitCursor;
            //Existing Validation
            string parentFolderId = radioButtonUAsc.Checked ? folderASCId : folderHCMId;

            //Profile Selecction Validations
            string path = Path.Combine(Path.GetTempPath(), $"{profileName}.xml");

            object tempData;
            XmlSerializer serializer;

            if (radioButtonUAsc.Checked)
            {
                serializer = new XmlSerializer(typeof(DataAutoSplitter));

                if (MessageBox.Show("Splitter Flags and run will be reset to upload profile.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                saveModule.ResetFlags();
                saveModule.UpdateAutoSplitterData();

                tempData = saveModule.dataAS;
            }
            else
            {
                serializer = new XmlSerializer(typeof(ProfileHCM));

                List<string> splits = new List<string>();
                if (!SplitterControl.GetControl().GetDebug()) splits = SplitterControl.GetControl().GetSplits();

                var tempDataHCM = new ProfileHCM
                {
                    ProfileName = profileName,
                    Author = textBoxAuthor.Text,
                    Description = TextboxDescription.Text,
                    Splits = splits
                };
                tempData = tempDataHCM;
            }
            using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(stream, tempData);
            }

            UploadFile(parentFolderId, path);
            Cursor = Cursors.Default;
        }

        private void UploadFile(string parentFolderId, string path)
        {
            //Upload Drive Processs
            try
            {
                // File metadata
                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = string.Concat(textBoxCurrrentProfile.Text, ".xml"),
                    Description = TextboxDescription.Text,
                    Parents = new List<string> { parentFolderId },
                    Properties = new Dictionary<string, string>
                    {
                        { "author", textBoxAuthor.Text },
                        { "date", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") },
                        { "games", string.Join(", ", checkedListBoxGames.CheckedItems.Cast<object>().Select(g => g.ToString())) }
                    }
                };

                // Determine MIME type
                string mimeType = MimeTypesMap.GetMimeType(path);

                // Upload file
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var request = driveService.Files.Create(fileMetadata, stream, mimeType);
                    request.Fields = "id";
                    var uploadResult = request.Upload(); ;

                    if (uploadResult.Status == Google.Apis.Upload.UploadStatus.Completed)
                    {
                        var uploadedFile = request.ResponseBody;
                        SetFilePublic(uploadedFile.Id);


                        Task.Run(() => SendInformation(uploadedFile.Id).GetAwaiter());

                        MessageBox.Show("File uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (parentFolderId == folderASCId)
                        {
                            radioButtonDAsc.Checked = true;
                        }
                        else { radioButtonDHcm.Checked = true; }
                    }
                    else
                    {
                        MessageBox.Show($"File upload failed: {uploadResult.Status}. Error: {uploadResult.Exception?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                File.Delete(path);
            }
        }
        private void SetFilePublic(string fileId)
        {
            try
            {
                Google.Apis.Drive.v3.Data.Permission userPermission = new Google.Apis.Drive.v3.Data.Permission()
                {
                    Type = "anyone",
                    Role = "reader"
                };

                var request = driveService.Permissions.Create(userPermission, fileId);
                request.Fields = "id";
                request.Execute();

                var updateFile = new Google.Apis.Drive.v3.Data.File
                {
                    ViewersCanCopyContent = true,
                    CopyRequiresWriterPermission = false
                };

                var updateRequest = driveService.Files.Update(updateFile, fileId);
                updateRequest.Fields = "id, name, webViewLink, shared";
                var updatedFile = updateRequest.Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error to Settings Permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SendInformation(string idFile)
        {
            try
            {
                Dictionary<string, object> register = new Dictionary<string, object>
                {
                    { "Email", EmailLoged },
                    { "IDFile", idFile },
                    { "NameFile", $"{textBoxCurrrentProfile.Text}.xml" },
                    { "Date", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") }
                };

                await FirestoreRestClient.AddDocumentAsync("uploadHistory", idFile, register);
                DebugLog.LogMessage($"Record uploaded to Firestore: {idFile} por {EmailLoged}");
            }
            catch (Exception ex) { DebugLog.LogMessage($"Error writing to Firestore {idFile}: {ex.Message}"); }
        }



        public async Task<bool> IsEmailBannedAsync(string email)
        {
            return await FirestoreRestClient.IsDocumentExistsAsync("bannedEmails", "Email", email);
        }


        #region KindUpload
        private bool _isHandlingCheckedChanged;
        private void radioButtonUAsc_CheckedChanged(object sender, EventArgs e)
        {
            if (_isHandlingCheckedChanged) return;

            _isHandlingCheckedChanged = true;
            radioButtonUHcm.Checked = !radioButtonUAsc.Checked;
            _isHandlingCheckedChanged = false;
        }

        private void radioButtonUHcm_CheckedChanged(object sender, EventArgs e)
        {
            if (_isHandlingCheckedChanged) return;

            _isHandlingCheckedChanged = true;
            radioButtonUHcm.Checked = !radioButtonUAsc.Checked;

            if (radioButtonUHcm.Checked)
            {
                string currentProfile = string.Empty;
                if (!SplitterControl.GetControl().GetDebug()) currentProfile = SplitterControl.GetControl().GetHCMProfileName();
                textBoxCurrrentProfile.Text = currentProfile;
                TextboxDescription.Enabled = true;
                textBoxAuthor.Enabled = true;
            }
            else
            {
                textBoxCurrrentProfile.Text = saveModule.GetProfileName();
                textBoxAuthor.Text = saveModule.GetAuthor();
                TextboxDescription.Text = saveModule.GetDescription();
                TextboxDescription.Enabled = false;
                textBoxAuthor.Enabled = false;
            }

            _isHandlingCheckedChanged = false;
        }


        #endregion
        #endregion
        #region DownloadFile
        public void DownloadFile(string fileId, string destinationPath)
        {
            try
            {
                var request = driveService.Files.Get(fileId);
                using (var stream = new MemoryStream())
                {
                    request.MediaDownloader.ProgressChanged += progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                DebugLog.LogMessage($"Downloading: {progress.BytesDownloaded} bytes...");
                                break;
                            case DownloadStatus.Completed:
                                DebugLog.LogMessage("Download completed.");
                                break;
                            case DownloadStatus.Failed:
                                DebugLog.LogMessage("Download failed.");
                                break;
                        }
                    };

                    request.Download(stream);

                    if (stream.Length > 0)
                    {
                        stream.Position = 0;
                        using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                        {
                            stream.CopyTo(fileStream);
                            fileStream.Close();
                        }
                        DebugLog.LogMessage($"File saved to {destinationPath}");
                    }
                    else
                    {
                        DebugLog.LogMessage("Stream is empty. No data was downloaded.");
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"Error downloading file: {ex.Message}");
            }
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewFilesASC.SelectedItems.Count > 0)
            {
                Cursor = Cursors.WaitCursor;
                var selectedItem = listViewFilesASC.SelectedItems[0];
                var fileId = selectedItem.SubItems[5].Text;
                var fileName = selectedItem.Text;

                if (!fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("The Selected Filed not is a XML Format.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    string tempFilePath = Path.Combine(Path.GetFullPath("./AutoSplitterProfiles"), "tmp_" + fileName); ;
                    DownloadFile(fileId, tempFilePath);

                    if (radioButtonDAsc.Checked)
                    {
                        var configuration = DeserializeXmlFile(tempFilePath, typeof(DataAutoSplitter)) as DataAutoSplitter;
                        SaveModule saveModuleLocal = new SaveModule
                        {
                            dataAS = configuration,
                        };

                        TextBoxSummary.Text = ProfileManager.BuildSummary(saveModuleLocal);
                    }
                    else
                    {
                        var profileHCm = DeserializeXmlFile(tempFilePath, typeof(ProfileHCM)) as ProfileHCM;
                        TextBoxSummary.Text = ProfileManager.BuildSummaryProfile(profileHCm);
                    }

                    File.Delete(tempFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error to process file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor = Cursors.Default;
            }
        }

        #region FilterListViewFileter
        List<ListViewItem> filteredItems = null;
        private void textBoxSearch_TextChanged(object sender, EventArgs e) => AplyFilters();

        private void checkedListBoxGamesSearch_ItemCheck(object sender, ItemCheckEventArgs e) => this.BeginInvoke((MethodInvoker)AplyFilters);

        private void AplyFilters()
        {
            filteredItems = allFiles;

            // TextFilter
            string filterText = textBoxSearch.Text;
            if (!string.IsNullOrWhiteSpace(filterText))
            {
                filteredItems = filteredItems
                    .Where(item => item.SubItems[0].Text.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }

            // CheckBoxFilter
            var checkedItems = checkedListBoxGamesSearch.CheckedItems.Cast<string>().ToList();
            if (checkedItems.Any())
            {
                filteredItems = filteredItems
                    .Where(item => checkedItems.Any(game => item.SubItems[3].Text.Contains(game)))
                    .ToList();
            }

            //Refresh Listvirew
            listViewFilesASC.Items.Clear();
            listViewFilesASC.Items.AddRange(filteredItems.ToArray());
        }
        #endregion

        private void radioButtonDHcm_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDHcm.Checked)
            {
                radioButtonDAsc.Checked = false;
                LoadFilesFromPublicFolder(folderHCMId);
            }
        }

        private void radioButtonDAsc_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDAsc.Checked)
            {
                radioButtonDHcm.Checked = false;
                LoadFilesFromPublicFolder(folderASCId);
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (listViewFilesASC.SelectedItems.Count > 0)
            {
                Cursor = Cursors.WaitCursor;
                var selectedItem = listViewFilesASC.SelectedItems[0];
                var fileId = selectedItem.SubItems[5].Text;
                var fileName = selectedItem.Text;

                string tempFilePath = Path.Combine(Path.GetFullPath("./AutoSplitterProfiles"), "tmp_" + fileName); ;
                DownloadFile(fileId, tempFilePath);

                if (radioButtonDAsc.Checked)
                {
                    //Check Integrity
                    _ = (DataAutoSplitter)DeserializeXmlFile(tempFilePath, typeof(DataAutoSplitter));

                    string finalFilePath = Path.Combine(Path.GetFullPath("./AutoSplitterProfiles"), fileName);

                    if (File.Exists(finalFilePath))
                    {
                        // Get file name without extension and extension
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                        string extension = Path.GetExtension(fileName);

                        // Generate the name with suffix _2, and if it exists, increment the number (_3, _4, etc.)
                        int count = 2;
                        string newFileName;

                        do
                        {
                            newFileName = $"{fileNameWithoutExtension}_{count}{extension}";
                            count++;
                        } while (File.Exists(Path.Combine(Path.GetFullPath("./AutoSplitterProfiles"), newFileName)));

                        // Rename Exist File
                        string renamedFilePath = Path.Combine(Path.GetFullPath("./AutoSplitterProfiles"), newFileName);
                        File.Move(finalFilePath, renamedFilePath);
                    }

                    File.Move(tempFilePath, finalFilePath);
                }
                else
                {
                    var splitterControl = SplitterControl.GetControl();
                    if (!splitterControl.GetDebug())
                    {
                        var profileHCM = (ProfileHCM)DeserializeXmlFile(tempFilePath, typeof(ProfileHCM));

                        if (splitterControl.GetAllHcmProfile().Exists(x => x.Equals(profileHCM.ProfileName)))
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("A profile with this name already exists!\nChange name and try again.", "Profile already exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            File.Delete(tempFilePath);
                            return;
                        }

                        splitterControl.NewProfile(profileHCM.ProfileName);
                        foreach (var split in profileHCM.Splits)
                        {
                            splitterControl.AddSplit(split);
                        }
                    }
                    else DebugLog.LogMessage("Debug Mode on Install Profile HCM: InterfaceHCM not Setted");
                }

                File.Delete(tempFilePath);
                Cursor = Cursors.Default;
                if (MessageBox.Show("Install Complete, you can Switch the profile on Profile Manager\nDo you want close this windows?", "Susesfully", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) Close();
            }
            else
            {
                MessageBox.Show("You must Select File to download", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void linkLabelPrivacy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => AutoSplitterMainModule.OpenWithBrowser(new Uri("https://neimex23.github.io/AutoSplitterCore/PrivacyPolicy.html"));

        private void linkLabelTerms_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => AutoSplitterMainModule.OpenWithBrowser(new Uri("https://neimex23.github.io/AutoSplitterCore/TermsAndConditions.html"));

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (listViewFilesASC.SelectedItems.Count > 0)
            {
                var selectedItem = listViewFilesASC.SelectedItems[0];
                var fileId = selectedItem.SubItems[5].Text;
                var fileName = selectedItem.Text;
                string baseUrl = "https://docs.google.com/forms/d/e/1FAIpQLScn76IddWwTunZdWiL-stkafrewrTWf6MLS8VcseH-awIyJYA/viewform?usp=pp_url";

                string encodedFileID = HttpUtility.UrlEncode(fileId);
                string encodedFileName = HttpUtility.UrlEncode(fileName);

                string fullUrl = $"{baseUrl}&entry.293656606={encodedFileID}&entry.1666283038={encodedFileName}";

                AutoSplitterMainModule.OpenWithBrowser(new Uri(fullUrl));
            }
        }
    }

    public class CrossPlatformLocalServerCodeReceiver : ICodeReceiver
    {
        public string RedirectUri => "http://127.0.0.1:55088/";

        public async Task<AuthorizationCodeResponseUrl> ReceiveCodeAsync(
            AuthorizationCodeRequestUrl url,
            CancellationToken cancellationToken)
        {
            AutoSplitterMainModule.OpenWithBrowser(new Uri(url.Build().ToString()));

            var listener = new HttpListener();
            listener.Prefixes.Add(RedirectUri);
            listener.Start();

            try
            {
                var getContextTask = GetContextAsync(listener);
                var cancellationTask = Task.Delay(-1, cancellationToken);
                var completedTask = await Task.WhenAny(getContextTask, cancellationTask);

                if (completedTask == getContextTask)
                {
                    var context = await getContextTask;
                    var response = context.Request.Url.Query;

                    if (response.StartsWith("?"))
                        response = response.Substring(1);

                    var parsedQuery = HttpUtility.ParseQueryString(response);
                    var code = parsedQuery.Get("code");
                    var error = parsedQuery.Get("error");
                    var errorDescription = parsedQuery.Get("error_description");

                    var authorizationCodeResponse = new AuthorizationCodeResponseUrl
                    {
                        Code = code,
                        Error = error,
                        ErrorDescription = errorDescription
                    };

                    string responseHtml = GenerateHTML();
                    byte[] responseBytes = Encoding.UTF8.GetBytes(responseHtml);

                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/html";
                    context.Response.ContentLength64 = responseBytes.Length;

                    using (var output = context.Response.OutputStream)
                    {
                        await output.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await output.FlushAsync();
                    }

                    return authorizationCodeResponse;
                }
                else
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    throw new OperationCanceledException();
                }
            }
            finally
            {
                listener.Stop();
            }
        }


        private Task<HttpListenerContext> GetContextAsync(HttpListener listener)
        {
            return listener.GetContextAsync();
        }

        private string GenerateHTML()
        {
            return @"
            <html lang=""en"">
              <head>
                <meta charset=""UTF-8"" />
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                <title>Authentication Complete</title>
                <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.19/dist/tailwind.min.css"" rel=""stylesheet"" />
              </head>
              <body class=""bg-black min-h-screen flex flex-col items-center justify-center text-center px-4"">
                <header class=""w-full bg-gray-800 text-white py-4"">
                  <div class=""container mx-auto"">
                    <h1 class=""text-3xl font-bold"">AutoSplitterCore</h1>
                  </div>
                </header>

                <main class=""flex-grow flex items-center justify-center"">
                  <div class=""bg-white text-black rounded-lg shadow-lg p-6 max-w-lg w-full"">
                    <h2 class=""text-3xl font-bold mb-4"">Authentication Complete</h2>
                    <p class=""text-lg mb-6"">You can now close this window.</p>
                  </div>
                </main>

                <footer class=""w-full bg-gray-900 text-white py-4 text-center"">
                  <div class=""container mx-auto flex flex-col items-center gap-2"">
                    <div class=""flex flex-wrap justify-center gap-3 text-sm md:text-base"">
                      <a href=""https://github.com/neimex23/AutoSplitterCore"" target=""_blank"" rel=""noopener noreferrer"" class=""hover:underline"">Open Source on GitHub</a>
                      <span class=""opacity-50"">|</span>
                      <a href=""https://neimex23.github.io/AutoSplitterCore/TermsAndConditions"" target=""_blank"" rel=""noopener noreferrer"" class=""hover:underline"">Terms and Conditions</a>
                      <span class=""opacity-50"">|</span>
                      <a href=""https://neimex23.github.io/AutoSplitterCore/PrivacyPolicy"" target=""_blank"" rel=""noopener noreferrer"" class=""hover:underline"">Privacy Policy</a>
                        <span class=""opacity-50"">|</span>
                      <a href=""https://neimex23.github.io/AutoSplitterCore/"" target=""_blank"" rel=""noopener noreferrer"" class=""hover:underline"">Home Page</a>
                    </div>
                    <p class=""text-xs md:text-sm opacity-75"">© 2025 AutoSplitterCore. Open Source Project.</p>
                  </div>
                </footer>
              </body>
            </html>
            ";
        }


    }
    public static class FirestoreRestClient
    {
        private static string _firestoreBaseUrl;
        private static string _accessToken;

        private static byte[] GetIAMFireBaseKeyBytes()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("AutoSplitterCore.appsettings.json"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonContent = reader.ReadToEnd();
                    var jsonObject = JObject.Parse(jsonContent);
                    return Encoding.UTF8.GetBytes(jsonObject["IAMJson"]["FireBase"].ToString());
                }
            }
            catch (Exception ex)
            {
                string Message = $"Error on Incrusted Resource IAMFirebase GoogleAuth: {ex.Message}";
                DebugLog.LogMessage(Message, ex);
                MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static async Task InitializeFirestoreAsync()
        {
            try
            {
                // Obtener las credenciales en formato JSON
                byte[] firebaseCredentials = GetIAMFireBaseKeyBytes();
                string jsonCredentials = Encoding.UTF8.GetString(firebaseCredentials);

                // Autenticación usando GoogleCredential sin Protobuf
                GoogleCredential credential = GoogleCredential.FromJson(jsonCredentials)
                    .CreateScoped("https://www.googleapis.com/auth/datastore");

                // Obtener un token de acceso
                _accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

                // Construir la URL base de Firestore
                string projectId = JsonConvert.DeserializeObject<dynamic>(jsonCredentials)["project_id"];
                _firestoreBaseUrl = $"https://firestore.googleapis.com/v1/projects/{projectId}/databases/(default)/documents";

                DebugLog.LogMessage("Firestore REST API Client Initialized.");
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"Error initializing Firestore: {ex.Message}");
                throw;
            }
        }

        public static async Task AddDocumentAsync(string collection, string documentId, Dictionary<string, object> data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                string url = $"{_firestoreBaseUrl}/{collection}/{documentId}";

                // Formatear los datos para la API REST de Firestore
                var firestoreData = new Dictionary<string, object>();
                foreach (var kvp in data)
                {
                    firestoreData[kvp.Key] = new Dictionary<string, object>
                    {
                        { "stringValue", kvp.Value.ToString() }
                    };
                }

                var jsonContent = JsonConvert.SerializeObject(new { fields = firestoreData });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
                {
                    Content = content
                };

                HttpResponseMessage response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }
            }
        }


        public static async Task<bool> IsDocumentExistsAsync(string collection, string fieldName, string value)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                string url = $"{_firestoreBaseUrl}/{collection}?pageSize=10";
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    DebugLog.LogMessage($"Error querying Firestore: {await response.Content.ReadAsStringAsync()}");
                    return false;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var documents = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                foreach (var doc in documents["documents"])
                {
                    var fields = doc["fields"];
                    if (fields != null && fields[fieldName] != null && fields[fieldName]["stringValue"].ToString() == value)
                    {
                        return true; // The email is on the banned list
                    }
                }

                return false;
            }
        }


    }
}
