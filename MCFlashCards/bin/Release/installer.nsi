; basic script template for NSIS installers
;
; Written by Philip Chu
; Copyright (c) 2004-2005 Technicat, LLC
;
; This software is provided 'as-is', without any express or implied warranty.
; In no event will the authors be held liable for any damages arising from the use of this software.
 
; Permission is granted to anyone to use this software for any purpose,
; including commercial applications, and to alter it ; and redistribute
; it freely, subject to the following restrictions:
 
;    1. The origin of this software must not be misrepresented; you must not claim that
;       you wrote the original software. If you use this software in a product, an
;       acknowledgment in the product documentation would be appreciated but is not required.
 
;    2. Altered source versions must be plainly marked as such, and must not be
;       misrepresented as being the original software.
 
;    3. This notice may not be removed or altered from any source distribution.

;-----------------------------------------
; Modified by Joe Dunne.  This version is much easier to use for multiple file installations.
;
;Note: To build this installer executable:
;First, install "Nullsoft installer" from: http://nsis.sourceforge.net/Download
;Then right click on this file (installer.nsi) and click "Compile NSIS Script". 

;-------------------------------
;-------------------------------
;User modifiable section:
;-------------------------------
;-------------------------------

;Indicate the required minimum .NET framework version:  DOT_MAJOR.DOT_MINOR.DOT_MINOR_MINOR  (Example: 2.0.50727 is .NET framework v2.0)
!define DOT_MAJOR "2"
!define DOT_MINOR "0"
!define DOT_MINOR_MINOR "50727"

;Executable file name:
!define setup "mcflashcards_setup.exe"
  
;Company name:
!define company "Joes Tools"
 
;Title to be displayed while installing:
!define prodname "Multiple choice FlashCards"

;Executable file that is being installed (shortcuts will automatically be made to it)
;Don't change this filename!!
!define exec "MCFlashCards.exe"

;Executable to run during setup:  (This can be one of the additional files below)
;This line is commented out. Don't bother modifying it.
;!define filetoexec "dooverMacro.exe"

;list of additional files (See install section and uninstall section to create install and uninstalls for these files).
!define	file1 "test.csv"
!define	file2 "test2.csv"
!define	file3 "par101ch2-3.csv"

; change this to wherever the files to be packaged reside
!define srcdir "."

;-------------------------------
;-------------------------------
;End user modifiable section
;-------------------------------
;-------------------------------

; optional stuff
 
; text file to open in notepad after installation
; !define notefile "README.txt"
 
; license text file
; !define licensefile license.txt
 
; icons must be Microsoft .ICO files
; !define icon "icon.ico"
 
; installer background screen
; !define screenimage background.bmp
 
;-------------------------------
; registry stuff
 
!define regkey "Software\${company}\${prodname}"
!define uninstkey "Software\Microsoft\Windows\CurrentVersion\Uninstall\${prodname}"
 
!define startmenu "$SMPROGRAMS\${company}\${prodname}"
!define uninstaller "uninstall.exe"
 
;--------------------------------
RequestExecutionLevel admin
XPStyle on
ShowInstDetails hide
ShowUninstDetails hide
 
Name "${prodname}"
Caption "${prodname}"
 
!ifdef icon
Icon "${icon}"
!endif
 
OutFile "${setup}"
 
SetDateSave on
SetDatablockOptimize on
CRCCheck on
SilentInstall normal
 
InstallDir "$PROGRAMFILES\${company}\${prodname}"
InstallDirRegKey HKLM "${regkey}" ""
 
!ifdef licensefile
LicenseText "License"
LicenseData "${srcdir}\${licensefile}"
!endif
 
; pages
; we keep it simple - leave out selectable installation types
 
!ifdef licensefile
Page license
!endif
 
; Page components
Page directory
Page instfiles
 
UninstPage uninstConfirm
UninstPage instfiles
 
;--------------------------------
 
AutoCloseWindow false
ShowInstDetails show
 
 
Function .onInit
	;Check if user is an administrator:
	System::Call "kernel32::GetModuleHandle(t 'shell32.dll') i .s"
	System::Call "kernel32::GetProcAddress(i s, i 680) i .r0"
	System::Call "::$0() i .r0"

	;Pop $0
	; $0 = 1 if the user belongs to the administrator's group
	; $0 = 0 if not

	;IntCmp: if $0 == 0, go if equal, go if less, go if greater
	IntCmp $0 0 isNotAdmin isNotAdmin isAdmin

isNotAdmin:
	MessageBox MB_OK 'You Must be logged in as Administrator!'
	Quit ;

isAdmin:

;	MessageBox MB_OK 'User is an Administrator!'

;Exit if .NET framework 2.0 or higher is not installed:
	Call IsDotNetInstalledAdv

FunctionEnd


!ifdef screenimage
; set up background image
; uses BgImage plugin
 
