using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace QualityOfLife;

public class Core : MelonMod
{
    private Dictionary<string, SceneInitSet> sceneInitTable;
    private HashSet<string> initializedScenes = new();

    public override void OnInitializeMelon()
    {
        MelonLogger.Msg($"{QualityOfLife.BuildInfo.Name} {QualityOfLife.BuildInfo.Version} initializing...");
        sceneInitTable = new Dictionary<string, SceneInitSet>
        {
            ["Menu"] = new SceneInitSet("MainMenu")
            {
                Actions = new()
                    {
                        (mainMenu, _) => QualityOfLife.Utils.UI.Fading.FadeDependenciesButtons(mainMenu),
                        (mainMenu, scene) => QualityOfLife.Shared.Settings.Initialize(mainMenu, scene),
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
                        (pauseMenu, scene) => QualityOfLife.Shared.Settings.Initialize(pauseMenu, scene)
                    },
                CleanupActions = new()
                { }
            },
            ["Tutorial"] = new SceneInitSet("UI/PauseMenu/Container")
            {
                Actions = new()
                    {
                        (pauseMenu, scene) => QualityOfLife.Utils.UI.Fading.FadeDependenciesButtons(pauseMenu),
                        (pauseMenu, scene) => QualityOfLife.Shared.Settings.Initialize(pauseMenu, scene)
                    },
                CleanupActions = new()
                { }
            }
        };
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
