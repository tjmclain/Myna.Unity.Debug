dotnet test -c Release
dotnet build -c Release
dotnet pack
dotnet nuget push ".\bin\Release\*.nupkg" --source https://api.nuget.org/v3/index.json