using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace QualityOfLife.Menu.ModManager
{
    public static class Config
    {
        public static List<Action<GameObject>> GetInitializers()
        {
            return new List<Action<GameObject>>
            { };
        }
    }
}
