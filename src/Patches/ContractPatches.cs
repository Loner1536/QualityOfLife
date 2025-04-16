using ScheduleOne.GameTime;
using ScheduleOne.Quests;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ContractCompassMarkersPatch
{
    [HarmonyPatch(typeof(Contract), nameof(Contract.Start))]
    public class ContractStartPatch
    {
        [HarmonyPostfix]
        private static void Postfix(Contract __instance)
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

    [HarmonyPatch(typeof(Contract), nameof(Contract.CheckExpiry))]
    public class ContractCheckExpiryPatch
    {
        [HarmonyPostfix]
        private static void Postfix(Contract __instance)
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

                    Image fillImage = fill.GetComponent<Image>();
                    if (fillImage != null)
                        fillImage.color = Color.white;

                    Image bgImage = background.GetComponent<Image>();
                    if (bgImage != null)
                        bgImage.color = backgroundColor;

                    questEntry.compassElement.Visible =
                        questEntry.ShouldShowPoI() &&
                        questEntry.isActiveAndEnabled &&
                        __instance.QuestState == EQuestState.Active;
                }
            }
        }
    }
}
