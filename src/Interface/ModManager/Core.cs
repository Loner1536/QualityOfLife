using System;
using System.Collections.Generic;
using UnityEngine;

namespace QualityOfLife.Interface.ModManager;

public class Core
{
    public static List<Action<GameObject, string>> GetInitializers()
    {
        var initializers = new List<Action<GameObject, string>>
        {
            (root, scene) => Components.ModManagerButton.Setup(root, scene),
            (root, _) => Components.Utils.RearrangeMenu(root),
        };

        initializers.AddRange(Mods.GetInitializers());
        return initializers;
    }
}
