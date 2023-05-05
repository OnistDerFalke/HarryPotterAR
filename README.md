# HarryPotterAR

Application made in Unity and Vuforia Engine to detect markers on "Harry Potter: Year In Hogwarts" Board Game.

## Setup and first steps
1. Clone repository on your drive.
2. Go to https://drive.google.com/drive/folders/1BIFQBBmdC4qUxUZUXfnEZBJeS3x8ttCm?usp=share_link and download Vuforia package.
3. Insert it to Packages dir which is located in main project directory.
4. Open the project with Unity (recommended version: *2021.3.20f1*).
5. Now you can run the project.


## Running on mobile device with Android
1. Turn on debugging mode on your device.
2. In Unity go to *File->Build Settings*.
3. Choose Android as *Platform* and click *Switch Platform* button.
4. Now from *Run Device* dropdown choose your mobile device name.
5. Click *Build and Run* button. After build the .apk will be sent to your mobile device and run.
6. Your app is ready on your mobile device.


## Printing markers

### Vuforia (MarcinMarks)
You can download MarcinMarks from https://drive.google.com/drive/folders/1BIFQBBmdC4qUxUZUXfnEZBJeS3x8ttCm?usp=share_link and print it.
Application should detect this markers when you set active MarcinMarks object in GameScene (no models for characters yet, just plain 3D shapes).
This marker detection is worse option for Vuforia. It is based on image detection without VuMarks. MarcinMarks images are rated 5 stars in Vuforia Database.

### Vuforia (VuMarks)
You can download VuMarks from https://drive.google.com/drive/folders/1BIFQBBmdC4qUxUZUXfnEZBJeS3x8ttCm?usp=share_link and print it.
Application should detect this markers when you set active VuMarks object in GameScene (no models for characters yet, just plain 3D shapes).
This marker detection is better option for Vuforia. It is based on special VuMarks detection which have data coded inside it's shapes.



