using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class myCoroutine : MonoBehaviour
{
    public GameObject controlUI;
    TextMeshProUGUI cooldownCounter;
    public static bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cooldown());
       // controlUI.SetActive(false);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    StartCoroutine(Remove());
    //    Debug.Log("StartCoroutine(Remove)");
    //}

    IEnumerator Cooldown()
    {
       // Time.timeScale = 0f;
       Time.timeScale = 0f;
        isRunning = true;
        yield return new WaitForSecondsRealtime(1f);
        controlUI.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        controlUI.SetActive(false);
        isRunning = false;
        Time.timeScale = 1f;
    }
}
