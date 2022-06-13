#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Service.Gateway.Api/Service.Gateway.Api.csproj", "Service.Gateway.Api/"]
RUN dotnet restore "Service.Gateway.Api/Service.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/Service.Gateway.Api"
RUN dotnet build "Service.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Service.Gateway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.Gateway.Api.dll"]