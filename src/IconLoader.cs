using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace QualityOfLife.IconLoaders;

public class Core
{
    private static Dictionary<string, Func<Dictionary<string, Sprite>>> categoryLoaders = new Dictionary<string, Func<Dictionary<string, Sprite>>>();
    private static Dictionary<string, Dictionary<string, Sprite>> loadedIcons = new Dictionary<string, Dictionary<string, Sprite>>();

    public static void Initilize()
    {
        categoryLoaders["Drug"] = DrugIconLoader.LoadIcons;

        foreach (var categoryPair in categoryLoaders)
        {
            string categoryName = categoryPair.Key;
            Func<Dictionary<string, Sprite>> loaderFunction = categoryPair.Value;
            Dictionary<string, Sprite> icons = loaderFunction?.Invoke();

            if (icons != null && icons.Count > 0)
            {
                loadedIcons[categoryName] = icons;
            }
            else
            {
                MelonLogger.Warning($"[IconLoader] No icons loaded for category: {categoryName}");
            }
        }
    }

    public static Sprite GetIcon(string category, string iconName)
    {
        if (loadedIcons.ContainsKey(category) && loadedIcons[category].ContainsKey(iconName))
        {
            return loadedIcons[category][iconName];
        }
        MelonLogger.Error($"[IconLoader] Icon not found in category '{category}': {iconName}");
        return null;
    }

    public static Dictionary<string, Sprite> GetCategoryIcons(string category)
    {
        if (loadedIcons.ContainsKey(category))
        {
            return loadedIcons[category];
        }
        MelonLogger.Error($"[IconLoader] Category not found: {category}");
        return null;
    }
}
