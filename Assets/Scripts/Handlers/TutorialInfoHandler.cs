using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialInfoHandler : MonoBehaviour
{
    [SerializeField] private TutorialInfo[] entries;

    [SerializeField] private TextMeshProUGUI title, body;
    [SerializeField] private Animator exampleDisplay;

    [SerializeField] private Button previousBtn, nextBtn;

    [SerializeField] private UnityEvent onFirstPage;
    
    public int _currentPage;

    private void OnEnable()
    {
        _currentPage = 0;
        SetPage();
    }

    private void SetPage()
    {
        nextBtn.gameObject.SetActive(_currentPage != entries.Length - 1);
        
        var entry = entries[_currentPage];

        title.text = entry.Title;
        body.text = entry.Body;
        exampleDisplay.Play(entry.ExampleClip.name);
    }
    
    public void GoNext()
    {
        _currentPage++;
        SetPage();
    }

    public void GoBack()
    {
        if (_currentPage == 0)
        {
            onFirstPage?.Invoke();
            return;
        }
        _currentPage--;
        SetPage();
    }
    
}
