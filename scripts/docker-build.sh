cd src
docker build -f ./Doing.Api/Dockerfile -t doing.api ./Doing.Api
docker build -f ./Doing.Services.Doings/Dockerfile -t doing.services.doings ./Doing.Services.Doings
docker build -f ./Doing.Services.Identity/Dockerfile -t doing.services.identity ./Doing.Services.Identity