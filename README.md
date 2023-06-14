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
5. Download the following unity asset store assets
    - [Quick Outline](https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488)

### Part 2: setting up the scenes

1. Make a scene for every minigame (food, sport, mind)
2. Make a scene for the lobby that connects every minigame
3. Use the [Terloplein 3d model](https://github.com/IgnaceShoeib/Health-4-All/raw/main/Assets/Models/Terloplein%203D%20version%203.fbx) in every scene
4. Place every mascot in the main lobby with a play button to load the correct scene using [this script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/ChangeScene.cs)
5. Add a complete XR origin Set Up to each scene, this is the player

### Part 3: the food scene

1. Add the [food mascot (the pear)](https://github.com/IgnaceShoeib/Health-4-All/raw/main/Assets/Models/Pear_Mascot_Jump_fixed.fbx) to the scene
2. Create an animator for him and add all his animations to it.
    - Make 6 different layers and put the weight on 1 for each and name them
        - Eyes
            - Add the animation for the eyes from entry and let it go to another eyes animation to make it loop forever
        - Mouth
            - From entry add an empty animation
            - From any state let it go to the animation and then to exit. Also put a condition on the connection from the any state.
        - Idle
            - Add the idle animation from entry and let it go to another idle animation to make it loop forever
        - Jump
            - From entry add an empty animation
            - From any state let it go to the animation and then to the thinking animation and then to the exit. Also put a condition on the connection from the any state.
        - Sad
            - From entry add an empty animation
            - From any state let it go to the animation and then to the thinking animation and then to the exit. Also put a condition on the connection from the any state.
        - SadMouth 
            - From entry add an empty animation
            - From any state let it go to the animation and then to exit. Also put a condition on the connection from the any state.
    - Add the animator to the mascot
3. Make a [FoodCombo script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Food/FoodCombo.cs) and [FoodClass script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Food/FoodClass.cs), this is needed for other scripts to function
4. Make a [Mascot script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Food/Mascot.cs) and add it to the mascot
    - Add a text bubble to the mascot and assign it in the mascot
    - Add thunder and rain to the scene and assign it in the mascot but make them inactive in the scene, the mascot will activate it when it is needed
    - Add the [Thunder script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Food/Thunder.cs) to the thunder
    - Add a cross and checkmark to the mascot and assign it in the mascot, make sure the material on the checkmark and cross is invisible
    - Add a canvas to the mascot with text for the score and 2 buttons to restart or return to the lobby, make sure the canvas is inactive and assign it to the mascot
5. Decorate the scene with benches, tables, trays, plates and other food related stuff
6. Add a lot of different food and the [Food script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Food/Food.cs) to it
7. Add a collider, rigidbody, the [collision sound script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/CollisionSound.cs) and an XR grab interactable to each object you want the user to be able to pick up, including food and non-food items like plates
8. Add sounds of the object falling to each interactable object with the [collision sound script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/CollisionSound.cs)
9. In the mascot, add each combination of food you want to compare and adjust the location of the food items for them to correctly appear in the text bubble
10. Add a billboard with a video on it that explains the game to the user

### Part 4: the sport scene

1. Add the [sport mascot (the banana)](https://github.com/IgnaceShoeib/Health-4-All/raw/main/Assets/Models/Banana_Mascot_Idle2.fbx) to the scene
2. Create an animator for him and add all his animations to it.
    - Make 2 different layers and put the weight on 1 for each and name them
        - Main
            - Add the animation for the idle animation from entry and let it go to another idle animation to make it loop forever. Also connect it from any state with an condition.
            - Add an animation from the any state to the squat animation and let it loop into itself. Also put a condition on the connection from the any state.
            - Add an animation from the any state to the stretch animation and let it loop into itself. Also put a condition on the connection from the any state.
        - Eyes
            - Add the animation for the eyes from entry and let it go to another eyes animation to make it loop forever
    - Add the animator to the mascot
3. First make a [sport game script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Sport/SportGame.cs), all sport activities will inherit from this script 
4. Now make different food activities
    1. Apple activity 
        - Place a tree and add an [apple controller script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Sport/AppleController.cs) to it. this will control the apple minigame
        - Assign an apple prefab to the tree that has an [apple script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Sport/Apple.cs) attached to it
        - Place a basket on the ground with a [basket script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Sport/Basket.cs) attached, we will put apples in here\
    2. Barbell activity
        - Place a barbell 3d object and add a [barbell script](https://github.com/IgnaceShoeib/Health-4-All/blob/main/Assets/Scripts/Sport/Barbell.cs) to it.
