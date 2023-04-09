FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build CqrsApi.Core/CqrsApi.Core.csproj -c Release -o /app/build
RUN dotnet publish CqrsApi.Core/CqrsApi.Core.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CqrsApi.Core.dll"]