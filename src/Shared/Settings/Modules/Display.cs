using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace Core.Shared.SettingsModules
{
    public static class Display
    {
        public static List<Action<GameObject>> GetInitializers()
        {
            return new List<Action<GameObject>>
            {
                (settingsMenu) => InitializeFPSLimiter(settingsMenu)
            };
        }

        private static void InitializeFPSLimiter(GameObject settingsMenu)
        {
            MelonLogger.Msg($"Found {settingsMenu}");
            Slider fpsLimiterSliderComponent = GetFPSLimiterSlider(settingsMenu)?.GetComponent<Slider>();

            if (fpsLimiterSliderComponent != null)
            {
                fpsLimiterSliderComponent.maxValue = 360;
            }
            else
            {
                MelonLogger.Warning("[Settings] FPS Limiter Slider component not found. Failed to change max.");
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
                MelonLogger.Warning("[Settings] FPS Limiter GameObject not found. Skipping initialization.");
                return null;
            }
        }
    }
}
