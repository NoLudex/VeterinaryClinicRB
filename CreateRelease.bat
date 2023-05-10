rem Создание релиз версии...
rd /s /q "./bin/Release"
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
xcopy ./database ./bin/Release/net7.0/win-x64/publish/database /E /Y
rename ./bin/Release/net7.0/win-x64/publish/VeterinaryClinicRB.dll.config app.config
xcopy ./dist/. ./bin/Release/net7.0/win-x64/publish /E /Y
pause