using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace QualityOfLife.Utils.UI;

public static class Fading
{
    public const float DefaultFadeValue = 0.1f;

    public static void FadeDependenciesButtons(GameObject gameObject)
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
            colors.fadeDuration = DefaultFadeValue;
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
