# ================= CONFIG ===================
$certName = "Neimex23"
$dllToSign = "bin\Release\AutoSplitterCore.dll"
$signtool = "C:\Program Files (x86)\Windows Kits\10\bin\10.0.26100.0\x64\signtool.exe"
# ============================================

Write-Host "🛠️ Creating code signing certificate '$certName'..."

# Crear certificado con clave exportable y propósito explícito de firma de código
$cert = New-SelfSignedCertificate `
    -Type Custom `
    -Subject "CN=$certName" `
    -KeyUsage DigitalSignature `
    -KeyExportPolicy Exportable `
    -KeyAlgorithm RSA `
    -KeyLength 2048 `
    -CertStoreLocation "Cert:\CurrentUser\My" `
    -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3") `  # EKU: Code Signing

if ($cert -eq $null) {
    Write-Host "❌ Failed to create certificate." -ForegroundColor Red
    exit 1
}

$thumbprint = $cert.Thumbprint.Trim()
Write-Host "📜 Certificate created with Thumbprint: $thumbprint"

# Verificar signtool
if (!(Test-Path $signtool)) {
    Write-Host "❌ SignTool not found at: $signtool" -ForegroundColor Red
    exit 1
}

# Firmar usando Thumbprint del certificado
Write-Host "🔏 Signing $dllToSign..."
& "$signtool" sign /sha1 "$thumbprint" /s My /tr http://timestamp.digicert.com /td sha256 /fd sha256 "$dllToSign"

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Signing failed." -ForegroundColor Red
} else {
    Write-Host "✅ DLL signed successfully." -ForegroundColor Green
}

pause
