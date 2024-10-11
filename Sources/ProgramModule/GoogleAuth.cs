using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public partial class GoogleAuth : ReaLTaiizor.Forms.LostForm
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "autosplittercore";
        bool tokenInitialice = false;

        string credPath = "token.json";
        string machineName = Environment.MachineName;
        static DriveService driveAdapter = null;
        UserCredential credential;

        string secretName = "AutoSplitterCore/GoogleAuth/credentials";
        string region = "sa-east-1";

        public GoogleAuth()
        {
            InitializeComponent();
        }

        // Función para obtener las credenciales desde AWS Secrets Manager
        private async Task<string> GetGoogleCredentialsFromSecretsManager()
        {
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(Amazon.RegionEndpoint.GetBySystemName(region));

            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            GetSecretValueResponse response;
            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el secreto: " + ex.Message);
                throw;
            }

            // El secreto está en formato JSON en response.SecretString
            return response.SecretString;
        }

        private async Task Auth()
        {
            if (!File.Exists(credPath))
            {
                // Obtener las credenciales desde AWS Secrets Manager
                string googleCredentialsJson = await GetGoogleCredentialsFromSecretsManager();

                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(googleCredentialsJson)))
                {
                    // Autenticar con las credenciales obtenidas
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        machineName,  // Usamos el nombre de la máquina aquí
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                    );

                    Console.WriteLine("Autenticación completada para la máquina: " + machineName + ". Token guardado en: " + credPath);
                    tokenInitialice = true;
                    CreateService();
                }
            }
        }

        private void CreateService()
        {
            // Crea el servicio de Google Drive
            driveAdapter = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private async Task LoadToken()
        {
            if (File.Exists(credPath))
            {
                string googleCredentialsJson = await GetGoogleCredentialsFromSecretsManager();

                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(googleCredentialsJson)))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        machineName,
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                    );
                    Console.WriteLine("Token cargado exitosamente para la máquina: " + machineName);
                }
                tokenInitialice = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Auth();
        }

        private async Task LoadProcedure()
        {
            await LoadToken();
            if (tokenInitialice)
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

