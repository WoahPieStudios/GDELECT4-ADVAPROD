using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class About_UIBehavior : MonoBehaviour
{
    public Transform gameTitle;
    public Transform backBtn;
   
    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    
    void Move()
    {
        gameTitle.DOMoveY(900, 1).SetEase(Ease.InOutSine);
        backBtn.DOMoveX(200, 1).SetEase(Ease.InOutSine);
    }
}
