using UnityEngine;
using System;
using System.Collections.Generic;

namespace Core.Shared.SettingsModules
{
    public static class Graphics
    {
        public static List<Action<GameObject>> GetInitializers()
        {
            return new List<Action<GameObject>>
            {
                (go) => Debug.Log("[Settings] Initializing Graphics settings...")
            };
        }
    }
}
