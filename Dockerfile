FROM mcr.microsoft.com/dotnet/runtime:6.0

WORKDIR .
COPY . .
ENTRYPOINT ["dotnet", "cli.dll"]