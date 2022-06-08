using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun _toEquip;

    private Gun _toUseGun;
    // Start is called before the first frame update
    void Start()
    {
        EquipGun(_toEquip);
    }

    private void EquipGun(Gun gun)
    {
        if (_toUseGun != null)
        {
            Destroy(_toUseGun.gameObject);
        }

        _toUseGun = Instantiate(_toEquip) as Gun;
        _toUseGun.gameObject.transform.SetParent(transform);
        _toUseGun.transform.localPosition = Vector3.zero;

    }

}