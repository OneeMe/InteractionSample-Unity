# PICO Interaction Sample Unity

![Model](https://github.com/Pico-Developer/PICOInteractionSample-Unity/blob/main/Interaction%20Sample.png)

This sample contains the following scenes that each showcase an interaction feature:
1. Controller and Headset tracking
2. Controller Haptics: Buffered and Unbuffered
3. Controller Locomotion
4. Hand Tracking Interactions
5. Custom Hand Gesture
6. Unity Spatial Keyboard

## Development Environment

- SDK version: 3.0.4 
- PICO device's system version: 5.11.0
- Unity version: 2022.3.29f1
- Graphics API: Vulkan
- App structure: 64-bit


## About the Project
The PICO Interaction Sample showcases the functionalities of the Interaction Pack of the PICO Unity Integration SDK, including basic 
controller and hand interactions, haptics, keyboard inputs, and locomotion. Developers can learn how to setup input and interactions with the Unity XR Interaction Toolkit 3.0 and the XR Hands packages.

Refer to the [documentation](https://developer.picoxr.com/document/unity/pico-interaction-sample/?v=3.0.0) page of this sample for detailed descriptions and usages of the project and scenes.


## Build and Installation
You can build the Unity project and install the sample APK file on your PICO 4 series device for testing. 
Use a USB cable to connect your PICO device to your PC, then open a command line window and use the following ADB command to install the APK file on the device: 

adb install "filepath\filename.apk"


## LICENSE
Most of this project is licensed under the MIT License, see [License.md](https://github.com/Pico-Developer/InteractionSample-Unity/blob/main/License.md) for licenses of dependent files.
