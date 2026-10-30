#!/bin/bash

pkill -f "dotnet ModernUO.dll"

cd /home/main-bitch/ModernUOServer || exit 1

git pull 

