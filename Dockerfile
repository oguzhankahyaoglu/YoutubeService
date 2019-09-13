FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY YoutubeService/*.csproj ./YoutubeService/
RUN dotnet restore

# copy everything else and build app
COPY YoutubeService/. ./YoutubeService/
WORKDIR /app/YoutubeService
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/YoutubeService/out ./
CMD ASPNETCORE_URLS=http://*:$PORT dotnet YoutubeService.dll