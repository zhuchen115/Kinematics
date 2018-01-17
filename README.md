# Kinematics
The robot joint forward kinematics and inverse kinematics.
## Forward Kinematics
The forward kinematics is calculated by vector transform. more DOF is acceptable.
## Inverse Kinematics
The inverse kinematics only limited to 4 types of robot configuration. 
1. Cartesian
2. Cylindrical
3. Spherial
4. Articulated

## Command line Demostration
The code to test the accuracy of kinematics calculation can be found [here](https://github.com/zhuchen115/Kinematics/blob/master/KineDemo/Program.cs)

## Build the Project
 The project is using Visual Studio 2017. You need Visual Studio 2017 to build the project. The community version of Visual Studio is enough.

## Unity Demo 
The unity projects is designed on Unity 2017.3.
Note the axis system used in the unity is left hand axis. The kinematics library is using right hand axis.
### Building Requirements of Unity Project
 + Visual Studio 2017
 + Unity 2017.3
 **You need to the set the player setting "Scripting Runtime Version" to ".net 4.6 equivlant"**
 [Find the document of runtime of unity here](https://blogs.unity3d.com/2017/07/11/introducing-unity-2017/#runtime)
 
### Building Instruction of Unity Project
 + If you rebuild the Main Kinematics Library. You need to copy the **RELEASE** Version DLL file to the *Assert* **ROOT DIRECTORY**
 + The project support UWP Release, You need to enable [**developer mode of Windows 10**](https://docs.microsoft.com/en-us/windows/uwp/get-started/enable-your-device-for-development) 


