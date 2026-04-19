using TMPro;
using UnityEngine;

public class TextLocalization : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private DescriptionSO _descriptionSO;

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        Localization.OnSwitchLocalization += UpdateText;
    }

    private void OnDisable()
    {
        Localization.OnSwitchLocalization -= UpdateText;
    }

    private void UpdateText()
    {
        _text.text = _descriptionSO.Description();
    }
}
