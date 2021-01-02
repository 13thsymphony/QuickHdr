# Quick HDR
Quick HDR is a simple background app that provides quick control over Windows HDR settings. You can turn HDR on and off with a global hotkey (CTRL+WIN+H), and check whether HDR is enabled with a system tray icon.

Quick HDR uses Windows accessibility technology to automatically access and drive the Settings app (Settings > System > Display page). This means that Quick HDR will open the Settings app in the background when triggered.

## How to install
*Note:* For now, you have to build from source (see below).

Quick HDR is only useful on systems that [support Windows HDR](https://support.microsoft.com/en-us/windows/display-requirements-for-hdr-video-in-windows-10-192f362e-1245-e14d-3d3f-4b3fc606b80f).

In addition, we strongly recommend that you update to the latest version of Windows 10 and the latest graphics driver. There is no technical limitation to supporting older OSes and drivers, but updating gives you the latest bugfixes and feature improvements for HDR Support.

## Instructions
1. Launch Quick HDR.
2. Quick HDR automatically registers itself to start automatically when you login. You can enable/disable this via [Startup Manager](https://support.microsoft.com/en-us/windows/change-which-apps-run-automatically-at-startup-in-windows-10-9115d841-735e-488d-e749-9ba301d441e6).
3. Press *CTRL + WIN + H* to toggle HDR on and off. You will see the Settings app open (minimized) as the app is accessing HDR controls via the Settings app. You will also see the screen momentarily flash black as the display switches between HDR and SDR modes.
4. You'll see a popup indicating what the new HDR mode is.
5. If you click the system tray icon, a popup menu will let you turn HDR on and off (same as the hotkey), or to exit Quick HDR, 

## Limitations and notes
* Quick HDR currently only supports HDR settings on the primary display. If you have a multi-monitor system, you can change this by opening Settings > System > Display and looking for the "Make this my main display" control.
* Quick HDR relies on UI Automation to automatically access and drive the Settings app. You will see the Settings app load whenever you toggle HDR on and off. In addition, this method is not 100% reliable and sometimes will have no effect. In most cases, just try toggling HDR again. Feel free to close the Settings app as Quick HDR will automatically relaunch it when needed.

## How to build
Requirements:
- Visual Studio 2019 or later with the .NET Desktop Development and Universal Windows Platform workloads
   - Specifically, this app uses .NET 5 and the [Windows Application Packaging](https://docs.microsoft.com/en-us/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) frameworks.

1. The main project (QuickHdr) is a regular WPF app and you can run it directly as a loose-file app.
2. If you want to package it as an MSIX installer for easy distribution, [Publish the AppPackage project](https://docs.microsoft.com/en-us/windows/msix/package/packaging-uwp-apps#generate-an-app-package-upload-file-for-store-submission).
