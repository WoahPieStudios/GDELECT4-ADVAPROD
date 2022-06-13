using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairChange : MonoBehaviour
{
    [SerializeField]
    protected Image crosshairImage;
    [SerializeField]
    protected List<Sprite> crosshairSprites;


    protected virtual void UpdateCrosshair(int index)
    {

    }

}
