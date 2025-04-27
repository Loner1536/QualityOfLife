using ScheduleOne.DevUtilities;
using ScheduleOne.PlayerScripts;
using ScheduleOne.UI;
using UnityEngine;

namespace QualityOfLife._Player.Camera;

public static class CameraController
{
    private static GameObject crosshair;
    private static Transform cameraTransform;
    private static PlayerCamera playerCam;
    private static ConsoleUI Console;

    private static float zRotation, xRotation;
    private static float zVelocity, xVelocity;
    private static float targetZRotation, targetXRotation;
    private static bool inConsole;

    private static UnityEngine.Camera mainCamera;
    public static bool JustGainedFocus;

    public static void Update()
    {
        if (JustGainedFocus)
        {
            JustGainedFocus = false;
            return;
        }

        if (!crosshair && !mainCamera)
        {
            crosshair = GameObject.Find("UI/HUD/Crosshair");
            mainCamera = UnityEngine.Camera.main;
            cameraTransform = mainCamera?.transform;
        }

        // Ensure camera and crosshair are properly initialized
        if (crosshair == null || !crosshair.activeSelf || cameraTransform == null || playerCam?.ViewingAvatar == true)
            return;

        float mouseDeltaX = Input.GetAxis("Mouse X") * QualityOfLife.Preferences.Camera.MouseTiltSpeed * Singleton<Settings>.instance.LookSensitivity;
        float zWalkTilt = 0f;
        float xWalkTilt = 0f;

        if (!inConsole)
        {
            if (Input.GetKey(KeyCode.A)) zWalkTilt = QualityOfLife.Preferences.Camera.WalkTilt;
            if (Input.GetKey(KeyCode.D)) zWalkTilt = -QualityOfLife.Preferences.Camera.WalkTilt;
            if (Input.GetKey(KeyCode.W)) xWalkTilt = QualityOfLife.Preferences.Camera.WalkTilt;
            if (Input.GetKey(KeyCode.S)) xWalkTilt = -QualityOfLife.Preferences.Camera.WalkTilt;
        }

        targetZRotation = mouseDeltaX + zWalkTilt;
        targetXRotation = xWalkTilt;

        zRotation = Mathf.SmoothDamp(zRotation, targetZRotation, ref zVelocity, QualityOfLife.Preferences.Camera.SmoothTime);
        xRotation = Mathf.SmoothDamp(xRotation, targetXRotation, ref xVelocity, QualityOfLife.Preferences.Camera.SmoothTime);

        cameraTransform.localRotation = Quaternion.Euler(
            cameraTransform.localEulerAngles.x + xRotation,
            cameraTransform.localEulerAngles.y,
            zRotation
        );

        inConsole = Console != null && Console.canvas != null && Console.canvas.enabled;
    }

    public static void InjectPlayerCamera(PlayerCamera cam) => playerCam = cam;
    public static void InjectConsole(ConsoleUI console) => Console = console;
}
