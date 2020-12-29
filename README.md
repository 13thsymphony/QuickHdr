# Quick HDR
Quick HDR is a simple background app that provides quick control over Windows HDR settings. You can turn HDR on and off with a global hotkey (CTRL+WIN+H), and check whether HDR is enabled with a system tray icon.

Quick HDR uses Windows accessibility technology to automatically access and drive the Settings app (Settings > System > Display page). This means that Quick HDR will open the Settings app in the background when triggered.

## System requirements
Quick HDR is only useful on systems that [support Windows HDR](https://support.microsoft.com/en-us/windows/display-requirements-for-hdr-video-in-windows-10-192f362e-1245-e14d-3d3f-4b3fc606b80f).

In addition, we strongly recommend that you update to the latest version of Windows 10 and the latest graphics driver. There is no technical limitation to supporting older OSes and drivers, but updating gives you the latest bugfixes and feature improvements for HDR Support.

## Instructions
1. Launch Quick HDR.
2. If you'd like to start Quick HDR automatically when you login to Windows, follow the instructions [here](https://support.microsoft.com/en-us/windows/add-an-app-to-run-automatically-at-startup-in-windows-10-150da165-dcd9-7230-517b-cf3c295d89dd).
3. Press *CTRL + WIN + H* to toggle HDR on and off. You will see the Settings app open (minimized) as the app is accessing HDR controls via the Settings app. You will also see the screen momentarily flash black as the display switches between HDR and SDR modes.
4. You can quickly see whether HDR is currently on or off with the system tray icon to the right of the taskbar.
5. If you click the system tray icon, a popup menu will let you turn HDR on and off (same as the hotkey), close Quick HDR, 

## Limitations and notes
* Quick HDR currently only supports HDR settings on the primary display. If you have a multi-monitor system, you can change this by opening Settings > System > Display and looking for the "Make this my main display" control.