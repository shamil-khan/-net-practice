@echo off

rem When run as administrator, need to fix the current directory of batch file.
@setlocal enableextensions
@cd /d "%~dp0"

set iis="%SYSTEMROOT%\System32\inetsrv\appcmd.exe"
set app_name=secure-web
set app_path="%CD%\%app_name%"
rem @echo %app_path%

@echo -----------------------------------------------------------
@echo Started - IIS Installer
@echo -----------------------------------------------------------

set /p website=Enter Web Site name: 
set /p port=Enter http port: 
set /p port_s=Enter https port: 
rem @echo %website%:%port%:%port_s%

@echo -----------------------------------------------------------
@echo Creating Website Pool
%iis% add apppool /name:"%website%_pool" /managedRuntimeVersion:"v4.0" /managedPipelineMode:"Integrated"

@echo -----------------------------------------------------------
@echo Create Web Site
rem set website=%website%_sites
%iis% add site /name:"%website%_site" /bindings:http://*:%port% /physicalpath:%app_path% /applicationDefaults.applicationPool:"%website%_pool" 

rem @echo -----------------------------------------------------------
rem @echo Creating Applications
rem @echo -----------------------------------------------------------

rem @echo -----------------------------------------------------------
rem @echo Creating Application
rem %iis% add app /site.name:"%website%_sites" /app.name:%app_name% /path:"/%app_name%" /physicalpath:%app_path% /applicationPool:"%website%_pool"

@echo -----------------------------------------------------------
@echo End - IIS Installer
@echo -----------------------------------------------------------

pause
