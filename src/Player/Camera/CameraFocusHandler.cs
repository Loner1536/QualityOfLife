namespace QualityOfLife._Player.Camera;

public static class CameraFocusHandler
{
    public static void OnFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            CameraController.JustGainedFocus = true;
        }
    }
}
