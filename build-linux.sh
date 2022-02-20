rm -rf dist

dotnet build ./Sample/TelstraPurple.ConsoleSample/TelstraPurple.ConsoleSample.csproj -r linux-x64 -c Release -o ./dist/TelstraPurple.ConsoleSample

dotnet build ./Sample/TelstraPurple.ConsoleFactorySample/TelstraPurple.ConsoleFactorySample.csproj -r linux-x64 -c Release -o ./dist/TelstraPurple.ConsoleFactorySample