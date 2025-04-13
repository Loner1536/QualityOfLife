#!/bin/bash

# Check if the build was successful
if [ $? -eq 0 ]; then
    # Find Steam installation and copy the mod to the appropriate directory
    if [ -d "$HOME/.local/share/Steam" ]; then
        cp bin/Debug/net48/QualityOfLife.dll "$HOME/.local/share/Steam/steamapps/common/Schedule I/Mods/"
    elif [ -d "$HOME/.steam/steam" ]; then
        cp bin/Debug/net48/QualityOfLife.dll "$HOME/.steam/steam/steamapps/common/Schedule I/Mods/"
    fi

    # Start the game
    steam steam://rungameid/3164500
else
    echo "Build failed!"
fi