Function .onGUIInit
	; extract background BMP into temp plugin directory
	InitPluginsDir
	File /oname=$PLUGINSDIR\1.bmp "${screenimage}"
 
	BgImage::SetBg /NOUNLOAD /FILLSCREEN $PLUGINSDIR\1.bmp
	BgImage::Redraw /NOUNLOAD
FunctionEnd
 
Function .onGUIEnd
	; Destroy must not have /NOUNLOAD so NSIS will be able to unload and delete BgImage before it exits
	BgImage::Destroy
FunctionEnd
 
!endif
 
;--------------------------------------------------------------
; beginning (invisible) section
Section
 
  WriteRegStr HKLM "${regkey}" "Install_Dir" "$INSTDIR"
  ; write uninstall strings
  WriteRegStr HKLM "${uninstkey}" "DisplayName" "${prodname} (remove only)"
  WriteRegStr HKLM "${uninstkey}" "UninstallString" '"$INSTDIR\${uninstaller}"'
 
!ifdef filetype
  WriteRegStr HKCR "${filetype}" "" "${prodname}"
!endif
 
  WriteRegStr HKCR "${prodname}\Shell\open\command\" "" '"$INSTDIR\${exec} "%1"'
 
!ifdef icon
  WriteRegStr HKCR "${prodname}\DefaultIcon" "" "$INSTDIR\${icon}"
!endif
 
  SetOutPath $INSTDIR
 
 
; package all files, recursively, preserving attributes
; assume files are in the correct places
File /a "${srcdir}\${exec}"
 
!ifdef licensefile
File /a "${srcdir}\${licensefile}"
!endif
 
!ifdef notefile
File /a "${srcdir}\${notefile}"
!endif
 
!ifdef icon
File /a "${srcdir}\${icon}"
!endif
 
;--------------------------------------------------------
; any application-specific files  **Make sure all files in File List are also listed here:
File /a "${srcdir}\${file1}"
File /a "${srcdir}\${file2}"
File /a "${srcdir}\${file3}"

;--------------------------------------------------------
 
  WriteUninstaller "${uninstaller}"
 
SectionEnd
 
;--------------------------------------------------------------
; create shortcuts
Section

;!ifdef AddToWindowsStartup
;  ;Add to Startup path:
;  CreateShortCut "$SMSTARTUP\${prodname}.lnk" "$INSTDIR\${exec}"	; "$INSTDIR\${exec}" 2 SW_SHOWNORMAL
;!endif
 
  CreateDirectory "${startmenu}"
  SetOutPath $INSTDIR ; for working directory
!ifdef icon
  CreateShortCut "${startmenu}\${prodname}.lnk" "$INSTDIR\${exec}" "" "$INSTDIR\${icon}"
  CreateShortCut "$DESKTOP\${prodname}.lnk" "$INSTDIR\${exec}" "" "$INSTDIR\${icon}"
!else
  CreateShortCut "${startmenu}\${prodname}.lnk" "$INSTDIR\${exec}"
  CreateShortCut "$DESKTOP\${prodname}.lnk" "$INSTDIR\${exec}"
!endif
 
!ifdef notefile
  CreateShortCut "${startmenu}\Release Notes.lnk "$INSTDIR\${notefile}"
!endif
 
!ifdef helpfile
  CreateShortCut "${startmenu}\Documentation.lnk "$INSTDIR\${helpfile}"
!endif
 
!ifdef website
WriteINIStr "${startmenu}\web site.url" "InternetShortcut" "URL" ${website}
 ; CreateShortCut "${startmenu}\Web Site.lnk "${website}" "URL"
!endif
 
!ifdef notefile
ExecShell "open" "$INSTDIR\${notefile}"
!endif

;Create uninstall shortcut:
CreateShortCut "${startmenu}\Uninstall.lnk "$INSTDIR\${uninstaller}"

;Set full access permissions on Installation folder (Needed for Windows Vista/7 or newer to allow the program to write to the ini file if needed):
;IMPORTANT NOTE: Install the NSIS AccessControl plugin in order to utilize the following function:
;http://nsis.sourceforge.net/AccessControl_plug-in
;Download AccessControl.zip and copy and paste the contents into c:\program files\NSIS
AccessControl::GrantOnFile "$INSTDIR" "(S-1-1-0)" "FullAccess"

;Delete any existing ini file (to allow for a clean re-install):
Delete $INSTDIR\doover.ini

;Rename default.ini to doover.ini
Rename $INSTDIR\default.ini $INSTDIR\doover.ini

;change LogPath ini option in doover.ini to point to the installation path:
Writeinistr $INSTDIR\doover.ini Settings LogPath $INSTDIR

!ifdef filetoexec
Exec "$INSTDIR\${filetoexec}"
!endif

SectionEnd
 
;--------------------------------------------------------------
; Uninstaller
; All section names prefixed by "Un" will be in the uninstaller
 
