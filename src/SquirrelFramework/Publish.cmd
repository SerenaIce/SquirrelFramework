

rem <!> Change the version and re-build the code under Release mode

rem generate .nuspec file.
rem nuget spec

rem Run run the following commands
nuget pack SquirrelFramework.csproj -Properties Configuration=Release -Properties NuspecFile=SquirrelFramework.nuspec

rem To change the version
nuget push SquirrelFramework.1.1.0.nupkg -Source https://www.nuget.org/api/v2/package