REM - This file assumes that you have access to the application and that you have docker installed
REM : Setup your applications name below
SET APP_NAME="ytdownloader-api"

REM - Delete all files and folders in publish
del /q ".\bin\Release\netcoreapp2.2\publish\*"
FOR /D %%p IN (".\bin\Release\netcoreapp2.2\publish\*.*") DO rmdir "%%p" /s /q

dotnet clean --configuration Release
dotnet publish -c Release
copy Dockerfile .\bin\Release\netcoreapp2.2\publish\
cd .\bin\Release\netcoreapp2.2\publish\
call heroku container:login
call heroku container:push web -a %APP_NAME%
call heroku container:release web -a %APP_NAME%


