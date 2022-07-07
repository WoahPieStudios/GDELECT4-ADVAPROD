using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class TutorialInfo : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField, TextArea(5, 10)] private string body;
    [SerializeField] private Sprite illustration;

    public string Title => title;
    public string Body => body;
    public Sprite Illustration => illustration;
}
