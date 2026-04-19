using UnityEngine;
using UnityEngine.UI;

public class FrequencyUI : MonoBehaviour
{
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private Sprite _fullSprite;
    [SerializeField] private Image _frequencyImange;

    public void Full()
    {
        _frequencyImange.sprite = _fullSprite;
    }

    public void Empty()
    {
        _frequencyImange.sprite = _emptySprite;
    }
}
