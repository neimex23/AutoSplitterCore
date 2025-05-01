@echo off

REM MIT License

REM Copyright(c) 2022-2025 Ezequiel Medina - Neimex23
REM Based on "PrepareRelease.bat" by Peter Kirmeier

REM Permission Is hereby granted, free Of charge, to any person obtaining a copy
REM of this software And associated documentation files (the "Software"), to deal
REM in the Software without restriction, including without limitation the rights
REM to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
REM copies of the Software, And to permit persons to whom the Software Is
REM furnished to do so, subject to the following conditions:

REM The above copyright notice And this permission notice shall be included In all
REM copies Or substantial portions of the Software.

REM THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
REM IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
REM FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
REM AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
REM LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
REM OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
REM SOFTWARE.

echo ====================== PrepareReleaseAutoSplitter.bat START ======================

REM =========== Configuration ===========
setlocal
set SEVENZIP_PATH="C:\Program Files\7-Zip\7z.exe"
set SCRIPT_DIR=%~dp0
set PR_FINAL=%SCRIPT_DIR%FinalFiles

REM AdvancedInstaller Path
set AI_COM_FILE=C:\Program Files (x86)\Caphyon\Advanced Installer 22.2\bin\x86\advinst.exe 
set AI_COM_PATH="%AI_COM_FILE%"
set AI_PROJECT=%SCRIPT_DIR%AutoSplitterCoreSetup.aip

REM =========== Create output folder ===========
if not exist "%PR_FINAL%" mkdir "%PR_FINAL%"

REM =========== Delete PDBs ===========
echo Deleting .pdb files from bin\Release and bin\ReleaseHCMv2...
del /q "%SCRIPT_DIR%bin\Release\*.pdb" 2>nul
del /q "%SCRIPT_DIR%bin\ReleaseHCMv2\*.pdb" 2>nul

REM =========== Sign DLLs ===========
echo Signing DLLs...
powershell.exe -ExecutionPolicy Bypass -File "%~dp0SignAutoSplitterCore.ps1" -NoPause

REM =========== Build Installer ===========
if exist "%AI_COM_FILE%" (
    echo Building AutoSplitterCore installer...
    %AI_COM_PATH% /build "%AI_PROJECT%"
) else (
    echo WARNING: Advanced Installer not found or not Enterprise version. Skipping installer build.
)

REM =========== Package Portable Version (HCMv1) ===========
echo Packaging portable version (HCMv1)...
set PR_BASE=%SCRIPT_DIR%bin\Release
set PR_TARGET=%PR_FINAL%\AutoSplitterCorePortable
set PR_OUTPUT=%PR_FINAL%\AutoSplitterCore_Portable_v3.x.0.zip

rmdir /S /Q "%PR_TARGET%" 2>nul
mkdir "%PR_TARGET%"
xcopy "%PR_BASE%\*" "%PR_TARGET%" /E /I /Y >nul

REM Copy profiles
mkdir "%PR_TARGET%\AutoSplitterProfiles"
xcopy "%SCRIPT_DIR%AutoSplitterProfiles\*.*" "%PR_TARGET%\AutoSplitterProfiles\" /E /I /Y >nul
xcopy "%SCRIPT_DIR%AutoSplitterProfiles\ProfilesForHCM\*.*" "%PR_TARGET%\AutoSplitterProfiles\ProfilesForHCM\" /E /I /Y >nul

REM Create ZIP
%SEVENZIP_PATH% a "%PR_OUTPUT%" "%PR_TARGET%\*" >nul

REM =========== Package Portable Version (HCMv2) ===========
echo Packaging portable version (HCMv2)...
set PR_BASE=%SCRIPT_DIR%bin\ReleaseHCMv2
set PR_TARGET=%PR_FINAL%\AutoSplitterCorePortableV2
set PR_OUTPUT=%PR_FINAL%\AutoSplitterCore_Portable_HCMv2_v3.x.0.zip

rmdir /S /Q "%PR_TARGET%" 2>nul
mkdir "%PR_TARGET%"
xcopy "%PR_BASE%\*" "%PR_TARGET%" /E /I /Y >nul

REM Copy profiles
mkdir "%PR_TARGET%\AutoSplitterProfiles"
xcopy "%SCRIPT_DIR%AutoSplitterProfiles\*.*" "%PR_TARGET%\AutoSplitterProfiles\" /E /I /Y >nul
xcopy "%SCRIPT_DIR%AutoSplitterProfiles\ProfilesForHCM\*.*" "%PR_TARGET%\AutoSplitterProfiles\ProfilesForHCM\" /E /I /Y >nul

REM Create ZIP
%SEVENZIP_PATH% a "%PR_OUTPUT%" "%PR_TARGET%\*" >nul

REM =========== Copy Final Installer ===========
echo Copying compiled installer...
copy "%SCRIPT_DIR%bin\AutoSplitterCoreInstaller\AutoSplitterCore_Installer_v3.x.0.exe" "%PR_FINAL%\" >nul

echo Done. DLLs signed, installer built, and portable packages created.
echo ====================== PrepareReleaseAutoSplitter.bat END ========================
endlocal