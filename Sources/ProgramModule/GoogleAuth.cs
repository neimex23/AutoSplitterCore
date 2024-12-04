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
        static string[] Scopes = { DriveService.Scope.Drive, "https://www.googleapis.com/auth/userinfo.email" };
        static string ApplicationName = "autosplittercore";

        string machineName = Environment.MachineName; //Sensitive Information and predicted think with other solution
        static DriveService driveService = null;
        static Oauth2Service oauth2Service = null;
        UserCredential credential;

        public GoogleAuth()
        {
            InitializeComponent();
        }

        #region AuthProcess
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Auth();
        }

        private async Task Auth()
        {
            try
            {
                string googleCredentialsJson = await GetGoogleCredentials();
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(googleCredentialsJson)))
                {
                    // Authenticate with the obtained credentials
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        machineName,
                        CancellationToken.None,
                        new FileDataStore("")
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
            linkLabel1.Text = userInfo.Email;
            btnLogin.Hide();
            LoadFilesFromPublicFolder();
        }
        #endregion


        public async Task<string> DownloadFileFromPublicUrl(string fileId)
        {
            try
            {
                string url = $"https://www.googleapis.com/drive/v3/files/{fileId}?alt=media&key={GetGoogleCredentials()}";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Leer el contenido del archivo
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading file: " + ex.Message);
                return string.Empty;
            }
        }

        private async void LoadFilesFromPublicFolder()
        {
            string folderId = "16Y9MeL_Zbi5NgfTbBvbG-7JzGpPkCEqV";
            var request = driveService.Files.List();
            request.Q = $"'{folderId}' in parents and mimeType='application/xml'";
            request.Fields = "files(id, name)";

            var result = request.Execute();
            foreach (var file in result.Files)
            {
                ListViewItem item = new ListViewItem(file.OriginalFilename);
                listViewFiles.Items.Add(item);
                Console.WriteLine($"Name: {file.Name}, ID: {file.Id}");
            }
        }
    }
}
