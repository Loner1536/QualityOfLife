using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace QualityOfLife
{
    public class Core : MelonMod
    {
        private Dictionary<string, SceneInitSet> sceneInitTable;
        private Dictionary<string, MelonOverrideInitSet> melonOverrideInitTable;
        private HashSet<string> initializedScenes = new();

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg($"{QualityOfLife.BuildInfo.Name} {QualityOfLife.BuildInfo.Version} initializing...");

            // Initialize sceneInitTable (Scene initialization actions)
            sceneInitTable = new Dictionary<string, SceneInitSet>
            {
                ["Menu"] = new SceneInitSet("MainMenu")
                {
                    Actions = new()
                    {
                        (mainMenu, _) => QualityOfLife.Utils.UI.Fading.FadeDependenciesButtons(mainMenu),
                        (mainMenu, scene) => QualityOfLife.Shared.Settings.Core.Initialize(mainMenu, scene),
                        (mainMenu, _) => QualityOfLife.Menu.Core.Initialize(mainMenu),
                        (_, _) => QualityOfLife.IconLoaders.Core.Initilize()
                    },
                    CleanupActions = new()
                    { }
                },
                ["Main"] = new SceneInitSet("UI/PauseMenu/Container")
                {
                    Actions = new()
                    {
                        (pauseMenu, scene) => QualityOfLife.Utils.UI.Fading.FadeDependenciesButtons(pauseMenu),
                        (pauseMenu, scene) => QualityOfLife.Shared.Settings.Core.Initialize(pauseMenu, scene),
                        (_, _) => QualityOfLife.Camera.Core.Initialize()
                    },
                    CleanupActions = new()
                    {
                        (_) => QualityOfLife.Camera.Core.Destory()
                    }
                },
                ["Tutorial"] = new SceneInitSet("UI/PauseMenu/Container")
                {
                    Actions = new()
                    {
                        (pauseMenu, scene) => QualityOfLife.Utils.UI.Fading.FadeDependenciesButtons(pauseMenu),
                        (pauseMenu, scene) => QualityOfLife.Shared.Settings.Core.Initialize(pauseMenu, scene),
                        (_, _) => QualityOfLife.Camera.Core.Initialize()
                    },
                    CleanupActions = new()
                    {
                        (_) => QualityOfLife.Camera.Core.Destory()
                    }
                },
            };

            melonOverrideInitTable = new Dictionary<string, MelonOverrideInitSet>
            {
                ["OnLateUpdate"] = new MelonOverrideInitSet()
                {
                    Actions = new()
                    {
                        () => QualityOfLife.Camera.Core.OnLateUpdate()
                    }
                },
            };
        }

        public override void OnLateUpdate()
        {
            string methodName = nameof(OnLateUpdate);
            if (melonOverrideInitTable.TryGetValue(methodName, out var overrideEntry))
            {
                foreach (var action in overrideEntry.Actions)
                {
                    action?.Invoke();
                }
            }
        }


        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (initializedScenes.Contains(sceneName)) return;

            if (sceneInitTable.TryGetValue(sceneName, out var initSet))
            {
                GameObject sceneObject = GameObject.Find(initSet.ObjectPath);
                if (sceneObject == null) return;

                foreach (var action in initSet.Actions)
                    action?.Invoke(sceneObject, sceneName);

                initializedScenes.Add(sceneName);
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (!initializedScenes.Remove(sceneName)) return;

            if (sceneInitTable.TryGetValue(sceneName, out var initSet))
            {
                GameObject sceneObject = GameObject.Find(initSet.ObjectPath);
                if (sceneObject == null) return;

                foreach (var cleanup in initSet.CleanupActions)
                    cleanup?.Invoke(sceneObject);
            }
        }
    }

    class SceneInitSet
    {
        public string ObjectPath;
        public List<Action<GameObject, string>> Actions = new();
        public List<Action<GameObject>> CleanupActions = new();

        public SceneInitSet(string objectPath)
        {
            ObjectPath = objectPath;
        }
    }

    class MelonOverrideInitSet
    {
        public List<Action> Actions = new();
    }
}
