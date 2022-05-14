@echo off
echo *****************************************************************************
echo * Windows Live OneCare Rewritten Setup                                      *
echo *****************************************************************************
echo *                                                                           *
echo *  Finalising your installation. Please wait and do not close this          *
echo *  window.                                                                  *
echo *                                                                           *
echo *  We'll notify you once OneCare is ready for you.                          *
echo *                                                                           *
echo *****************************************************************************
echo *  Installing packages now. Do not close this window.                       *
echo *****************************************************************************
mkdir "C:\Program Files\Microsoft Windows OneCare Live"
mkdir "C:\Program Files\Microsoft Windows OneCare Live\Logs"
echo *****************************************************************************
echo *  Installing MP_AVBits                                                     *
echo *****************************************************************************
CALL C:\Windows\system32\msiexec.exe /i "C:\Onecare\mp_AVBits.msi" /qn /l*v "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\AVBitsInstall.log" INSTALLDIR="%PROGRAMFILES%\Microsoft Windows OneCare Live\Antivirus" REBOOT="ReallySuppress"
echo *  Installing MPAM-FE                                                       *
echo *****************************************************************************
CALL C:\OneCare\mpam-fe.exe /q ONECARE
echo *  Installing Backup-PXEngine                                               *
echo *****************************************************************************
CALL C:\Windows\system32\msiexec.exe /i "C:\Onecare\PxEngine.msi" /qn /l*v "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\BackupInstall.log" ALLUSERS=1 ARPSYSTEMCOMPONENT=1 REBOOT="ReallySuppress"
echo *  Installing DrWatsonX86                                                   *
echo *****************************************************************************
CALL C:\Windows\system32\msiexec.exe /i "C:\Onecare\dw20shared.msi" /qn /l*v "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\WatsonInstall.log" REBOOT="ReallySuppress" APPGUID=D07A8E7E-D324-4945-BA8C-E532AD008FF3 REINSTALL=ALL REINSTALLMODE=vomus
echo *  Installing Firewall                                                      *
echo *****************************************************************************
CALL C:\Windows\system32\msiexec.exe /i "C:\Onecare\MPSSetup.msi" /qn /l*v "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\FWInstall.log" INSTALLDIR="%PROGRAMFILES%\Microsoft Windows OneCare Live\Firewall" REBOOT="ReallySuppress"
echo *  Installing Microsoft Core XML Services                                   *
echo *****************************************************************************
CALL C:\Windows\system32\msiexec.exe /i "C:\Onecare\msxml.msi" /qn /l*v "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\MSXMLInstall.log" INSTALLDIR="%PROGRAMFILES%\Microsoft Windows OneCare Live\Firewall" REBOOT="ReallySuppress"
echo *  Installing OneCare Resources                                             *
echo *****************************************************************************
CALL C:\Windows\system32\msiexec.exe /i "C:\Onecare\OCLocRes.msi" /qn /l*v "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\ResourceInstall.log" TARGETFOLDER="Microsoft Windows OneCare Live" ALLUSERS=1 ARPSYSTEMCOMPONENT=1 REBOOT="ReallySuppress"
echo *  Installing WinSS                                                         *
echo *****************************************************************************
CALL C:\Windows\system32\msiexec.exe /i "C:\Onecare\WinSS.msi" /qn /l*v "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\WinSSInstall.log" TARGETFOLDER="Microsoft Windows OneCare Live" REBOOT="ReallySuppress"
echo *  Installing Idcrl                                                         *
echo *****************************************************************************
CALL C:\Windows\system32\msiexec.exe /i "C:\Onecare\Idcrl.msi" /qn /l*v "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\IdcrlInstall.log" ALLUSERS=1 ARPSYSTEMCOMPONENT=1 TARGETDIR="%PROGRAMFILES%\Microsoft Windows OneCare Live" REBOOT="ReallySuppress"
echo *  Installing GTOneCare                                                     *
echo *****************************************************************************
CALL C:\Windows\system32\msiexec.exe /i "C:\Onecare\GTOneCare.msi" /qn SYSLANGID=en-us /l*v "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\GTOneCare.log" INSTALLDIR="%PROGRAMFILES%\Microsoft Windows OneCare Live\GTOneCare" REBOOT="ReallySuppress"
call NET START WINSS