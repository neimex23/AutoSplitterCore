using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AutoSplitterCore
{
    public class CertificateInstaller
    {
        const string CertSubject = "CN=ASC_Cert";
        const string CertFilePath = "ASC_Cert.cer";

        public static void EnsureCertificateTrusted()
        {
            using (var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadWrite);

                // Verificar si ya está instalado
                var existingCertificate = store.Certificates.Cast<X509Certificate2>()
                    .FirstOrDefault(existingCert => existingCert.Subject == CertSubject);

                if (existingCertificate != null)
                {
                    DebugLog.LogMessage("Certificate already installed.");
                    return;
                }

                // Instalar desde archivo .cer
                if (!File.Exists(CertFilePath))
                {
                    DebugLog.LogMessage($"Certificate file not found: {CertFilePath}");
                    return;
                }

                var certBytes = File.ReadAllBytes(CertFilePath);
                var cert = new X509Certificate2(certBytes);

                store.Add(cert);
                DebugLog.LogMessage("Certificate installed to Trusted Root CA.");
            }
        }
    }
}
