FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restore first so the layer is cached until a project file changes
COPY Gelosoft.TaskManagementSystem.sln ./
COPY TMS.API/*.csproj TMS.API/
COPY TMS.APPLICATION/*.csproj TMS.APPLICATION/
COPY TMS.DOMAIN/*.csproj TMS.DOMAIN/
COPY TMS.INFRASTRUCTURE/*.csproj TMS.INFRASTRUCTURE/
COPY TMS.TESTS/*.csproj TMS.TESTS/
RUN dotnet restore Gelosoft.TaskManagementSystem.sln

COPY . .
RUN dotnet publish TMS.API -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "TMS.API.dll"]
