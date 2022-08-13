#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# This Dockerfile was automatically generated in Visual Studio => Add => Docker Support...

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebAppServer.Api/WebAppServer.Api.csproj", "WebAppServer.Api/"]
COPY ["WebAppServer.Autofac/WebAppServer.Autofac.csproj", "WebAppServer.Autofac/"]
COPY ["WebAppServer.Domain/WebAppServer.Domain.csproj", "WebAppServer.Domain/"]
COPY ["WebAppServer.Repository/WebAppServer.Repository.csproj", "WebAppServer.Repository/"]
COPY ["WebAppServer.Entities/WebAppServer.Entities.csproj", "WebAppServer.Entities/"]
COPY ["WebAppServer.Common/WebAppServer.Common.csproj", "WebAppServer.Common/"]
COPY ["WebAppServer.V1.Contracts/WebAppServer.V1.Contracts.csproj", "WebAppServer.V1.Contracts/"]
COPY ["WebAppServer.Repository.Seeder/WebAppServer.Repository.Seeder.csproj", "WebAppServer.Repository.Seeder/"]
COPY ["WebAppServer.Repository.DbUp/WebAppServer.Repository.DbUp.csproj", "WebAppServer.Repository.DbUp/"]
RUN dotnet restore "WebAppServer.Api/WebAppServer.Api.csproj"
COPY . .
WORKDIR "/src/WebAppServer.Api"
RUN dotnet build "WebAppServer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppServer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppServer.Api.dll"]