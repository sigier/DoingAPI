cd src
dotnet publish ./Doing.Api -c Release -o ./bin/Docker
dotnet publish ./Doing.Services.Activities -c Release -o ./bin/Docker
dotnet publish ./Doing.Services.Identity -c Release -o ./bin/Docker