# BirdUtility

#The scene is located in flight/assets/scenes/Demo, or using Unity open the Flight folder to open the project

#To add the functionality into an existing project, add the Bird.cs, PlayerBird.cs and AIBird.cs files to the project. 

#To make a bird create an empty gameobject by right clicking in the hierarchy and click create empty.

#To make it an ai controlled bird, who still need some work on them, add the AIBird script to the object. 

#If you want the player to control the bird add the PlayerBird script instead and then make the main camera the child of said object by dragging the main camera object in the hierarchy onto the new gameobject.

#I made it so the script generates the birds body automatically using the same module as the one I showed in class, if you want to remove it and add your own delete lines 24-39, or everything in between "#region BirdModule" (line 24) and "#endregion" (line 39) in Bird.cs and add the module to the bird gameObject
