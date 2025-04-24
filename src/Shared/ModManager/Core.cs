using System;
using System.Collections.Generic;
using UnityEngine;

namespace QualityOfLife.Menu.ModManager;

public class Core
{
    public static List<Action<GameObject>> GetInitializers()
    {
        var initializers = new List<Action<GameObject>>
            {
                Interface.ModManagerButton.Setup,
                Interface.Utils.RearrangeMenu,
            };

        initializers.AddRange(Mods.GetInitializers());
        return initializers;
    }
}
