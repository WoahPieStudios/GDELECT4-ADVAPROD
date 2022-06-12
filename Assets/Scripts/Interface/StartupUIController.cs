using System.Collections;
using System.Collections.Generic;
using AdditiveScenes.Scripts.ScriptableObjects;
using UnityEngine;
using TMPro;

public class StartupUIController : MonoBehaviour
{
    [SerializeField] private PauseEventChannel pauseEventChannel;
    public GameObject controlUI;
    TextMeshProUGUI cooldownCounter;
    private bool _hasRan;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //    // StartCoroutine(Cooldown());
    //    // controlUI.SetActive(false);
    // }

    //// Update is called once per frame
    //void Update()
    //{
    //    StartCoroutine(Remove());
    //    Debug.Log("StartCoroutine(Remove)");
    //}

    public void StartCooldown()
    {
        if(!_hasRan)
            StartCoroutine(Cooldown());
    }
    
    IEnumerator Cooldown()
    {
       // Time.timeScale = 0f;
       //Time.timeScale = 0f;
       pauseEventChannel.OnPause();
       yield return new WaitForSecondsRealtime(1f);
       controlUI.SetActive(true);
       yield return new WaitForSecondsRealtime(5f);
       controlUI.SetActive(false);
       pauseEventChannel.OnResume();
       _hasRan = true;
       //Time.timeScale = 1f;
    }
}
