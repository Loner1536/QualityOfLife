using MelonLoader;
using UnityEngine;

namespace Core
{
    public class QualityOfLife : MelonMod
    {
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            switch (sceneName)
            {
                case "Menu":
                    InitializeScene("MainMenu");
                    break;

                case "Main":
                    InitializeScene("UI/PauseMenu/Container");
                    break;

                default:
                    break;
            }
        }

        private void InitializeScene(string objectPath)
        {
            GameObject sceneObject = GameObject.Find(objectPath);
            if (sceneObject == null)
            {
                MelonLogger.Warning($"[Core] {objectPath} GameObject not found in scene.");
                return;
            }

        }
    }
}
