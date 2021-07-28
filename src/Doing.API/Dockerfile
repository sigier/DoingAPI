FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT Docker
ENTRYPOINT dotnet Doing.API.dll