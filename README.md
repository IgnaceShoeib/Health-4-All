# Health-4-All
## Tutorial for how to set up the project from scratch
### Part 1: setting up the project

1. Download unity hub
2. Download the lastest version of the unity editor 2022.3
3. Install the following packages 
    - AI Navigation
    - Burst
    - Core RP Library
    - Custom NUnit
    - Input System
    - JetBrains Rider Editor
    - Mathematics
    - Oculus XR Plugin
    - OpenXR Plugin
    - Searcher
    - Shader Graph
    - Test Framework
    - TextMeshPro
    - Timeline
    - Unity UI
    - Universal RP
    - Version Control
    - Visual Scripting
    - Visual Studio Code Editor
    - Visual Studio Editor
    - XR Core Utilities 
    - XR Interaction Toolkit
    - XR Legacy Input Helpers
    - XR Plugin Management
4. Download the following XR Interaction Toolkit samples
    - Starter Assets
    - XR Device Simulator

### Part 2: setting up the scenes

1. Make a scene for every minigame (food, sport, mind)
2. Make a scene for the lobby that connects every minigame
3. Use the [Terloplein 3d model](https://github.com/IgnaceShoeib/Health-4-All/raw/main/Assets/Models/Terloplein%203D%20version%203.fbx) in every scene
4. Place every mascot in the main lobby with a play button to load the correct scene using [this script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/ChangeScene.cs)

### Part 3: the Food scene

1. Add the [food mascot (the pear)](https://github.com/IgnaceShoeib/Health-4-All/raw/main/Assets/Models/Pear_Mascot_Jump_fixed.fbx) to the scene
2. Create an animator for him and add all his animations to it.
    - Make 6 different layers and put the weight on 1 for each and name them
        - Eyes
            - Add the animation for the eyes from entry and let it go to another eyes animation to make it loop forever
        - Mouth
            - From entry add an empty animation
            - From any state let it go to the animation and then to exit
        - Idle
            - Add the idle animation from entry and let it go to another idle animation to make it loop forever
        - Jump
            - From entry add an empty animation
            - From any state let it go to the animation and then to the thinking animation and then to the exit
        - Sad
            - From entry add an empty animation
            - From any state let it go to the animation and then to the thinking animation and then to the exit 
        - SadMouth 
            - From entry add an empty animation
            - From any state let it go to the animation and then to exit
    - Add the animator to the mascot
    - Make a [Mascot script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Food/Mascot.cs) and add it to the mascot
    - Add a text bubble to the mascot and assign it in the mascot
    - Add thunder and rain to the scene and assign it in the mascot but make them inactive in the scene, the mascot will activate it when it is needed
    - Add a the [Thunder script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Food/Thunder.cs) to the thunder
    - Add a cross and checkmark to the mascot and assign it in the mascot, make sure the material on the checkmark and cross is invisible
    - Add a canvas to the mascot with text for the score and 2 buttons to restart or return to the lobby, make sure the canvas is inactive and assign it to the mascot
    - Decorate the scene with benches, tables, trays, plates and other food related stuff
    - Add a lot of different food and the [Food script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Food/Food.cs) to it
