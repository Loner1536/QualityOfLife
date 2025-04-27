using UnityEngine;
using MelonLoader;

namespace QualityOfLife.Interface.ModManager.Components;

public static class Utils
{
    public static void RearrangeMenu(GameObject root)
    {
        GameObject[] buttons = {
                FindFirstValid(root.transform, "Home/Bank/Settings", "Container/Bank/Settings"),
                FindFirstValid(root.transform, "Home/Bank/Credits", "Container/Bank/Stuck"),
                FindFirstValid(root.transform, "Home/Bank/Quit", "Container/Bank/Quit"),
                root.transform.Find("Container/Bank/Feedback")?.gameObject
            };

        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                if (button.TryGetComponent(out RectTransform rect))
                {
                    rect.anchoredPosition -= new Vector2(0, 40);
                }
                else
                {
                    MelonLogger.Warning($"[ModManager] Failed to get RectTransform for '{button.name}'!");
                }
            }
        }
    }

    public static GameObject FindFirstValid(Transform root, string path1, string path2)
    {
        return root.Find(path1)?.gameObject ?? root.Find(path2)?.gameObject;
    }
}
