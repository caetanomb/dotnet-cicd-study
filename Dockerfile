FROM microsoft/aspnetcore-build as build-image

WORKDIR /home/app

COPY ./AccountOwnerServer/AccountOwnerServer.csproj ./AccountOwnerServer/
COPY ./Contracts/Contracts.csproj ./Contracts/
COPY ./Repository/Repository.csproj ./Repository/
COPY ./Entities/Entities.csproj ./Entities/
COPY ./LoggerService/LoggerService.csproj ./LoggerService/
COPY ./Tests/Tests.csproj ./Tests/
COPY ./AccountOwnerServer.sln .

ADD domainca.crt /usr/local/share/ca-certificates/domainca.crt
RUN chmod 644 /usr/local/share/ca-certificates/domainca.crt && update-ca-certificates

RUN dotnet restore

COPY . .

RUN dotnet test --no-restore --verbosity=normal ./Tests/Tests.csproj

RUN dotnet publish ./AccountOwnerServer/AccountOwnerServer.csproj -o /publish/

FROM microsoft/aspnetcore

WORKDIR /publish

COPY --from=build-image /publish .

ENV TEAMCITY_PROJECT_NAME = ${TEAMCITY_PROJECT_NAME}

ENTRYPOINT ["dotnet", "AccountOwnerServer.dll"]