FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY . ./

RUN dotnet restore ./src/Open5ETools.Web/Open5ETools.Web.csproj
RUN dotnet publish "src/Open5ETools.Web/Open5ETools.Web.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "Open5ETools.Web.dll"]