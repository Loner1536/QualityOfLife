using ScheduleOne.DevUtilities;
using ScheduleOne.PlayerScripts;
using ScheduleOne.UI.Compass;
using ScheduleOne.Quests;
using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;
using System.Linq;
using System.Collections.Generic;

using QualityOfLife.Utils.Products;

namespace QualityOfLife.Patches;

[HarmonyPatch(typeof(CompassManager))]
public class CompassPatches
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(CompassManager.UpdateElement))]
    private static void UpdateElement_Postfix(CompassManager.Element element)
    {
        if (!element.Visible || !element.Transform) return;

        var playerPosition = PlayerSingleton<PlayerCamera>.Instance.transform.position;
        var elementPosition = element.Transform.position;
        var distance = Vector3.Distance(playerPosition, elementPosition);
        element.DistanceLabel.text = $"{Mathf.CeilToInt(distance)}m";
    }
}
