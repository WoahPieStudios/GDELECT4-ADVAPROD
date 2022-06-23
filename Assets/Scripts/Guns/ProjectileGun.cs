using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : Gun
{

    [SerializeField]
    private bool _hasBulletDrop;

    [SerializeField]
    private GameObject _bullet;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Shoot()
    {
        //test
        Debug.Log("Yes");
    }
}
