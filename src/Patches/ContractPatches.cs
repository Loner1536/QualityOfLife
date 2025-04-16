using System.Collections.Generic;
using System.Reflection;
using ScheduleOne.GameTime;
using ScheduleOne.Quests;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ContractCompassMarkersPatch
{
    [HarmonyPatch(typeof(Contract))]
    public class ContractPatch
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(Contract), "Start");
            yield return AccessTools.Method(typeof(Contract), "CheckExpiry");
        }

        [HarmonyPostfix]
        private static void Postfix(MethodBase __originalMethod, Contract __instance)
        {
            if (__originalMethod.Name == "Start")
            {
                foreach (var questEntry in __instance.Entries)
                {
                    if (questEntry.compassElement != null && questEntry.isActiveAndEnabled)
                    {
                        questEntry.compassElement.Visible = false;
                    }
                }
                return;
            }

            if (__originalMethod.Name == "CheckExpiry")
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
}
