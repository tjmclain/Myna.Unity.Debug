Write-Host ""
Write-Host "Test Release configuration"
dotnet test -c Release

Write-Host ""
Write-Host "Build Release configuration"
dotnet build -c Release

Write-Host ""
Write-Host "Pack NuGet package"
dotnet pack

Write-Host ""
Write-Host "Push Nuget package"
dotnet nuget push ".\bin\Release\*.nupkg" --source https://api.nuget.org/v3/index.json --skip-duplicate