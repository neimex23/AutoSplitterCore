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

using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Download;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Web;
using Google.Apis.Util.Store;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;


namespace AutoSplitterCore
{
    public partial class GoogleAuth : ReaLTaiizor.Forms.LostForm
    {
        static string[] Scopes = { "https://www.googleapis.com/auth/userinfo.email"};
        static string ApplicationName = "autosplittercore";
        static FirestoreDb _firestoreDb;

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
                        new FileDataStore("AutoSplitterCore_Tokens", true) 
                    );


                    Console.WriteLine("Authentication completed for ´user´ machine: " + machineName);
                    CreateService();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error on Authentification process: " + e.Message);
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
                throw new Exception("Error on Incrusted Resource: ", ex);
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
                throw new Exception("Error on Incrusted Resource: ", ex);
            }
        }

        public static byte[] GetIAMFireBaseKeyBytes()
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
                throw new Exception("Error on Incrusted Resource: ", ex);
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
                Console.WriteLine("Error making the request: " + e.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
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
            InitializeFirestore();
           
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

        public static void InitializeFirestore()
        {
            byte[] firebaseCredentials = GetIAMFireBaseKeyBytes();

            using (var stream = new MemoryStream(firebaseCredentials))
            {
                var credentials = GoogleCredential.FromStream(stream)
                    .CreateScoped(FirestoreClient.DefaultScopes);

                Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), credentials.ToChannelCredentials());

                FirestoreClient _firestoreClient = new FirestoreClientBuilder
                {
                    Endpoint = FirestoreClient.DefaultEndpoint,
                    ChannelCredentials = credentials.ToChannelCredentials()
                }.Build();

                _firestoreDb = FirestoreDb.Create(ApplicationName, _firestoreClient);
            }
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
            if (checkedListBoxGames.CheckedItems.Count == 0) { MessageBox.Show("You must select games for the profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);  return; }

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
                    Name = string.Concat(textBoxCurrrentProfile.Text,".xml"),
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
                string mimeType = MimeMapping.GetMimeMapping(path);

                // Upload file
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var request = driveService.Files.Create(fileMetadata, stream, mimeType);
                    request.Fields = "id";
                    var uploadResult = request.Upload();;

                    if (uploadResult.Status == Google.Apis.Upload.UploadStatus.Completed)
                    {
                        var uploadedFile = request.ResponseBody;
                        SetFilePublic(uploadedFile.Id);


                        Task.Run(() => SendInformation(uploadedFile.Id).GetAwaiter());

                        MessageBox.Show("File uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (parentFolderId == folderASCId)
                        {
                            radioButtonDAsc.Checked = true;
                        }else {  radioButtonDHcm.Checked = true; }                   
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
            CollectionReference collection = _firestoreDb.Collection("uploadHistory");
            Dictionary<string, object> register = new Dictionary<string, object>
            {
                { "Email", EmailLoged },
                { "IDFile", idFile },
                { "NameFile", string.Concat(textBoxCurrrentProfile.Text, ".xml") },
                { "Date", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") }
            };
            await collection.AddAsync(register);
            Console.WriteLine($"Upload Register on Firestore: {idFile} by {EmailLoged}");
        }

        public async Task<bool> IsEmailBannedAsync(string email)
        {
            CollectionReference coleccion = _firestoreDb.Collection("bannedEmails");
            QuerySnapshot snapshot = await coleccion.WhereEqualTo("Email", email).GetSnapshotAsync();
            return snapshot.Count > 0;
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
                                Console.WriteLine($"Downloading: {progress.BytesDownloaded} bytes...");
                                break;
                            case DownloadStatus.Completed:
                                Console.WriteLine("Download completed.");
                                break;
                            case DownloadStatus.Failed:
                                Console.WriteLine("Download failed.");
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
                        Console.WriteLine($"File saved to {destinationPath}");
                    }
                    else
                    {
                        Console.WriteLine("Stream is empty. No data was downloaded.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
            }
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewFilesASC.SelectedItems.Count > 0)
            {
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
                            MainModule = this.saveModule.MainModule
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

                    File.Move(tempFilePath, finalFilePath);      
                }else
                {
                    var splitterControl = SplitterControl.GetControl();
                    if (!splitterControl.GetDebug())
                    {
                        var profileHCM = (ProfileHCM)DeserializeXmlFile(tempFilePath, typeof(ProfileHCM));

                        if (splitterControl.GetAllHcmProfile().Exists(x => x.Equals(profileHCM.ProfileName)))
                        {
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
                    else Console.WriteLine("Debug Mode on Install Profile HCM: InterfaceHCM not Setted");
                }

                File.Delete(tempFilePath);
                if (MessageBox.Show("Install Complete, you can Switch the profile on Profile Manager\nDo you want close this windows?", "Susesfully",MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) Close();
            }else
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
}
