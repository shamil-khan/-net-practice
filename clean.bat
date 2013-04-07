@echo off
@echo ----------------- start ---------------------------

rem When run as administrator, need to fix the current directory of batch file.
@setlocal enableextensions
@cd /d "%~dp0"


@echo iis reset
@echo -------------------------------------------
"%SYSTEMROOT%\System32\iisreset.exe"

set clean_path=%CD%
@echo -------------------------------------------
@echo start cleaning "%clean_path%"
@echo -------------------------------------------

@echo delete - "%clean_path%\" ,  bin, obj, _logs, *.suo, *.csproj.user 
rd /s/q "%clean_path%\bin"
rd /s/q "%clean_path%\obj"
rd /s/q "%clean_path%\_logs"
del /q /s /f /a:h "%clean_path%\*.suo"
del /q /s "%clean_path%\*.csproj.user"

for /d /r "%clean_path%" %%d in (bin,obj,_logs) do ( 
	@if exist "%%d" (
		@echo delete - "%%d" 
		rd /s/q "%%d"
	)
)

@echo ----------------- finish --------------------------
pause