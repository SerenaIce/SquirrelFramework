

rem <!> Change the version and re-build the code under Release mode

rem generate .nuspec file.
rem nuget spec

rem CD to the release folder and run the following commands
rem For .NET Core library, use vs Command line
dotnet pack --configuration release -p:NuspecFile=SquirrelFramework.Utility.nuspec
rem or execute
rem msbuild /t:pack /p:Configuration=Release /p:NuspecFile=SquirrelFramework.Utility.nuspec

rem To change the version
nuget push SquirrelFramework.Utility.1.0.1.nupkg -Source https://www.nuget.org/api/v2/package