# Launcher
- Is a simple Windows process launcher.
- It's work in progress and should be simple.

Main reason why I build this tool is that:
- I'm getting lost in plenty of file explorer windows 
- they are nearly not distinguishable from each other in windows taskbar
- Windows file explorer navigation and pinning features driven me crazy

![AppWindow](https://github.com/viper3400/Launcher/blob/master/ProcessLauncher.png)

## Configuration

- Lauchner is configurable in config.txt. 
- The file has to be placed within the application directory next to the EXE. 
- Each line means one command and one button in the UI.
- Each line need to follow the syntax:

```[NAME IN UI];[PATH];[SHELLEXECUTE];[COLOR]```

Starting with 1.1.0 you can add a color from Bushes class to set  background color of the button (https://docs.microsoft.com/en-us/dotnet/api/system.drawing.brushes)

Example

```
Open folder C:\Temp;C:\Temp\;true;red
Open Putty;C:\Temp\putty.exe;false
```

With the ShellExecute parameter set to true you can launch documents or folders and so on with the default application.

# Publish as SingeFile Application
```
dotnet publish -r win-x64 -c Release -o publish -p:PublishReadyToRun=true 
               -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true 
               -p:IncludeNativeLibrariesForSelfExtract=true -p:Version=[x.y.z.n]
```

* https://github.com/dotnet/designs/blob/main/accepted/2020/single-file/design.md#user-experience
* https://github.com/dotnet/runtime/issues/36590

# Credits
## App Icon
* https://www.deviantart.com/franksouza183
* http://www.iconarchive.com/show/fs-icons-by-franksouza183/Places-folder-windows-icon.html
