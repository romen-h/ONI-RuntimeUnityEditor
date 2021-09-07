# Runtime ~~Unity~~ ONI Editor / Debugging Tools
In-game inspector, editor and interactive console for Oxygen Not Included. Based on t he [Runtime Unity Editor](https://github.com/ManlyMarco/RuntimeUnityEditor) project.

### Features
- GameObject and component browser
- Object inspector that allows modifying values of objects in real time
- ~~REPL C# console~~ *Not upgraded for .NEY 4.7.1 yet*

![preview](https://user-images.githubusercontent.com/39247311/64476158-ce1a4c00-d18b-11e9-97d6-084452cdbf0a.PNG)

### How to use
1. Install BepInEx v4.x or v5.x if you don't have it already. You can download it [here](https://github.com/BepInEx/BepInEx
).
2. Download the latest build from the [Releases](https://github.com/ManlyMarco/RuntimeUnityEditor/releases) page. Make sure to get the correct version for your BepInEx.
3. Extract the BepInEx folder from the archive directly into your game directory (you should already have a BepInEx folder there from previous step). Replace files if asked.
4. To turn on press the F12 key when in-game. A window should appear on top of the game. If it doesn't appear, check logs for errors.

Note: If the plugin fails to load under BepInEx 4 with a type load exception, move RuntimeUnityEditor.Core.dll to BepInEx/core folder.

### How to build
1. Get Visual Studio 2019 (recommended) or the latest version of Visual Studio 2017.
2. Clone the repository.
3. Open the solution in Visual Studio and build the `RuntimeUnityEditor.Core` and `RuntimeUnityEditor.ONI` projects.

Notes:
- If you already have the repository cloned or want to update the mcs submodule you need to run `git submodule update --init --recursive` on your local repository.
- You have to reconnect the references to the Oxygen Not Included "Managed" folder.

---

You can support development of my plugins through my Patreon page: https://www.patreon.com/ManlyMarco
