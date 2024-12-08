//MIT License

//Copyright (c) 2022-2024 Ezequiel Medina

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
using Google.Apis.Util.Store;
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

namespace AutoSplitterCore
{
    public partial class GoogleAuth : ReaLTaiizor.Forms.LostForm
    {
        static string[] Scopes = {"https://www.googleapis.com/auth/userinfo.email", DriveService.Scope.Drive };
        static string ApplicationName = "autosplittercore";

        string machineName = Environment.MachineName; 
        static DriveService driveService = null;
        static Oauth2Service oauth2Service = null;
        UserCredential credential;

        SaveModule saveModule;
        string currentProfilePath;

        public GoogleAuth(SaveModule saveModule, string currentProfilePath)
        {
            InitializeComponent();
            this.saveModule = saveModule;
            this.currentProfilePath = currentProfilePath;
        }

        #region AuthProcess
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Task.Run(() => Auth());
            Cursor = Cursors.Default;
        }

        private void btnForgetLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var dataStore = new EncryptedDataStore("Google.Apis.Auth");

                dataStore.Clear(); // Remove all credentials

                Console.WriteLine("Logout completed. All stored credentials have been cleared.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during logout process: " + ex.Message);
            }
        }

        private async Task Auth()
        {
            try
            {
                string googleCredentialsJson = await GetGoogleCredentials();
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(googleCredentialsJson)))
                {
                    // Authenticate with the obtained credentials
                    var dataStore = new EncryptedDataStore("Google.Apis.Auth");

                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        machineName,
                        CancellationToken.None,
                        new FileDataStoreAdapter(dataStore)
                    );


                    Console.WriteLine("Authentication completed for ´user´ machine: " + machineName);
                    CreateService();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error on Authentification process: " + e.Message);
            }
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
            // Create the Google Drive service
            driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            oauth2Service = new Oauth2Service(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            Userinfo userInfo = oauth2Service.Userinfo.Get().Execute();
            string email = userInfo.Email;       
            linkLabel1.Invoke(new Action(() =>
            {
                linkLabel1.Text = email;
                AfterLoginEvents();
            }));       
        }
        #endregion

        private void AfterLoginEvents()
        {
            btnLogin.Enabled = false;
            btnForgetLogin.Enabled = true;
            LoadFilesFromPublicFolder();

            textBoxCurrrentProfile.Text = saveModule.GetProfileName();
            textBoxAuthor.Text = saveModule.GetAuthor();
            TextboxDescription.Text = saveModule.GetDescription();
            textBoxDate.Text = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void LoadFilesFromPublicFolder()
        {
            string folderId = "16Y9MeL_Zbi5NgfTbBvbG-7JzGpPkCEqV";
            var request = driveService.Files.List();
            request.Q = $"'{folderId}' in parents and name contains '.xml'";
            request.Fields = "files(id, name)";

            var result = request.Execute();
            foreach (var file in result.Files)
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(file.Id);
                listViewFiles.Items.Add(item);
                listViewFiles.Refresh();
                Console.WriteLine($"Name: {file.Name}, ID: {file.Id}");
            }
        }

        private void btnUploadProfile_Click(object sender, EventArgs e)
        {
            if (saveModule.GetProfileName() == "Default" || saveModule.GetAuthor() == "Owner" || saveModule.GetDescription() == "Default Profile") { 
                MessageBox.Show("You must use a profile name, author and description different from default", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsFileNameRepeated(saveModule.GetProfileName())) { MessageBox.Show("ProfileName alrady exist, please use another.", "Error", MessageBoxButtons.OK); return; }

            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = Path.GetFileName(currentProfilePath),
                Description = saveModule.GetDescription(),
                Properties = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "author", saveModule.GetAuthor() },
                    { "date", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") }
                }
            };

            using (var stream = new FileStream(currentProfilePath, FileMode.Open))
            {
                var request = driveService.Files.Create(fileMetadata, stream, "application/xml");
                request.Fields = "id";
                var file = request.Upload();

                if (file.Status == Google.Apis.Upload.UploadStatus.Failed)
                {
                    throw new Exception($"Error to upload file: {file.Exception.Message}");
                }
            }

        }

        public bool IsFileNameRepeated(string fileName)
        {
            try
            {
                var request = driveService.Files.List();
                request.Q = $"name = '{fileName}' and trashed = false";
                request.Fields = "files(id, name)";

                var result = request.Execute();

                return result.Files.Count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        private void GoogleAuth_Load(object sender, EventArgs e)
        {

        }
    }
}
