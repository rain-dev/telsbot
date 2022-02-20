rm -rf dist

dotnet build ./Sample/TelstraPurple.ConsoleSample/TelstraPurple.ConsoleSample.csproj -r win-x64 -c Release -o ./dist/windows/TelstraPurple.ConsoleSample

dotnet build ./Sample/TelstraPurple.ConsoleFactorySample/TelstraPurple.ConsoleFactorySample.csproj -r win-x64 -c Release -o ./dist/windows/TelstraPurple.ConsoleFactorySample

dotnet build ./Sample/TelstraPurple.ConsoleSample/TelstraPurple.ConsoleSample.csproj -r linux-x64 -c Release -o ./dist/linux/TelstraPurple.ConsoleSample

dotnet build ./Sample/TelstraPurple.ConsoleFactorySample/TelstraPurple.ConsoleFactorySample.csproj -r linux-x64 -c Release -o ./dist/linux/TelstraPurple.ConsoleFactorySample