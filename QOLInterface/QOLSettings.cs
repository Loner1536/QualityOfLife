using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace QualityOfLife
{
    public class QOLSettings : MelonMod
    {
        public static void Initialize(GameObject gameObject)
        {
            MelonLogger.Msg("[QOLSettings] Initializing...");

            GameObject settingsMenu = (gameObject.transform.Find("Settings") ?? gameObject.transform.Find("SettingsScreen_Ingame"))?.gameObject;
            if (settingsMenu == null)
            {
                MelonLogger.Warning($"[QOLSettings] 'Settings' GameObject not found in {gameObject}.");
            }
            else
            {
                GameObject settingsContent = settingsMenu.transform.Find("Content").gameObject;
                if (settingsContent == null)
                {
                    MelonLogger.Warning("[QOLSettings] 'Content' GameObject not found in Settings.");
                }
                else
                {
                    GameObject DisplayMenu = settingsContent.transform.Find("Display").gameObject;
                    if (DisplayMenu == null)
                    {
                        MelonLogger.Warning("[QOLSettings] 'Display' GameObject not found in 'Settings'.");
                    }
                    else
                    // DISPLAY MENU SETTINGS
                    {
                        // FPS LIMITER INCREASER
                        GameObject fpsLimiter = DisplayMenu.transform.Find("Target FPS").gameObject;
                        if (fpsLimiter == null)
                        {
                            MelonLogger.Warning("[QOLSettings] 'Target FPS' GameObject not found in 'Settings/Display'.");
                        }
                        else
                        {
                            // FPS LIMITER
                            GameObject fpsSlider = fpsLimiter.transform.Find("Slider").gameObject;
                            if (fpsSlider != null)
                            {
                                Slider fpsSliderComponent = fpsSlider.GetComponent<Slider>();
                                if (fpsSliderComponent != null)
                                {
                                    fpsSliderComponent.minValue = 10;
                                    fpsSliderComponent.maxValue = 360;
                                }
                                else
                                {
                                    MelonLogger.Warning("[QOLSettings] Slider component not found in 'Settings/Display/Target FPS/Slider'.");
                                }
                            }
                            else
                            {
                                MelonLogger.Warning("[QOLSettings] 'Slider' GameObject not found in 'Settings/Display/Target FPS'.");
                            }
                        }
                    }
                    GameObject GraphicsMenu = settingsContent.transform.Find("Graphics").gameObject;
                    if (GraphicsMenu == null)
                    {
                        MelonLogger.Warning("[QOLSettings] 'Graphics' GameObject not found in 'Settings'.");
                    }
                    else
                    // GRAPHICS MENU SETTINGS
                    {
                        // FOV LIMITER
                        GameObject fovLimiter = GraphicsMenu.transform.Find("FOV").gameObject;
                        if (fovLimiter == null)
                        {
                            MelonLogger.Warning("[QOLSettings] 'FOV' GameObject not found in 'Settings/Graphics'.");
                        }
                        else
                        {
                            GameObject fovSlider = fovLimiter.transform.Find("Slider").gameObject;
                            if (fovSlider == null)
                            {
                                MelonLogger.Warning("[QOLSettings] 'Slider' GameObject not found in 'Settings/Graphics'.");
                            }
                            else
                            {
                                Slider fovSliderComponent = fovSlider.GetComponent<Slider>();
                                if (fovSliderComponent == null)
                                {
                                    MelonLogger.Warning("[QOLSettings] Slider component not found in 'Settings/Graphics/Target FPS/Slider'.");
                                }
                                else
                                {
                                    fovSliderComponent.minValue = 40;
                                    fovSliderComponent.maxValue = 120;
                                }
                            }
                        }
                    }
                    GameObject AudioMenu = settingsContent.transform.Find("Audio").gameObject;
                    if (AudioMenu == null)
                    {
                        MelonLogger.Warning("[QOLSettings] 'Audio' GameObject not found in 'Settings'.");
                    }
                    else
                    // AUDIO MENU SETTINGS
                    { }
                    GameObject ControlsMenu = settingsContent.transform.Find("Controls").gameObject;
                    if (ControlsMenu == null)
                    {
                        MelonLogger.Warning("[QOLSettings] 'Controls' GameObject not found in 'Settings'.");
                    }
                    else
                    // CONTROLS MENU SETTINGS
                    { }
                }
            }
        }
    }
}
