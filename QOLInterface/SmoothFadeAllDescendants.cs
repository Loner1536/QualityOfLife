using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace QualityOfLife
{
    public class SmoothFadeAllDescendants : MelonMod
    {
        public const float DefaultFadeValue = 0.1f;

        public static void Initialize(GameObject gameObject)
        {
            if (gameObject == null)
            {
                MelonLogger.Warning("[SmoothFadeAllDescendants] GameObject is null. Initialization aborted.");
                return;
            }
            MelonLogger.Msg("[SmoothFadeAllDescendants] Initializing...");

            List<Button> allButtons = GetAllButtonsUnder(gameObject);
            foreach (var btn in allButtons)
            {
                SetFadeValue(btn);
            }
        }

        private static List<Button> GetAllButtonsUnder(GameObject root)
        {
            var buttons = new List<Button>();
            if (root != null)
            {
                AddButtonsRecursive(root.transform, buttons);
            }
            return buttons;
        }

        private static void AddButtonsRecursive(Transform current, List<Button> buttons)
        {
            if (current == null) return;

            Button button = current.GetComponent<Button>();
            if (button != null)
            {
                buttons.Add(button);
            }

            foreach (Transform child in current)
            {
                AddButtonsRecursive(child, buttons);
            }
        }

        private static void SetFadeValue(Button button)
        {
            if (button == null) return;

            ColorBlock colors = button.colors;
            colors.fadeDuration = DefaultFadeValue;
            button.colors = colors;
        }
    }
}
