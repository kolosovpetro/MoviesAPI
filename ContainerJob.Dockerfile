# Use the official .NET 6 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project files
COPY ["MoviesAPI.Core/MoviesAPI.Core.csproj", "MoviesAPI.Core/"]
COPY ["MoviesAPI.Data/MoviesAPI.Data.csproj", "MoviesAPI.Data/"]
COPY ["MoviesAPI.Models/MoviesAPI.Models.csproj", "MoviesAPI.Models/"]
COPY ["MoviesAPI.Requests/MoviesAPI.Requests.csproj", "MoviesAPI.Requests/"]

# Restore dependencies
RUN dotnet restore "MoviesAPI.Core/MoviesAPI.Core.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/MoviesAPI.Core"
RUN dotnet build "MoviesAPI.Core.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "MoviesAPI.Core.csproj" -c Release -o /app/publish /p:UseAppHost=false