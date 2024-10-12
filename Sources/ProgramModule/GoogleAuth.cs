using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public partial class GoogleAuth : ReaLTaiizor.Forms.LostForm
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "autosplittercore";
        bool tokenInitialized = false;

        string credPath = "token.json"; // Encrypt final token to protect account security
        string machineName = Environment.MachineName; //Sensitive Information and predicted think with other solution
        static DriveService driveService = null;
        UserCredential credential;

        public GoogleAuth()
        {
            InitializeComponent();
        }

        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetGoogleCredentials()
        {
            // API Gateway URL
            string url = "Reserved until implementing more security";

            try
            {
                // Create content for the POST request (if necessary)
                var content = new StringContent("", Encoding.UTF8, "application/json"); // You can add data if your API requires it

                // Make a POST request to the API Gateway
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                // Get the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Remove the undesired initial part
                if (responseBody.StartsWith("{\"AutoSplitterCoreAuth\":"))
                {
                    // Get the value between the quotes and the closing of the object
                    string value = responseBody.Substring(responseBody.IndexOf(':') + 1);
                    // Remove the closing of the JSON object
                    if (value.EndsWith("}"))
                    {
                        value = value.Substring(0, value.Length - 1).Trim();
                    }
                    value = value.Replace("\\\"", "\"");
                    return value.Trim('\"').Trim(); // Remove surrounding quotes and blank space
                }

                return responseBody; // If it's not what you expected, return the complete body
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error making the request: " + e.Message);
                throw;
            }
        }

        private async Task Auth()
        {
            if (!File.Exists(credPath))
            {
                // Obtain credentials from AWS Secrets Manager
                string googleCredentialsJson = await GetGoogleCredentials();   

                try
                {
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(googleCredentialsJson)))
                    {
                        // Authenticate with the obtained credentials
                        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.FromStream(stream).Secrets,
                            Scopes,
                            machineName,  // Use the machine name here
                            CancellationToken.None,
                            new FileDataStore(credPath, true)
                        );

                        Console.WriteLine("Authentication completed for machine: " + machineName + ". Token saved in: " + credPath);
                        tokenInitialized = true;
                        CreateService();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error on Authentification process: " + e.Message);
                }
            }
        }

        private void CreateService()
        {
            // Create the Google Drive service
            driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private async Task LoadToken()
        {
            if (File.Exists(credPath))
            {
                string googleCredentialsJson = await GetGoogleCredentials();

                try
                {
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(googleCredentialsJson)))
                    {
                        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.FromStream(stream).Secrets,
                            Scopes,
                            machineName,
                            CancellationToken.None,
                            new FileDataStore(credPath, true)
                        );
                        Console.WriteLine("Token loaded successfully for machine: " + machineName);
                    }
                    tokenInitialized = true;
                }catch (Exception e)
                {
                    Console.WriteLine("Error on LoadToken process: " + e.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Auth();
        }

        private async Task LoadProcedure()
        {
            await LoadToken();
            if (tokenInitialized)
            {
                button1.Hide();
                CreateService();
            }
        }

        private void GoogleAuth_Load(object sender, EventArgs e)
        {
            LoadProcedure();
        }
    }
}
