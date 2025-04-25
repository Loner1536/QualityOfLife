using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace QualityOfLife.Menu.ModManager;

public static class Mods
{
    public static List<Action<GameObject, string>> GetInitializers()
    {
        return new List<Action<GameObject, string>>
        {
            // Add your Mod Initializers here, making sure they all accept both GameObject and string
        };
    }
}
