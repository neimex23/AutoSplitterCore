# ================= CONFIG ===================
$certName = "ASC_Cert"
$subject = "CN=$certName"
$dllsToSign = @(
    "bin\Release\AutoSplitterCore.dll",
    "bin\ReleaseHCMv2\AutoSplitterCore.dll"
)
$signtool = "C:\Program Files (x86)\Windows Kits\10\bin\10.0.26100.0\x64\signtool.exe"
$exportPath = "ASC_Cert.cer"
# ============================================

Write-Host "🔍 Looking for existing certificate '$subject'..."

# Search for existing cert
$cert = Get-ChildItem -Path "Cert:\CurrentUser\My" | Where-Object { $_.Subject -eq $subject } | Select-Object -First 1
$certJustCreated = $false

if ($cert -ne $null) {
    Write-Host "✅ Existing certificate found with Thumbprint: $($cert.Thumbprint)"
} else {
    Write-Host "🛠️ Creating new code signing certificate '$certName'..."

    # Create new certificate
    $cert = New-SelfSignedCertificate `
        -Type Custom `
        -Subject $subject `
        -KeyUsage DigitalSignature `
        -KeyExportPolicy Exportable `
        -KeyAlgorithm RSA `
        -KeyLength 2048 `
        -CertStoreLocation "Cert:\CurrentUser\My" `
        -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3") # EKU: Code Signing

    if ($cert -eq $null) {
        Write-Host "❌ Failed to create certificate." -ForegroundColor Red
        exit 1
    }

    Write-Host "📜 New certificate created with Thumbprint: $($cert.Thumbprint)"
    $certJustCreated = $true
}

$thumbprint = $cert.Thumbprint.Trim()

# Export .cer if just created
if ($certJustCreated) {
    Write-Host "💾 Exporting certificate to $exportPath..."
    Export-Certificate -Cert $cert -FilePath $exportPath -Force
    if (Test-Path $exportPath) {
        Write-Host "✅ Certificate exported as $exportPath"
    } else {
        Write-Host "❌ Failed to export certificate." -ForegroundColor Red
    }
}

# Verify SignTool
if (!(Test-Path $signtool)) {
    Write-Host "❌ SignTool not found at: $signtool" -ForegroundColor Red
    exit 1
}

# Sign all DLLs
foreach ($dll in $dllsToSign) {
    if (Test-Path $dll) {
        Write-Host "🔏 Signing $dll..."
        & "$signtool" sign /sha1 "$thumbprint" /s My /tr http://timestamp.digicert.com /td sha256 /fd sha256 "$dll"
        if ($LASTEXITCODE -ne 0) {
            Write-Host "❌ Signing failed: $dll" -ForegroundColor Red
        } else {
            Write-Host "✅ Signed successfully: $dll" -ForegroundColor Green
        }
    } else {
        Write-Host "⚠️ DLL not found: $dll" -ForegroundColor Yellow
    }
}

pause
