# The Kinematics Demostration on Unity Platform
This project demostrate the calculation of Forward Kinematics and Inverse Kinematics of Articulated Robot.  

## Startup Configuration
The startup in the command line allows to change the link length of robot and movement speed.  
From the base "link1" to the top "link3", the link length can be changed by using the parameter in the command line before startup.
**You cannot change the link Length when you start the application**
In default, the link length is set 2, but it can be changed before startup by using command line.
+ For Standard Release, it possible to startup in the "cmd.exe" or by using shortcut.
+ For UWP Version, please refer to [Microsoft Document](https://blogs.windows.com/buildingapps/2017/07/05/command-line-activation-universal-windows-apps/)

### Example for startup configuration

*Assume the execution file name is robotdemo.exe*  
startup with link length setting  
``` Shell
    robotdemo.exe -link1 1.8 -link2 2.1 -link3 1.9
```    
Startup with endpoint movement speed setting  
``` Shell
    robotdemo.exe -speed 0.2
```
Startup with both link length and movement speed  
``` Shell
    robotdemo.exe -link1 1.8 -link2 2.1 -speed 0.02
```

[**Release output is upload to the github**](https://github.com/zhuchen115/Kinematics/releases)

## Building this DEMO
 1. **Ensure the Kinematics.dll in in the root directory of Assert**
 2. Make sure you have set the "Script Runtime" to .net 4.6 compatiable 

## UWP Release Notice
  1. You need Visual Studio 2017 to build the UWP not in Unity.
  2. Make sure the windows 10 enabled developer mode.
