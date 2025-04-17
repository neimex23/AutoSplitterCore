$subjectName = "CN=ASC_Cert"

# Remove from Personal Store (CurrentUser\My)
$myCerts = Get-ChildItem -Path "Cert:\CurrentUser\My" | Where-Object { $_.Subject -eq $subjectName }

if ($myCerts.Count -eq 0) {
    Write-Host "ℹ️ No certificates found in Personal Store with subject '$subjectName'."
} else {
    foreach ($cert in $myCerts) {
        Write-Host "🗑️ Removing from Personal Store: $($cert.Thumbprint)"
        Remove-Item -Path "Cert:\CurrentUser\My\$($cert.Thumbprint)" -Force
    }
    Write-Host "✅ All matching certificates removed from Personal Store."
}

# Remove from Trusted Root Store (CurrentUser\Root)
$rootCerts = Get-ChildItem -Path "Cert:\CurrentUser\Root" | Where-Object { $_.Subject -eq $subjectName }

if ($rootCerts.Count -eq 0) {
    Write-Host "ℹ️ No certificates found in Trusted Root Store with subject '$subjectName'."
} else {
    foreach ($cert in $rootCerts) {
        Write-Host "🗑️ Removing from Trusted Root Store: $($cert.Thumbprint)"
        Remove-Item -Path "Cert:\CurrentUser\Root\$($cert.Thumbprint)" -Force
    }
    Write-Host "✅ All matching certificates removed from Trusted Root Store."
}

pause