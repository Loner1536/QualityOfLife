using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace QualityOfLife.Entry;

public class Core : MelonMod
{
    private Dictionary<string, SceneInitSet> sceneInitTable;
    private Dictionary<string, MelonOverrideInitSet> melonOverrideInitTable;
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
                        SceneInitializer.InitializeMainMenu
                    }
            },
            ["Main"] = new SceneInitSet("UI/PauseMenu/Container")
            {
                Actions = new()
                    {
                        SceneInitializer.InitializePauseMenu
                    },
                CleanupActions = new()
                    {
                        SceneInitializer.CleanupPauseMenu
                    }
            },
            ["Tutorial"] = new SceneInitSet("UI/PauseMenu/Container")
            {
                Actions = new()
                    {
                        SceneInitializer.InitializePauseMenu
                    },
                CleanupActions = new()
                    {
                        SceneInitializer.CleanupPauseMenu
                    }
            },
        };

        melonOverrideInitTable = new Dictionary<string, MelonOverrideInitSet>
        {
            ["OnLateUpdate"] = new MelonOverrideInitSet()
            {
                Actions = new()
                    {
                        LateUpdateHandler.HandleLateUpdate
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

public class SceneInitSet
{
    public string ObjectPath;
    public List<Action<GameObject, string>> Actions = new();
    public List<Action<GameObject>> CleanupActions = new();

    public SceneInitSet(string objectPath)
    {
        ObjectPath = objectPath;
    }
}

public class MelonOverrideInitSet
{
    public List<Action> Actions = new();
}
