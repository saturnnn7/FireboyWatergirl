using FBW.Client.Scenes;
using FBW.Client.Settings;
using Nez;

namespace FBW.Client;

public class Game1 : Nez.Core
{
    protected override void Initialize()
    {
        base.Initialize();

        KeybindManager.Initialize();

        // Launch directly into GameScene for now
        // Later: start from MenuScene
        Scene = new GameScene();
    }
}