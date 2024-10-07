using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
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
        public GoogleAuth()
        {
            InitializeComponent();
        }

        private void GoogleAuth_Load(object sender, EventArgs e)
        {

        }

        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "AutoSplitterCore";

        public static DriveService GetService()
        {
            UserCredential credential;
            string credFilePath = "credentials.json"; // Ruta para credenciales generales

            if (!File.Exists(credFilePath))
            {
                // Si no existe credentials.json, iniciar flujo interactivo para crear las credenciales
                Console.WriteLine("No se encontraron credenciales. Iniciando flujo de autenticación interactiva...");
                credential = GetInteractiveCredentials();
            }
            else
            {
                // Si credentials.json existe, utilizarlo para autenticar
                using (var stream = new FileStream(credFilePath, FileMode.Open, FileAccess.Read))
                {
                    string tokenPath = "token.json"; // Archivo de token genérico
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user", // Sin especificar un userId real
                        CancellationToken.None,
                        new FileDataStore(tokenPath, true)).Result;
                    Console.WriteLine("Token de autenticación guardado en: " + tokenPath);
                }
            }

            // Crear el servicio de Google Drive
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        // Método para obtener las credenciales mediante flujo interactivo
        private static UserCredential GetInteractiveCredentials()
        {
            Console.WriteLine("Iniciando autenticación OAuth interactiva...");

            // Aquí puedes usar GoogleWebAuthorizationBroker para crear las credenciales desde la interacción del usuario
            var clientSecrets = new ClientSecrets
            {
                ClientId = "TU_CLIENT_ID", // Debes ingresar tu ClientId aquí
                ClientSecret = "TU_CLIENT_SECRET" // Debes ingresar tu ClientSecret aquí
            };

            string tokenPath = "token.json"; // Archivo de token genérico

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                Scopes,
                "user", // Sin especificar un userId
                CancellationToken.None,
                new FileDataStore(tokenPath, true)).Result;

            Console.WriteLine("Autenticación completada. Token guardado en: " + tokenPath);
            return credential;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetService();
        }
    }
}

