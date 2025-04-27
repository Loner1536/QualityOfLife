using UnityEngine;

namespace QualityOfLife._Player.Camera;

public static class Core
{
    private static bool inMainGame;
    private static bool cameraOverride;
    private static bool hasPlayerSpawned;

    public static void Initialize()
    {
        inMainGame = true;
        if (cameraOverride && hasPlayerSpawned) return;

        Application.focusChanged += CameraFocusHandler.OnFocusChanged;
        MelonLoader.MelonCoroutines.Start(CameraInit.WaitForPlayerAndInitialize(() =>
        {
            hasPlayerSpawned = true;
            cameraOverride = true;
        }));
    }

    public static void OnLateUpdate()
    {
        if (inMainGame && cameraOverride)
        {
            CameraController.Update();
        }
    }

    public static void Destory()
    {
        inMainGame = false;
        Application.focusChanged -= CameraFocusHandler.OnFocusChanged;
    }
}
