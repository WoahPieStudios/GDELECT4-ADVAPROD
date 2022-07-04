using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIBehavior : MonoBehaviour
{
    public Transform HealthBar;
    public Transform Ammo;
    public Transform Objectives;
    public Transform Ability;

    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    void Move()
    {
        HealthBar.DOMoveY(15, 1).SetEase(Ease.InOutSine);
        Ammo.DOMoveY(150, 1).SetEase(Ease.InOutSine);
        Objectives.DOMoveY(1050, 1).SetEase(Ease.InOutSine);
        Ability.DOMoveY(150, 1).SetEase(Ease.InOutSine);

    }
}
