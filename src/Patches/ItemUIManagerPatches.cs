using MelonLoader;
using ScheduleOne.ItemFramework;
using ScheduleOne.UI.Items;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;

namespace Core.Patches;

[HarmonyPatch(typeof(ItemUIManager))]
public class ItemUIManagerPatches
{
    private static ItemSlot cachedSlot;
    private static bool rightClickHeld;

    [HarmonyPostfix]
    [HarmonyPatch(nameof(ItemUIManager.Update))]
    private static void Update_Postfix(ItemUIManager __instance)
    {
        bool ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetMouseButtonDown(1))
        {
            cachedSlot = __instance.HoveredSlot?.assignedSlot;
            if (shift)
            {
                // Maybe Add CRTL + SHIFT + Right Click to quick move step of 5
            }
            else
            {
                if (ctrl && shift && cachedSlot != null && __instance.draggedAmount > 0)
                    __instance.SetDraggedAmount(__instance.draggedAmount - Mathf.Min(5, __instance.draggedAmount));
            }
            rightClickHeld = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightClickHeld = false;
            cachedSlot = null;
        }

        if (!(ctrl && rightClickHeld)) return;

        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) < 0.01f) return;

        int currentAmount = __instance.draggedAmount;
        if (currentAmount <= 0) return;

        int maxAmount = 999;
        var item = cachedSlot?.ItemInstance;
        var data = item?.GetItemData();
        if (data != null)
            maxAmount = Mathf.Max(data.Quantity, 1);

        List<int> steps = new List<int> { 1 };
        for (int i = 5; i <= maxAmount; i += 5)
            steps.Add(i);

        int closest = 0, diff = int.MaxValue;
        for (int i = 0; i < steps.Count; i++)
        {
            int d = Mathf.Abs(steps[i] - currentAmount);
            if (d < diff)
            {
                diff = d;
                closest = i;
            }
        }

        int newIndex = Mathf.Clamp(closest + (scroll > 0f ? 1 : -1), 0, steps.Count - 1);
        int newAmount = steps[newIndex];

        __instance.SetDraggedAmount(newAmount);
        MelonLogger.Msg($"[ItemUI Scroll] New amount: {newAmount}");
    }
}
