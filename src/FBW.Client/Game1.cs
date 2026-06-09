using Nez;
using FBW.Client.Settings;

namespace FBW.Client;

public class Game1 : Nez.Core
{
    protected override void Initialize()
    {
        base.Initialize();
        KeybindManager.Initialize();
    }
}