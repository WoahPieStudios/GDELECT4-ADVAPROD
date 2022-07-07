using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialInfoHandler : MonoBehaviour
{
    [SerializeField] private TutorialInfo[] entries;

    [SerializeField] private TextMeshProUGUI title, body;
    [SerializeField] private Image illustration;

    [SerializeField] private Button previousBtn, nextBtn;
    
    public int _currentPage;

    private void OnEnable()
    {
        _currentPage = 0;
        SetPage();
    }

    private void SetPage()
    {
        previousBtn.gameObject.SetActive(_currentPage != 0);
        nextBtn.gameObject.SetActive(_currentPage != entries.Length - 1);
        
        var entry = entries[_currentPage];

        title.text = entry.Title;
        body.text = entry.Body;
        illustration.sprite = entry.Illustration;

    }
    
    public void GoNext()
    {
        _currentPage++;
        SetPage();
    }

    public void GoBack()
    {
        _currentPage--;
        SetPage();
    }
    
}
