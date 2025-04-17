using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace QualityOfLife
{
    public class Core : MelonMod
    {
        private Dictionary<string, SceneInitSet> sceneInitTable;
        private HashSet<string> initializedScenes = new HashSet<string>();

        public override void OnInitializeMelon()
        {
            sceneInitTable = new Dictionary<string, SceneInitSet>();

            var mainAndTutorialInit = new SceneInitSet("UI/PauseMenu/Container");
            mainAndTutorialInit.Actions.Add((PauseMenu, sceneName) => QualityOfLife.Utils.UI.Fading.FadeDependenciesButtons(PauseMenu));
            mainAndTutorialInit.Actions.Add((PauseMenu, sceneName) => QualityOfLife.Shared.Settings.Initialize(PauseMenu, sceneName));

            var menuInit = new SceneInitSet("MainMenu");
            menuInit.Actions.Add((MainMenu, _) => QualityOfLife.Utils.UI.Fading.FadeDependenciesButtons(MainMenu));
            menuInit.Actions.Add((MainMenu, sceneName) => QualityOfLife.Shared.Settings.Initialize(MainMenu, sceneName));
            menuInit.Actions.Add((MainMenu, _) => QualityOfLife.Menu.Core.Initialize(MainMenu));


            sceneInitTable["Menu"] = menuInit;
            sceneInitTable["Main"] = mainAndTutorialInit;
            sceneInitTable["Tutorial"] = mainAndTutorialInit;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (initializedScenes.Contains(sceneName))
                return;

            if (sceneInitTable.TryGetValue(sceneName, out var initSet))
            {
                InitializeScene(initSet.ObjectPath, initSet.GetAllActions(), sceneName);
                initializedScenes.Add(sceneName);
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (initializedScenes.Contains(sceneName))
            {
                initializedScenes.Remove(sceneName);
            }
        }

        private void InitializeScene(string objectPath, IEnumerable<Action<GameObject, string>> actions, string sceneName)
        {
            GameObject sceneObject = GameObject.Find(objectPath);
            if (sceneObject == null) return;

            foreach (var action in actions)
            {
                action?.Invoke(sceneObject, sceneName);
            }
        }
    }

    class SceneInitSet
    {
        public string ObjectPath;
        public List<Action<GameObject, string>> Actions = new List<Action<GameObject, string>>();

        public SceneInitSet(string objectPath)
        {
            ObjectPath = objectPath;
        }

        public IEnumerable<Action<GameObject, string>> GetAllActions()
        {
            foreach (var action in Actions)
                yield return action;
        }
    }
}
