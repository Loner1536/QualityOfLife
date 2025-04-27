using UnityEngine;

namespace QualityOfLife;

public class SceneInitializer
{
    public static void InitializeMainMenu(GameObject mainMenu, string scene)
    {
        Utility.Interface.FadeToDependencies.Add(mainMenu, 0.1);
        Interface.Settings.Core.Initialize(mainMenu, scene);

        foreach (var action in Interface.ModManager.Core.GetInitializers())
            action?.Invoke(mainMenu, scene);

        Interface.IconLoaders.Core.Initilize();
    }

    public static void InitializePauseMenu(GameObject pauseMenu, string scene)
    {
        Utility.Interface.FadeToDependencies.Add(pauseMenu, 0.1);
        Interface.Settings.Core.Initialize(pauseMenu, scene);

        foreach (var action in Interface.ModManager.Core.GetInitializers())
            action?.Invoke(pauseMenu, scene);

        QualityOfLife._Player.Camera.Core.Initialize();
    }

    public static void CleanupPauseMenu(GameObject pauseMenu)
    {
        QualityOfLife._Player.Camera.Core.Destory();
    }
}
