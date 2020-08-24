REM - This file assumes that you have access to the application and that you have docker installed
REM : Setup your applications name below
SET APP_NAME="ytdownloader-api"

REM - Delete all files and folders in publish
del /q ".\YoutubeService\bin\Debug\netcoreapp3.1\*"
FOR /D %%p IN (".\YoutubeService\bin\Debug\netcoreapp3.1\*.*") DO rmdir "%%p" /s /q

dotnet clean --configuration Release
dotnet publish -c Release
copy Dockerfile .\YoutubeService\bin\Debug\netcoreapp3.1\publish\
cd .\YoutubeService\bin\Debug\netcoreapp3.1\publish\
call heroku container:login
call heroku container:push web -a %APP_NAME%
call heroku container:release web -a %APP_NAME%


