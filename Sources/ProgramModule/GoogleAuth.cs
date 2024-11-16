using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
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

        private void GoogleAuth_Load(object sender, EventArgs e)
        {
            LoadProcedure();
        }

        private async Task LoadProcedure()
        {
            LoadToken();
            if (tokenInitialized)
            {
                button1.Hide();
                CreateService();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Auth();
        }

        #region Encrypt Files

        public static string GetEncryptKey()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("AutoSplitterCore.appsettings.json"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonContent = reader.ReadToEnd();
                    var jsonObject = JObject.Parse(jsonContent);
                    return jsonObject["EncryptFiles"]["EncryptKey"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error on Incrusted Resource: ", ex);
            }
        }


        private static readonly string encryptionKey = GetEncryptKey();

            private static string Encrypt(string plainText)
            {
                using (Aes aes = Aes.Create())
                {
                    var key = Encoding.UTF8.GetBytes(encryptionKey.PadRight(32, '0'));
                    aes.Key = key;
                    aes.GenerateIV();
                    var iv = Convert.ToBase64String(aes.IV);
                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        var plainBytes = Encoding.UTF8.GetBytes(plainText);
                        var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                        return iv + ":" + Convert.ToBase64String(encryptedBytes);
                    }
                }
            }

            private static string Decrypt(string encryptedText)
            {
                var parts = encryptedText.Split(':');
                var iv = Convert.FromBase64String(parts[0]);
                var cipherText = Convert.FromBase64String(parts[1]);
                using (Aes aes = Aes.Create())
                {
                    var key = Encoding.UTF8.GetBytes(encryptionKey.PadRight(32, '0'));
                    aes.Key = key;
                    aes.IV = iv;
                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        var plainBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                        return Encoding.UTF8.GetString(plainBytes);
                    }
                }
            }


        #endregion

        private static readonly HttpClient client = new HttpClient();

        private void CreateService()
        {
            // Create the Google Drive service
            driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
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
                            "user", 
                            CancellationToken.None,
                            new FileDataStore("")
                        );

                        using (var stream2 = new MemoryStream())
                        {
                            new FileDataStore("").StoreAsync("StoredCredential", credential.Token).Wait();
                            stream2.Seek(0, SeekOrigin.Begin);
                            var tokenBytes = stream2.ToArray();

                            // encrypt and save credentials token on file
                            var encryptedToken = Encrypt(Convert.ToBase64String(tokenBytes));
                            File.WriteAllText(credPath, encryptedToken);
                        }


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

        private async Task LoadToken()
        {
            if (File.Exists(credPath))
            {
                try
                {
                    string googleCredentialsJson = await GetGoogleCredentials();
                    // Descifrar y deserializar el token
                    var encryptedToken = File.ReadAllText(credPath);
                    var decryptedTokenJson = Decrypt(encryptedToken);
                    var token = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(decryptedTokenJson);
                   
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(googleCredentialsJson)))
                    {
                        // Crear el flujo de autorización manualmente
                        var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                        {
                            ClientSecrets = GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes = Scopes,
                        DataStore = new FileDataStore("")
                        });

                        // Reconstruir las credenciales del usuario
                        credential = new UserCredential(flow, "user", token);

                        Console.WriteLine("Token loaded successfully for machine: " + machineName);
                        tokenInitialized = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error during token loading process: " + e.Message);
                }
            }
        }


     
    }
}
