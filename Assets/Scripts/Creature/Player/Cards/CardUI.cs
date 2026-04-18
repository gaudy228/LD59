using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _manaCost;
    [SerializeField] private TextMeshProUGUI _frequencyText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private DescriptionSO _description;
    [SerializeField] private Card _card;
    [SerializeField] private Image _arrowImage;
    [SerializeField] private Sprite _arrow;

    private void Start()
    {
        _manaCost.text = _card.ManaCost.ToString();

        _frequencyText.text = Mathf.Abs(_card.ChangeFrequency).ToString();

        _descriptionText.text = _description.Description;

        _arrowImage.sprite = _arrow;

        if (_card.ChangeFrequency < 0)
        {
            _arrowImage.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        _card.Buy();
    }
}
