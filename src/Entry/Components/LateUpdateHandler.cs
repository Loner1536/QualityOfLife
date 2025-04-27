namespace QualityOfLife;

public class LateUpdateHandler
{
    public static void HandleLateUpdate()
    {
        QualityOfLife._Player.Camera.Core.OnLateUpdate();
    }
}
