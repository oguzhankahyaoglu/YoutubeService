REM - This file assumes that you have access to the application and that you have docker installed
REM : Setup your applications name below
SET APP_NAME="ytdownloader-api"

call docker build -t %APP_NAME% .
call heroku container:push web -a %APP_NAME%
call heroku container:release web -a %APP_NAME%

PAUSE