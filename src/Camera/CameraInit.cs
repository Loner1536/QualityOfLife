using UnityEngine;
using System.Collections;
using ScheduleOne.DevUtilities;
using ScheduleOne.PlayerScripts;
using ScheduleOne.UI;

namespace QualityOfLife.Camera;

public static class CameraInit
{
    public static IEnumerator WaitForPlayerAndInitialize(System.Action onReady)
    {
        while (Player.Local == null || Player.Local.gameObject == null)
            yield return null;

        yield return new WaitForSeconds(1f);

        var cam = PlayerSingleton<PlayerCamera>.instance;

        if (cam != null)
        {
            cam.SmoothLook = true;
            cam.SmoothLookSpeed = QualityOfLife.Preferences.Camera.SmoothSensitivity;

            CameraController.InjectPlayerCamera(cam);
            CameraController.InjectConsole(Object.FindObjectOfType<ConsoleUI>());

            onReady?.Invoke();
        }
        else
        {
            MelonLoader.MelonLogger.Warning("PlayerCamera not found. Skipping camera override.");
        }
    }
}
