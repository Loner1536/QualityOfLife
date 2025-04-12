using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace QualityOfLife
{
    public class QOLSettings : MelonMod
    {
        public static void Initialize(GameObject mainMenu)
        {
            MelonLogger.Msg("[QOLMainMenu] Initializing...");

            Transform settings = mainMenu.transform.Find("Settings");
            if (settings == null)
            {
                MelonLogger.Warning("[QOLMainMenu] 'Settings' GameObject not found in MainMenu.");
            }
            else
            {
                Transform framerateSliderTransform = settings.Find("Content/Display/Target FPS/Slider");
                if (framerateSliderTransform == null)
                {
                    MelonLogger.Warning("[QOLMainMenu] 'Slider' under 'Target FPS' not found.");
                }
                else
                {
                    Slider framerateSlider = framerateSliderTransform.GetComponent<Slider>();
                    if (framerateSlider == null)
                    {
                        MelonLogger.Warning("[QOLMainMenu] 'Target FPS' Slider component is missing.");
                    }
                    else framerateSlider.maxValue = 360;
                    return;
                }
            }
        }
    }
}
