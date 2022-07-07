using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    //temporary. Will change when tutorial branch merges
    public bool _onTutorial;


    [SerializeField]
    private WeaponItem _default;

    private Gun _toUseGun;
    

    void Start()
    {
        if (!_onTutorial)
        {
            EquipGun(_default.gun);
            ChangeWeaponIcon.OnChangeIcon(_default.weaponIcon);
        }



    }

    private void OnEnable()
    {
        WeaponItem.onGetWeapon += EquipGun;
    }

    private void OnDisable()
    {
        WeaponItem.onGetWeapon -= EquipGun;
    }

    private void EquipGun(Gun gun)
    {

        if (_toUseGun != null)
        {
            //change this to drop
            Destroy(_toUseGun.gameObject);
        }


        _toUseGun = Instantiate(gun) as Gun;
        _toUseGun.gameObject.transform.SetParent(transform);
        _toUseGun.transform.localPosition = Vector3.zero;
        _toUseGun.transform.localEulerAngles = Vector3.zero;

    }





}
