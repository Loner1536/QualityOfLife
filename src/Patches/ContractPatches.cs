using ScheduleOne.GameTime;
using ScheduleOne.Quests;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Collections.Generic;

using QualityOfLife.Utility.Products;

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
    private static List<Color> drugColors = new List<Color>
    {
        new Color(0.5f, 0.8f, 0.5f), // Weed color
        new Color(0.8f, 0.6f, 0.8f), // Meth color
        new Color(0.7f, 0.7f, 0.9f)  // Cocaine color
    };

    [HarmonyTargetMethod]
    public static MethodBase TargetMethod() =>
        AccessTools.Method(typeof(Contract), nameof(Contract.CheckExpiry));

    [HarmonyPostfix]
    public static void Postfix(Contract __instance)
    {
        foreach (QuestEntry questEntry in __instance.Entries)
        {
            if (questEntry != null && questEntry.compassElement != null)
            {
                if (questEntry != null)
                {
                    string drugName = GetDrugName.Get(questEntry.PoI.MainText);
                    string drugType = GetDrugType.Get(drugName);
                    int drugIndex = GetDrugIndex(drugType);

                    GameObject JournalContainerIcon = questEntry.ParentQuest.journalEntry.Find("IconContainer/ContractIcon(Clone)").gameObject;
                    GameObject CompassContainerIcon = questEntry.compassElement.Rect.Find("ContractIcon(Clone)").gameObject;
                    GameObject MapPoIContainerIcon = questEntry.PoI.IconContainer?.gameObject;
                    GameObject QuestContainerIcon = questEntry.ParentQuest.hudUI?.gameObject;

                    UpdateIconsSprite(CompassContainerIcon, drugType, __instance, questEntry);
                    UpdateIconsSprite(JournalContainerIcon, drugType, __instance, questEntry);
                    UpdateIconsSprite(MapPoIContainerIcon, drugType, __instance, questEntry);
                    UpdateIconsSprite(QuestContainerIcon, drugType, __instance, questEntry);
                }
            }
        }
    }

    private static void UpdateIconsSprite(GameObject root, string drugType, Contract contract, QuestEntry questEntry)
    {
        if (root == null) return;

        int drugIndex = GetDrugIndex(drugType);

        foreach (Image image in root.GetComponentsInChildren<Image>(true))
        {
            if (image == null) continue;

            if (image.gameObject.name == "Background")
            {
                if (image.sprite != null && image.sprite.name == "Circle")
                {
                    if (drugIndex >= 0)
                    {
                        bool inWindow = TimeManager.Instance.IsCurrentTimeWithinRange(
                            contract.DeliveryWindow.WindowStartTime,
                            contract.DeliveryWindow.WindowEndTime
                        );

                        Color backgroundColor = Color.grey;

                        if (contract.expiryReminderSent)
                        {
                            backgroundColor = new Color(0.8f, 0.4f, 0.4f, 0.8f);
                        }
                        else if (inWindow)
                        {
                            backgroundColor = drugColors[drugIndex];
                        }
                        image.color = backgroundColor;
                    }
                }
            }
            else if (image.gameObject.name == "Fill")
            {
                if (drugType == "weed")
                {
                    Sprite weedIcon = Interface.IconLoaders.Core.GetIcon("Drug", "weedIcon");
                    if (weedIcon != null)
                    {
                        image.sprite = weedIcon;
                        image.color = Color.white;
                        image.rectTransform.sizeDelta = new Vector2(10, 10);
                        image.rectTransform.anchoredPosition = new Vector2(1, 0);
                    }
                }
                else if (drugType == "meth")
                {
                    Sprite methIcon = Interface.IconLoaders.Core.GetIcon("Drug", "methIcon");
                    if (methIcon != null)
                    {
                        image.sprite = methIcon;
                        image.color = Color.white;
                        image.rectTransform.sizeDelta = new Vector2(10, 10);
                    }
                }
                else if (drugType == "cocaine")
                {
                    Sprite cocaineIcon = Interface.IconLoaders.Core.GetIcon("Drug", "cocaineIcon");
                    if (cocaineIcon != null)
                    {
                        image.sprite = cocaineIcon;
                        image.color = Color.white;
                        image.rectTransform.sizeDelta = new Vector2(15, 15);
                    }
                }

                questEntry.compassElement.Visible =
                    questEntry.ShouldShowPoI() &&
                    questEntry.isActiveAndEnabled &&
                    contract.QuestState == EQuestState.Active;
            }
        }
    }

    private static int GetDrugIndex(string drugType)
    {
        if (drugType == "weed") return 0;
        if (drugType == "meth") return 1;
        if (drugType == "cocaine") return 2;
        return -1;
    }
}
