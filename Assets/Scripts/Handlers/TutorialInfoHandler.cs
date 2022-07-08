using System;
using System.Collections;
using System.Collections.Generic;
using AdditiveScenes.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialInfoHandler : MonoBehaviour
{
    [SerializeField] private TutorialInfo[] entries;

    [SerializeField] private TextMeshProUGUI title, body;
    [SerializeField] private Animator exampleDisplay;

    [SerializeField] private bool showPreviousBtnOnFirstPage;
    [SerializeField] private Button previousBtn, nextBtn;
    [SerializeField] private SFXChannel buttonSFX;
    [SerializeField] private UnityEvent onFirstPage;
    
    public int _currentPage;

    private void OnEnable()
    {
        _currentPage = 0;
        SetPage();
    }

    private void SetPage()
    {
        if (_currentPage == 0 && showPreviousBtnOnFirstPage)
        {
            previousBtn.gameObject.SetActive(true);
        }
        else if(_currentPage != 0){
            previousBtn.gameObject.SetActive(true);
        }
        else
        {
            previousBtn.gameObject.SetActive(false);
        }
        nextBtn.gameObject.SetActive(_currentPage != entries.Length - 1);
        
        var entry = entries[_currentPage];

        title.text = entry.Title;
        body.text = entry.Body;
        exampleDisplay.Play(entry.ExampleClip.name);
    }
    
    public void GoNext()
    {
        _currentPage++;
        buttonSFX.PlayAudio();
        SetPage();
    }

    public void GoBack()
    {
        buttonSFX.PlayAudio();
        if (_currentPage == 0)
        {
            onFirstPage?.Invoke();
            return;
        }
        _currentPage--;
        SetPage();
    }
    
}
