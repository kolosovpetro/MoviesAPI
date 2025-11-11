FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["MoviesAPI.Core/MoviesAPI.Core.csproj", "MoviesAPI.Core/"]
COPY ["MoviesAPI.Data/MoviesAPI.Data.csproj", "MoviesAPI.Data/"]
COPY ["MoviesAPI.Models/MoviesAPI.Models.csproj", "MoviesAPI.Models/"]
COPY ["MoviesAPI.Requests/MoviesAPI.Requests.csproj", "MoviesAPI.Requests/"]
RUN dotnet restore "MoviesAPI.Core/MoviesAPI.Core.csproj"
COPY . .
WORKDIR "/src/MoviesAPI.Core"

ARG VERSION
RUN dotnet build "MoviesAPI.Core.csproj" -c Release -p:Version=$VERSION -o /app/build

FROM build AS publish
RUN dotnet publish "MoviesAPI.Core.csproj" -c Release -p:Version=$VERSION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoviesAPI.Core.dll"]