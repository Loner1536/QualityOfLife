using MelonLoader;

namespace QualityOfLife.Preferences
{
    public static class Camera
    {
        private static MelonPreferences_Category category;
        private static MelonPreferences_Entry<float> smoothSensitivity;
        private static MelonPreferences_Entry<float> mouseTiltSpeed;
        private static MelonPreferences_Entry<float> smoothTime;
        private static MelonPreferences_Entry<float> walkTilt;

        public static float SmoothSensitivity => smoothSensitivity.Value;
        public static float MouseTiltSpeed => mouseTiltSpeed.Value;
        public static float SmoothTime => smoothTime.Value;
        public static float WalkTilt => walkTilt.Value;

        static Camera()
        {
            category = MelonPreferences.CreateCategory("QOL_Camera", "Camera Settings");
            smoothSensitivity = category.CreateEntry("Smooth Sensitivity", 20f, "Camera Smooth Sensitivity");
            mouseTiltSpeed = category.CreateEntry("Mouse Tilt Speed", 0.01f, "Camera Mouse Tilt Speed");
            smoothTime = category.CreateEntry("Smooth Time", 0.1f, "Camera Smooth Time");
            walkTilt = category.CreateEntry("Walk Tilt", 3f, "Camera Walk Tilt");
        }
    }
}
