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
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public GoogleAuth()
        {
            InitializeComponent();
        }


        private async Task Auth()
        {
            if (!File.Exists(credPath))
            {
                using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    // Si no existe, solicita autorización
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
        {    // Verifica si el archivo de tokens existe
            if (File.Exists(credPath))
            {
                using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    // Carga el token desde el archivo utilizando el nombre de la máquina como clave
                    var token = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    machineName,  // Usamos el nombre de la máquina aquí
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                    );
                    credential = token;
                    Console.WriteLine("Token cargado exitosamente para la máquina: " + machineName);
                }
                tokenInitialice = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Auth();
        }

        private void GoogleAuth_Load(object sender, EventArgs e)
        {
            LoadToken();
            if (tokenInitialice) { button1.Hide(); CreateService(); }
        }
    }
}

