#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Service.Security.Api/Service.Security.Api.csproj", "Service.Security.Api/"]
RUN dotnet restore "Service.Security.Api/Service.Security.Api.csproj"
COPY . .
WORKDIR "/src/Service.Security.Api"
RUN dotnet build "Service.Security.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Service.Security.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.Security.Api.dll"]