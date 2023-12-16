dotnet restore MoviesAPI.sln
dotnet build MoviesAPI.sln --configuration Release /p:ContinuousIntegrationBuild=true --no-restore
dotnet publish "MoviesAPI.Core\MoviesAPI.Core.csproj" -c Release -f net6.0 -r linux-x64 --self-contained
dotnet publish "MoviesAPI.Core\MoviesAPI.Core.csproj" -c Release -f net6.0 -r win-x64 --self-contained