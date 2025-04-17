using ScheduleOne.GameTime;
using ScheduleOne.Quests;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace QualityOfLife.Patches;

[HarmonyPatch]
public class ContractStartPatch
{
    [HarmonyTargetMethod]
    public static MethodBase TargetMethod() =>
        AccessTools.Method(typeof(Contract), nameof(Contract.Start));

    [HarmonyPostfix]
    public static void Postfix(Contract __instance)
    {
        foreach (var questEntry in __instance.Entries)
        {
            if (questEntry.compassElement != null && questEntry.isActiveAndEnabled)
            {
                questEntry.compassElement.Visible = false;
            }
        }
    }
}

[HarmonyPatch]
public class ContractCheckExpiryPatch
{
    [HarmonyTargetMethod]
    public static MethodBase TargetMethod() =>
        AccessTools.Method(typeof(Contract), nameof(Contract.CheckExpiry));

    [HarmonyPostfix]
    public static void Postfix(Contract __instance)
    {
        foreach (var questEntry in __instance.Entries)
        {
            if (questEntry.compassElement != null)
            {
                bool inWindow = TimeManager.Instance.IsCurrentTimeWithinRange(
                    __instance.DeliveryWindow.WindowStartTime,
                    __instance.DeliveryWindow.WindowEndTime
                );

                Transform icon = questEntry.compassElement.Rect.Find("ContractIcon(Clone)");
                if (icon == null) continue;

                Transform fill = icon.Find("Fill");
                Transform background = icon.Find("Background");

                Color backgroundColor = Color.grey;

                if (__instance.expiryReminderSent)
                {
                    backgroundColor = new Color(0.8f, 0.4f, 0.4f, 0.8f);
                }
                else if (inWindow)
                {
                    backgroundColor = new Color(0.4f, 0.8f, 0.4f, 0.8f);
                }

                if (fill.TryGetComponent(out Image fillImage))
                    fillImage.color = Color.white;

                if (background.TryGetComponent(out Image bgImage))
                    bgImage.color = backgroundColor;

                questEntry.compassElement.Visible =
                    questEntry.ShouldShowPoI() &&
                    questEntry.isActiveAndEnabled &&
                    __instance.QuestState == EQuestState.Active;
            }
        }
    }
}
