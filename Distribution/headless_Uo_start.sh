#!/bin/bash

cd /home/main-bitch/ModernUOServer/Distribution || exit 1
export PATH=$PATH:/usr/share/dotnet

case "$(pgrep -fc 'dotnet.*ModernUO.dll')" in
0) echo "Starting Modern UO Service: $(date)" >> ./monitor_log.txt
	dotnet ModernUO.dll
	;;
1) echo "Modern UO is Running and Happy: $(date)" 
	;;
*) echo "Remove multiple instances of Modern UO Running: $(date)" ./monitor_log.txt
	kill $(pidof 'dotnet ModernUO.dll' | awk '{print $1}')
	;;
esac
