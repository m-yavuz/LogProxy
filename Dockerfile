FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
    
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
    
WORKDIR /src
COPY ./ ./
RUN dotnet restore src/LogProxy.API/LogProxy.API.csproj
COPY . .
RUN dotnet build src/LogProxy.API/LogProxy.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish src/LogProxy.API/LogProxy.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .


ENTRYPOINT ["dotnet", "LogProxy.API.dll"]