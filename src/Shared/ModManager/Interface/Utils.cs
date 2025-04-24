using UnityEngine;
using MelonLoader;

namespace QualityOfLife.Menu.ModManager.Interface;

public static class Utils
{
    public static void RearrangeMenu(GameObject mainMenu)
    {
        string[] buttonPaths = { "Home/Bank/Settings", "Home/Bank/Credits", "Home/Bank/Quit" };

        foreach (string path in buttonPaths)
        {
            Transform button = mainMenu.transform.Find(path);
            if (button != null && button.TryGetComponent(out RectTransform rect))
                rect.anchoredPosition -= new Vector2(0, 40);
            else if (button != null)
                MelonLogger.Warning($"[ModManager] Failed to get RectTransform for {path}!");
            else
                MelonLogger.Warning($"[ModManager] Failed to find {path} in Main Menu!");
        }
    }
}
