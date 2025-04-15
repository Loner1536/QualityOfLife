using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Core
{
    public class QualityOfLife : MelonMod
    {
        private Dictionary<string, SceneInitSet> sceneInitTable;
        private HashSet<string> initializedScenes = new HashSet<string>();

        public override void OnInitializeMelon()
        {
            sceneInitTable = new Dictionary<string, SceneInitSet>();

            var mainAndTutorialInit = new SceneInitSet("UI/PauseMenu/Container");
            mainAndTutorialInit.Actions.Add((go) => MelonLogger.Msg("UIUtils: Smooth fade"));
            mainAndTutorialInit.Actions.Add((go) => Core.InterfaceUtils.Fades.Initialize(go));
            mainAndTutorialInit.Actions.Add((go) => MelonLogger.Msg("Executing action: Main/Tutorial only action"));

            var menuInit = new SceneInitSet("MainMenu");
            menuInit.Actions.Add((go) => MelonLogger.Msg("UIUtils: Smooth fade"));
            menuInit.Actions.Add((go) => Core.InterfaceUtils.Fades.Initialize(go));
            menuInit.Actions.Add((go) => MelonLogger.Msg("Menu only: Init logic"));

            sceneInitTable["Menu"] = menuInit;
            sceneInitTable["Main"] = mainAndTutorialInit;
            sceneInitTable["Tutorial"] = mainAndTutorialInit;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (initializedScenes.Contains(sceneName))
            {
                MelonLogger.Msg($"[Core] {sceneName} already initialized. Skipping.");
                return;
            }

            if (sceneInitTable.TryGetValue(sceneName, out var initSet))
            {
                InitializeScene(initSet.ObjectPath, initSet.GetAllActions());
                initializedScenes.Add(sceneName);
            }
            else
            {
                MelonLogger.Msg($"[Core] No initialization registered for scene '{sceneName}'");
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (initializedScenes.Contains(sceneName))
            {
                initializedScenes.Remove(sceneName);
            }
        }

        private void InitializeScene(string objectPath, IEnumerable<Action<GameObject>> actions)
        {
            GameObject sceneObject = GameObject.Find(objectPath);
            if (sceneObject == null)
            {
                MelonLogger.Warning($"[Core] {objectPath} GameObject not found in scene.");
                return;
            }

            foreach (var action in actions)
            {
                action?.Invoke(sceneObject);
            }
        }
    }

    class SceneInitSet
    {
        public string ObjectPath;
        public List<Action<GameObject>> Actions = new List<Action<GameObject>>();

        public SceneInitSet(string objectPath)
        {
            ObjectPath = objectPath;
        }

        public IEnumerable<Action<GameObject>> GetAllActions()
        {
            foreach (var action in Actions)
                yield return action;
        }
    }
}
