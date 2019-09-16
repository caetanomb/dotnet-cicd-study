FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /home/app

COPY ./AccountOwnerServer/AccountOwnerServer.csproj ./AccountOwnerServer/
COPY ./Contracts/Contracts.csproj ./Contracts/
COPY ./Repository/Repository.csproj ./Repository/
COPY ./Entities/Entities.csproj ./Entities/
COPY ./LoggerService/LoggerService.csproj ./LoggerService/
COPY ./Tests/Tests.csproj ./Tests/
COPY ./Integration/IntegrationTests.csproj ./Integration/
COPY ./AccountOwnerServer.sln .

ADD domainca.crt /usr/local/share/ca-certificates/domainca.crt
RUN chmod 644 /usr/local/share/ca-certificates/domainca.crt && update-ca-certificates

RUN dotnet restore

COPY . .

WORKDIR /home/app/Integration/

ENTRYPOINT [ "dotnet", "test", "--verbosity=normal"]