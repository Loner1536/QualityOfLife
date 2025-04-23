using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace QualityOfLife.Modules.Settings;

public static class Graphics
{
    public static List<Action<GameObject>> GetInitializers()
    {
        var initializers = new List<Action<GameObject>>
        { };

        return initializers;
    }
}
