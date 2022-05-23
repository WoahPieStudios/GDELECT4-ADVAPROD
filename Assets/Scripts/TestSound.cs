using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestSound : MonoBehaviour
{
    public AudioClip buttonSound;
    public AudioClip BG_music;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space is pressed");
            SoundManager.instance.PlaySFX(buttonSound);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
        SoundManager.instance.PlaySFX(buttonSound);
    }
}
