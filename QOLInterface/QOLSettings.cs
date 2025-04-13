using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace QualityOfLife
{
    public class QOLSettings : MelonMod
    {
        public static void Initialize(GameObject mainMenu)
        {
            MelonLogger.Msg("[QOLSettings] Initializing...");

            Transform settingsMenu = mainMenu.transform.Find("Settings");
            if (settingsMenu == null)
            {
                MelonLogger.Warning("[QOLSettings] 'Settings' GameObject not found in MainMenu.");
            }
            else
            {
                GameObject settingsContent = settingsMenu.Find("Content").gameObject;
                if (settingsContent == null)
                {
                    MelonLogger.Warning("[QOLSettings] 'Content' GameObject not found in Settings.");
                }
                else
                {
                    GameObject DisplayMenu = settingsContent.transform.Find("Display").gameObject;
                    if (DisplayMenu == null)
                    {
                        MelonLogger.Warning("[QOLSettings] 'Display' GameObject not found in Settings.");
                    }
                    else
                    {
                        // FPS LIMITER INCREASER
                        GameObject fpsLimiter = DisplayMenu.transform.Find("Target FPS").gameObject;
                        if (fpsLimiter == null)
                        {
                            MelonLogger.Warning("[QOLSettings] 'Target FPS' GameObject not found in Display Menu.");
                        }
                        else
                        // DISPLAY MENU SETTINGS
                        {
                            // FPS Limiter Settings
                            GameObject fpsSlider = fpsLimiter.transform.Find("Slider").gameObject;
                            if (fpsSlider != null)
                            {
                                Slider fpsSliderComponent = fpsLimiter.GetComponent<Slider>();
                                if (fpsSliderComponent != null)
                                {
                                    fpsSliderComponent.minValue = 10;
                                    fpsSliderComponent.maxValue = 360;
                                    MelonLogger.Msg("[QOLSettings] 'Target FPS' Slider initialized.");
                                }
                                else
                                {
                                    MelonLogger.Warning("[QOLSettings] 'Target FPS' Slider component not found.");
                                }
                            }
                            else
                            {
                                MelonLogger.Warning("[QOLSettings] 'Target FPS' Slider GameObject not found.");
                            }
                        }
                    }
                    GameObject GraphicsMenu = settingsContent.transform.Find("Graphics").gameObject;
                    if (GraphicsMenu == null)
                    {
                        MelonLogger.Warning("[QOLSettings] 'Graphics' GameObject not found in Settings.");
                    }
                    else
                    // GRAPHICS MENU SETTINGS
                    { }
                    GameObject AudioMenu = settingsContent.transform.Find("Audio").gameObject;
                    if (AudioMenu == null)
                    {
                        MelonLogger.Warning("[QOLSettings] 'Audio' GameObject not found in Settings.");
                    }
                    else
                    // AUDIO MENU SETTINGS
                    { }
                    GameObject ControlsMenu = settingsContent.transform.Find("Controls").gameObject;
                    if (ControlsMenu == null)
                    {
                        MelonLogger.Warning("[QOLSettings] 'Controls' GameObject not found in Settings.");
                    }
                    else
                    // CONTROLS MENU SETTINGS
                    { }
                }
            }
        }
    }
}
