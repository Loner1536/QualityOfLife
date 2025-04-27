using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace QualityOfLife.Utility.Interface;

public static class FadeToDependencies
{
    public static void Add(GameObject gameObject, double fadeValue)
    {
        if (gameObject == null)
        {
            Debug.LogWarning("[Animations] GameObject is null. Initialization aborted.");
            return;
        }

        Debug.Log("[Animations] Initializing smooth fade on all descendant buttons...");

        var buttons = new List<Button>();
        AddButtonsRecursive(gameObject.transform, buttons);

        foreach (var button in buttons)
        {
            if (button == null) continue;
            var colors = button.colors;
            colors.fadeDuration = (float)fadeValue;
            button.colors = colors;
        }
    }

    private static void AddButtonsRecursive(Transform current, List<Button> buttons)
    {
        if (current == null) return;

        Button button = current.GetComponent<Button>();
        if (button != null)
            buttons.Add(button);

        foreach (Transform child in current)
            AddButtonsRecursive(child, buttons);
    }
}
