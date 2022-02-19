rm -rf dist

dotnet build ./Sample/TelstraPurple.ConsoleSample/TelstraPurple.ConsoleSample.csproj -c Release -o ./dist/TelstraPurple.ConsoleSample

dotnet build ./Sample/TelstraPurple.ConsoleFactorySample/TelstraPurple.ConsoleFactorySample.csproj -c Release -o ./dist/TelstraPurple.ConsoleFactorySample