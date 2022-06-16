using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIBehavior : MonoBehaviour
{
    public Transform HealthBar;
    public Transform Ammo;
    public Transform Objectives;

    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    void Move()
    {
        HealthBar.DOMoveY(50, 1).SetEase(Ease.InOutSine);
        Ammo.DOMoveY(105, 1).SetEase(Ease.InOutSine);
        Objectives.DOMoveY(1050, 1).SetEase(Ease.InOutSine);

    }
}
