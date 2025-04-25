using System;
using System.Collections.Generic;
using UnityEngine;

namespace QualityOfLife.Menu.ModManager;

public class Core
{
    public static List<Action<GameObject, string>> GetInitializers()
    {
        var initializers = new List<Action<GameObject, string>>
        {
            (root, scene) => Interface.ModManagerButton.Setup(root, scene),
            (root, _) => Interface.Utils.RearrangeMenu(root),
        };

        initializers.AddRange(Mods.GetInitializers());
        return initializers;
    }
}
