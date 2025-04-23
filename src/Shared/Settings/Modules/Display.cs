using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace QualityOfLife.Modules.Settings;

public static class Display
{
    public static List<Action<GameObject>> GetInitializers()
    {
        var initializers = new List<Action<GameObject>>
        {
            InitializeFPSLimiter
        };

        return initializers;
    }

    private static void InitializeFPSLimiter(GameObject settingsMenu)
    {
        Slider fpsLimiterSliderComponent = GetFPSLimiterSlider(settingsMenu)?.GetComponent<Slider>();

        if (fpsLimiterSliderComponent != null)
        {
            fpsLimiterSliderComponent.maxValue = 360;
        }
        else
        {
            MelonLogger.Warning("[Modules.Settings.Display] FPS Limiter Slider component not found. Failed to change max.");
        }
    }

    private static GameObject GetFPSLimiterSlider(GameObject settingsMenu)
    {
        GameObject fpsLimiterSlider = settingsMenu.transform.Find("Content/Display/Target FPS/Slider")?.gameObject;
        if (fpsLimiterSlider != null)
        {
            return fpsLimiterSlider;
        }
        else
        {
            MelonLogger.Warning("[Modules.Settings.Display] FPS Limiter GameObject not found. Skipping initialization.");
            return null;
        }
    }
}
