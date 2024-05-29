FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY /app/publish .
ENTRYPOINT ["dotnet", "OpaAuth.dll"]
EXPOSE 80

