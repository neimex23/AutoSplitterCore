@echo off
Color 1F
echo ======== AutoSpliterCore Update Script ========
echo                      v1.3
echo ============================================
echo Warning: This script must not be run individually, 
echo you must download the files from the program first 
echo ============================================
rem

if %1 equ 1 (
     echo ========= ASC_Installer Function =========
    echo  ========= Stage 1: Finishing Proccess  =========
    rem     
    taskkill /im "HitCounterManager.exe" /f >nul 2>nul
    taskkill /im "AutoSplitterCore.exe" /f >nul 2>nul
    TIMEOUT 3
    rem
    echo  ========= Stage 2: Updating AutoSpliterCore_Installer  =========
    start /wait UpdateASCInstaller.msi
    rem
    echo ========= Stage 3: Cleaning =========
    del .\UpdateASCInstaller.msi
    echo All junk items were removed
    echo ===========================================  
	
) else if %1 equ 2 (

    echo ========= ASC_Portable Function =========
    echo  ========= Stage 1: Finishing Proccess  =========
    rem     
    taskkill /im "HitCounterManager.exe" /f >nul 2>nul
    taskkill /im "AutoSplitterCore.exe" /f >nul 2>nul
    TIMEOUT 3
    rem
    echo  ========= Stage 2: Updating AutoSpliterCore_Portable  =========
    xcopy /y /e .\Update\ .\
    rem     
    echo ========= Stage 3: Cleaning =========
    rmdir /S /Q .\Update
    echo All junk items were removed
    echo ============================================
	
) else if %1 equ 3 (

    echo ========= SoulMemory Function =========
    echo ========= Stage 1: Finishing Proccess =========
    rem  
    taskkill /im "HitCounterManager.exe" /f >nul 2>nul
    taskkill /im "AutoSplitterCore.exe" /f >nul 2>nul
    TIMEOUT 3
    rem
    echo ========= Stage 2: Updating Dll =========
    xcopy ".\Update\SoulMemory.dll" ".\" /Y
    xcopy ".\Update\soulmods_x64.dll" ".\" /Y
    xcopy ".\Update\soulmods_x86.dll" ".\" /Y

    rem     
    echo ========= Stage 3: Cleaning =========
    rmdir /S /Q .\Update
    echo All junk items were removed
    echo ============================================
)

pause

@echo off
if "%1"=="1" (
    goto RunApp
) else if "%1"=="2" (
    goto RunApp
) else if "%1"=="3" (
    goto RunApp
) else (
    exit /b
)

:RunApp
@echo on
echo ========= Starting Application =========
if exist "HitCounterManager.exe" (
    powershell -Command "Start-Process 'HitCounterManager.exe' -Verb RunAs"
) else if exist "AutoSplitterCore.exe" (
    powershell -Command "Start-Process 'AutoSplitterCore.exe' -Verb RunAs"
) else ( 
    echo HitCounterManager.exe or AutoSplitterCore.exe not found
    pause
)