UninstallText "This will uninstall ${prodname}."
 
!ifdef icon
UninstallIcon "${icon}"
!endif
 
Section "Uninstall"
 
  DeleteRegKey HKLM "${uninstkey}"
  DeleteRegKey HKLM "${regkey}"
 
  Delete "${startmenu}\*.*"
  RMDir "${startmenu}"

;Delete desktop shortuct:
Delete "$DESKTOP\${prodname}.lnk"

!ifdef licensefile
Delete "$INSTDIR\${licensefile}"
!endif
 
!ifdef notefile
Delete "$INSTDIR\${notefile}"
!endif
 
!ifdef icon
Delete "$INSTDIR\${icon}"
!endif
 
Delete "$INSTDIR\${exec}"

Delete "$SMSTARTUP\${prodname}.lnk"

;--------------------------------------------------------
; any application-specific files  **Make sure all files in File List are also listed here::
Delete "$INSTDIR\${file1}"
Delete "$INSTDIR\${file2}"
Delete "$INSTDIR\${file3}"

;--------------------------------------------------------
 
;Delete installer:
Delete "$INSTDIR\${uninstaller}"
 
;Delete install folder:
RMDir $INSTDIR
 
SectionEnd




; Usage
; Define in your script two constants:
;   DOT_MAJOR "(Major framework version)"
;   DOT_MINOR "{Minor framework version)"
;   DOT_MINOR_MINOR "{Minor framework version - last number after the second dot)"
; 
; Call IsDotNetInstalledAdv
; This function will abort the installation if the required version 
; or higher version of the .NET Framework is not installed.  Place it in
; either your .onInit function or your first install section before 
; other code.
Function IsDotNetInstalledAdv
   Push $0
   Push $1
   Push $2
   Push $3
   Push $4
   Push $5
 
  StrCpy $0 "0"
  StrCpy $1 "SOFTWARE\Microsoft\.NETFramework" ;registry entry to look in.
  StrCpy $2 0
 
  StartEnum:
    ;Enumerate the versions installed.
    EnumRegKey $3 HKLM "$1\policy" $2
 
    ;If we don't find any versions installed, it's not here.
    StrCmp $3 "" noDotNet notEmpty
 
    ;We found something.
    notEmpty:
      ;Find out if the RegKey starts with 'v'.  
      ;If it doesn't, goto the next key.
      StrCpy $4 $3 1 0
      StrCmp $4 "v" +1 goNext
      StrCpy $4 $3 1 1
 
      ;It starts with 'v'.  Now check to see how the installed major version
      ;relates to our required major version.
      ;If it's equal check the minor version, if it's greater, 
      ;we found a good RegKey.
      IntCmp $4 ${DOT_MAJOR} +1 goNext yesDotNetReg
      ;Check the minor version.  If it's equal or greater to our requested 
      ;version then we're good.
      StrCpy $4 $3 1 3
      IntCmp $4 ${DOT_MINOR} +1 goNext yesDotNetReg
 
      ;detect sub-version - e.g. 2.0.50727
      ;takes a value of the registry subkey - it contains the small version number
      EnumRegValue $5 HKLM "$1\policy\$3" 0
 
      IntCmpU $5 ${DOT_MINOR_MINOR} yesDotNetReg goNext yesDotNetReg
 
    goNext:
      ;Go to the next RegKey.
      IntOp $2 $2 + 1
      goto StartEnum
 
  yesDotNetReg: 
    ;Now that we've found a good RegKey, let's make sure it's actually
    ;installed by getting the install path and checking to see if the 
    ;mscorlib.dll exists.
    EnumRegValue $2 HKLM "$1\policy\$3" 0
    ;$2 should equal whatever comes after the major and minor versions 
    ;(ie, v1.1.4322)
    StrCmp $2 "" noDotNet
    ReadRegStr $4 HKLM $1 "InstallRoot"
    ;Hopefully the install root isn't empty.
    StrCmp $4 "" noDotNet
    ;build the actuall directory path to mscorlib.dll.
    StrCpy $4 "$4$3.$2\mscorlib.dll"
    IfFileExists $4 yesDotNet noDotNet
 
  noDotNet:
    ;Nope, something went wrong along the way.  Looks like the 
    ;proper .NET Framework isn't installed.  
 
     MessageBox MB_OK "You must have .NET Framework 2.0 installed.  Aborting Installation!"
     Abort
     StrCpy $0 0
     Goto done
 
     yesDotNet:
    ;MessageBox MB_OK "Success.  You have at least v${DOT_MAJOR}.${DOT_MINOR}.${DOT_MINOR_MINOR} or greater of the .NET Framework installed."
	
    ;Everything checks out.  Go on with the rest of the installation.
    StrCpy $0 1
 
   done:
     Pop $4
     Pop $3
     Pop $2
     Pop $1
     Exch $0
 FunctionEnd