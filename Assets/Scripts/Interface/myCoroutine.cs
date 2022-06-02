using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class myCoroutine : MonoBehaviour
{
    public GameObject controlUI;
    TextMeshProUGUI cooldownCounter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cooldown());
       // controlUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Remove());
        Debug.Log("StartCoroutine(Remove)");
    }

    IEnumerator Cooldown()
    {
       // Time.timeScale = 0f;
        yield return new WaitForSeconds(1f);
        controlUI.SetActive(true);
    }

    IEnumerator Remove()
    {
       // Time.timeScale = 1f;
        yield return new WaitForSeconds(5f);
        controlUI.SetActive(false);
    }
}
