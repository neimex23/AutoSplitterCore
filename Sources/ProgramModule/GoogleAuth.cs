using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Cloud.SecretManager.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public partial class GoogleAuth : ReaLTaiizor.Forms.LostForm
    {
        private static string clientId = string.Empty;
        private static string clientSecret = string.Empty;
        public GoogleAuth()
        {
            InitializeComponent();
        }

        private void GoogleAuth_Load(object sender, EventArgs e)
        {

        }

        public static async Task GetSecretsAsync()
        {
            var projectId = "autosplittercore"; // Reemplaza con tu ID de proyecto en Google Cloud
            var client = SecretManagerServiceClient.Create();

            // Acceder al secreto del clientId
            var clientIdSecret = await client.AccessSecretVersionAsync(new SecretVersionName(projectId, "client-id", "latest"));
            // Acceder al secreto del clientSecret
            var clientSecretSecret = await client.AccessSecretVersionAsync(new SecretVersionName(projectId, "client-secret", "latest"));

            // Convertir los valores de secretos a texto
            clientId = clientIdSecret.Payload.Data.ToStringUtf8();
            clientSecret = clientSecretSecret.Payload.Data.ToStringUtf8();
        }

        // Método para autenticar al usuario en Google Drive
        public static async Task<DriveService> AuthenticateAsync()
        {
            UserCredential credential;
            string credPath = "token.json"; // Ruta donde se almacenará el token de acceso

            // Intentar reutilizar el token existente
            if (File.Exists(credPath))
            {
                using (var stream = new FileStream(credPath, FileMode.Open, FileAccess.Read))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        new[] { DriveService.Scope.DriveFile },
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                    );
                }
            }
            else
            {
                // Si no existe el token.json, crear nuevas credenciales OAuth2
                var secrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                };

                // Pedir al usuario que inicie sesión mediante OAuth2
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets,
                    new[] { DriveService.Scope.DriveFile },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                );
            }

            // Crear el servicio de Google Drive utilizando las credenciales obtenidas
            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "autosplittercore",
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        private async void Initialize()
        {
            try
            {
                await GoogleAuth.GetSecretsAsync();
                var service = await GoogleAuth.AuthenticateAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}

