# Simple Temp Cleaner

This is a command line application that cleans your temporary files for Windows. You can run it manually or you can install it as a task.

## Folders To Clean

-   `C:\Windows\Temp` (Run > `temp`)
-   `C:\Users\user\AppData\Local\Temp` (Run > `%temp%`)
-   `C:\Users\user\AppData\Roaming\Microsoft\Windows\Recent` (Run > `shell:recent`)

## Basic Usage

Just run the application as administrator in a terminal like `PowerShell`.

```
./Simple_Temp_Cleaner.exe
```

## Install As A Task

You can install the application as a task. It will automatically clean your temporary files every 10 minutes. (Do not forget to run it as administrator)

```
./Simple_Temp_Cleaner.exe --install
```

or

```
./Simple_Temp_Cleaner.exe -i
```

## Uninstall

If you installed the application as a task, you can uninstall it by executing this command: (Do not forget to run it as administrator)

```
./Simple_Temp_Cleaner.exe --uninstall
```

or

```
./Simple_Temp_Cleaner.exe -u
```

## ☕ Support

If you find this project useful and would like to support [me](https://github.com/BUR4KBEY), you can do so by visiting [my website](https://burakbey.dev).

<a href="https://burakbey.dev" target="_blank"><img src="https://burakbey.dev/github_support_snippet.png" style="height: 56px !important;width: 200px !important;" alt="Buy me a coffee"></img></a>
