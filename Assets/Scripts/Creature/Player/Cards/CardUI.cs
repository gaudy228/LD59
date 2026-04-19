using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _manaCost;
    [SerializeField] private GameObject _discriptionPanel;
    [SerializeField] private Card _card;
    [SerializeField] private Image _arrowImage;

    [SerializeField] private List<Image> _frequencyUIs = new List<Image>();

    [SerializeField] private Sprite _lowFrequency;
    [SerializeField] private Sprite _middleFrequency;
    [SerializeField] private Sprite _highFrequency;

    private void Start()
    {
        _manaCost.text = _card.ManaCost.ToString();

        if (_card.ChangeFrequency < 0)
        {
            _arrowImage.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (NeedFrequency(FrequencyType.Low))
        {
            _frequencyUIs[0].sprite = _lowFrequency;
        }
        if(NeedFrequency(FrequencyType.Middle))
        {
            _frequencyUIs[1].sprite = _middleFrequency;
        }
        if(NeedFrequency(FrequencyType.High))
        {
            _frequencyUIs[2].sprite = _highFrequency;
        }
    }

    private bool NeedFrequency(FrequencyType frequencyType)
    {
        foreach (var item in _card.NeedTypes)
        {
            if(item == frequencyType)
            {
                return true;
            }
        }
        return false;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        _card.Buy();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _discriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _discriptionPanel.SetActive(false);
    }
}
