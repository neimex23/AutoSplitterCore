param(
    [switch]$Pause
)

# ================= CONFIG ===================
$certName = "ASC_Cert"
$subject = "CN=$certName"
$scriptRoot = $PSScriptRoot

$dllsToSign = @(
    (Join-Path $scriptRoot "bin\Release\AutoSplitterCore.dll"),
    (Join-Path $scriptRoot "bin\ReleaseHCMv2\AutoSplitterCore.dll")
)

$signtool = "C:\Program Files (x86)\Windows Kits\10\bin\10.0.26100.0\x64\signtool.exe"
$exportPath = Join-Path $scriptRoot "ASC_Cert.cer"
# ============================================

Write-Host "Looking for existing certificate '$subject'..."

# Search for existing cert (must be currently valid: not expired and already active)
$now = Get-Date
$cert = Get-ChildItem -Path "Cert:\CurrentUser\My" |
    Where-Object { $_.Subject -eq $subject -and $_.NotAfter -gt $now -and $_.NotBefore -le $now } |
    Sort-Object NotAfter -Descending |
    Select-Object -First 1
$certJustCreated = $false

# Remove any expired certs with the same subject so they don't linger in the store
Get-ChildItem -Path "Cert:\CurrentUser\My" |
    Where-Object { $_.Subject -eq $subject -and $_.NotAfter -le $now } |
    ForEach-Object {
        Write-Host "Removing expired certificate (Thumbprint: $($_.Thumbprint), expired: $($_.NotAfter))..."
        Remove-Item -Path $_.PSPath -Force
    }

if ($cert -ne $null) {
    Write-Host "Existing certificate found with Thumbprint: $($cert.Thumbprint)"
} else {
    Write-Host "Creating new code signing certificate '$certName'..."

    $cert = New-SelfSignedCertificate `
        -Type Custom `
        -Subject $subject `
        -KeyUsage DigitalSignature `
        -KeyExportPolicy Exportable `
        -KeyAlgorithm RSA `
        -KeyLength 2048 `
        -CertStoreLocation "Cert:\CurrentUser\My" `
        -NotAfter (Get-Date).AddYears(10) `
        -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3") # EKU: Code Signing

    if ($cert -eq $null) {
        Write-Host "Failed to create certificate." -ForegroundColor Red
        exit 1
    }

    Write-Host "New certificate created with Thumbprint: $($cert.Thumbprint)"
    $certJustCreated = $true
}

$thumbprint = $cert.Thumbprint.Trim()

# Ensure .cer file exists
if ($certJustCreated -or !(Test-Path $exportPath)) {
    Write-Host "Exporting certificate to $exportPath..."
    Export-Certificate -Cert $cert -FilePath $exportPath -Force
    if (Test-Path $exportPath) {
        Write-Host "Certificate exported as $exportPath"
    } else {
        Write-Host "Failed to export certificate." -ForegroundColor Red
        exit 1
    }
}

# Verify SignTool
if (!(Test-Path $signtool)) {
    Write-Host "SignTool not found at: $signtool" -ForegroundColor Red
    exit 1
}

# Sign all DLLs
foreach ($dll in $dllsToSign) {
    if (Test-Path $dll) {
        Write-Host "Signing $dll..."
        & "$signtool" sign /sha1 "$thumbprint" /s My /tr http://timestamp.digicert.com /td sha256 /fd sha256 "$dll"
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Signing failed: $dll" -ForegroundColor Red
        } else {
            Write-Host "Signed successfully: $dll" -ForegroundColor Green

            # Copy the .cer file next to the DLL
            $dllDirectory = Split-Path $dll -Parent
            $targetCertPath = Join-Path $dllDirectory (Split-Path $exportPath -Leaf)
            Copy-Item -Path $exportPath -Destination $targetCertPath -Force
            if (Test-Path $targetCertPath) {
                Write-Host "Certificate copied to $targetCertPath"
            } else {
                Write-Host "Failed to copy certificate to $dllDirectory" -ForegroundColor Red
            }
        }
    } else {
        Write-Host "DLL not found: $dll" -ForegroundColor Yellow
    }
}

if ($Pause) {
    Write-Host "Press any key to continue..."
    $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')
}
