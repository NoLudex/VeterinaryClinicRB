@echo off
rd /s /q bin\Release\Release
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
ren bin\Release\net7.0\win-x64\publish\VeterinaryClinicRB.dll.config app.config
xcopy /s /i /exclude:dist\*.* dist\*.* bin\Release\net7.0\win-x64\publish\
pause