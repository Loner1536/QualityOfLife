using ScheduleOne.DevUtilities;
using ScheduleOne.PlayerScripts;
using ScheduleOne.UI.Compass;
using UnityEngine;
using HarmonyLib;

namespace Core.Patches
{
    [HarmonyPatch(typeof(CompassManager))]
    public class CompassPatches
    {
        [HarmonyPatch(nameof(CompassManager.UpdateElement))]
        [HarmonyPostfix]
        private static void Postfix(CompassManager.Element element)
        {
            if (!element.Visible || element.Transform == null) return;

            var playerPosition = PlayerSingleton<PlayerCamera>.Instance.transform.position;
            var elementPosition = element.Transform.position;
            var distance = Vector3.Distance(playerPosition, elementPosition);
            element.DistanceLabel.text = $"{Mathf.CeilToInt(distance)}m";
        }
    }
}
