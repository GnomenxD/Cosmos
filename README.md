# Cosmos
The **CosmosEngine** is a framework build on top of the [MonoGame](https://www.monogame.net/) framework. It has gone through multiple iterations and was actively worked on over a longer period. It has been a personal develop project used while studying software development with expertise in game development. It has been used in various student project.

Cosmos is a 2D only game development framework that uses MonoGame in its core, but have been developed with the intend that when creating a new project you only need to reference the Cosmos framework and not MonoGame in your own project.

Now that I'm no longer actively using it I have decided to put it up publicly on GitHub, hoping it might help others that could be stuggeling getting started using MonoGame. This also allows for others to contribute to the framework and/or fixing other issues and bugs I might have left behind.

I feel like I have documented most of the code in the project properly. I might come back fixing or adding functionality when I find the time and need. But it's no longer an active development of mine.

## Tutorial
Here is a quick **HOW TO** use the Cosmos Framework to create a new game.
The setup process is a little annoying until I figure out how to create the **project templates** I desire, had a few complications at the moment.
1. Create a folder for the entire project (this will be the root directory).
2. Download and place the CosmosEngine folder in the root directory.
3. Create a new MonoGame Project for your targeted application.
4. Add the CosmosEngine project to your **solution** and reference it to your new project.
5. Rename your class and remove any using for MonoGame.
6. Now add CosmosEngine to your using.
7. Your *Game* class should inherit from CosmosEngine.Game now instead.
8. Now you should be ready to develop a game using the Cosmos framework.

### GameBehaviour
To create your own component with custom functionality, create a new class and inherit from CosmosEngine.GameBehaviour. This gives you access to a wide varity of methods that will be automatically be executed.

## To Do
There is a lot missing, most things I never got to incorporate into the Cosmos framework. Here I have compiled a list of missing functionality that I personally would have loved to introduce into the framework. It will also be things that does exist but are either held together with scutchtape and glue or just outdated.

Some of the things listed is something I already have ideas and plans on creating, but unexpected delivery date.
- [ ] **Audio System**
- [ ] **New ContentManager**
    - This will be done using an external tool I'm currently working on called the *Asset Library Builder*, instead of uploading the assets in the ContentManager, they just need to be placed into the project folder and marked with either **Copy if newer** or **Copy always**. This will then generate reference that can be accessed through scripts.
- [ ] **Netcode**
    - Netcode does exist already and it actually do "*work*" through UDP, but it's a mess that wasn't planned correctly, it needs to be updated and fixed to work properly if network ever needs to become a thing in the Cosmos framework.
- [ ] **Physics and Colliders**
- [ ] **Spritesheet Support**
- [ ] **Sprite Animations**
- [ ] **UI Overhault**
- [ ] **Editor View**
    - An editor view with a scene hierarchy, inspector, debug console and more *editor* tools if nessecary. This should be excluded from the actual **release** build.
- [ ] **Input System**
    - The desired Input System already exists in the project, but it has to be rewritten for the new Modules system that was developed. For now use InputManager instead for quick controls.
- [ ] **More Input Support**
    - Support for Controller and Touch.
- [ ] **Prefab and Instantiation**
- [ ] **Scene Management**

More could and properly is going to added to the list.

## Sample Projects
This github includes a few sample projects which is developed with the intend to test different functionality.

