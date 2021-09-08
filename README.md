# Runtime ~~Unity~~ ONI Editor / Debugging Tools
In-game inspector, editor and interactive console for Oxygen Not Included.  
Based on the [Runtime Unity Editor](https://github.com/ManlyMarco/RuntimeUnityEditor) project.

### Features
- GameObject and component browser
- Object inspector that allows modifying values of objects in real time
- ~~REPL C# console~~ *Not upgraded for .NET 4.7.1 yet*

![preview](https://user-images.githubusercontent.com/39247311/64476158-ce1a4c00-d18b-11e9-97d6-084452cdbf0a.PNG)

### How to build
1. Get Visual Studio 2019 (recommended) or the latest version of Visual Studio 2017.
2. Clone the repository.
3. Open the solution in Visual Studio and build the `RuntimeUnityEditor.Core` and `RuntimeUnityEditor.ONI` projects.

Notes:
- You have to reconnect the references to the Oxygen Not Included "Managed" folder.
- After installing the mod, copy the `mod.yaml` and `mod_info.yaml` files to the mod folder.
