using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace QualityOfLife.Shared.Settings;

public static class Audio
{
    public static List<Action<GameObject>> GetInitializers()
    {
        var initializers = new List<Action<GameObject>>
        { };

        return initializers;
    }
}
