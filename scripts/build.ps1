# Build the project
dotnet build

# Check if the build was successful
if ($LASTEXITCODE -eq 0) {
    # Copy the mod to the Steam Mods directory
    Copy-Item "bin\Debug\net48\QualityOfLife.dll" "$env:ProgramFiles(x86)\Steam\steamapps\common\Schedule I\Mods\"

    # Start the game
    Start-Process "steam://rungameid/3164500"
} else {
    Write-Host "Build failed!"
}
