using System;

public class GrappleCrosshair : CrosshairChange
{
    private static event Action<int> onCrosshairUpdateGrapple;
    /// <summary>
    /// 0 = defualt
    /// 1 = can grapple
    /// 2 = cannot grapple
    /// </summary>
    private void OnEnable()
    {
        onCrosshairUpdateGrapple += UpdateCrosshair;
    }

    private void OnDisable()
    {
        onCrosshairUpdateGrapple -= UpdateCrosshair;
    }

    void Start()
    {
        // Always make the first iteration the default
        crosshairImage.sprite = crosshairSprites[0];
    }

    protected override void UpdateCrosshair(int index)
    {
        switch (index)
        {
            case 0:
                crosshairImage.sprite = crosshairSprites[0];
                break;
            case 1:
                crosshairImage.sprite = crosshairSprites[1];
                break;

            case 2:
                crosshairImage.sprite = crosshairSprites[2];
                break;
            default:
                throw new System.Exception();
        }
    }

    public static void OnUpdateGrappleCH(int index) => onCrosshairUpdateGrapple?.Invoke(index);

}
