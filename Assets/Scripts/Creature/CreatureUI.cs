using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureUI : MonoBehaviour
{
    [SerializeField] protected Creature _creature;

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _armorBar;
    [SerializeField] private Slider _frequencySlider;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _armorText;

    [SerializeField] private List<Image> _frequencyUIs = new List<Image>();

    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private Sprite _lowSprite;
    [SerializeField] private Sprite _middleSprite;
    [SerializeField] private Sprite _highSprite;


    private void Awake()
    {
        Subscription();
    }

    private void OnDisable()
    {
        UnSubscription();
    }

    public virtual void Subscription()
    {
        _creature.OnHealthChanged += ChangeHealthUI;
        _creature.OnArmorChanged += ChangeArmorUI;
        _creature.OnFrequencyChanged += ChangeFrequencyUI;
    }

    public virtual void UnSubscription()
    {
        _creature.OnHealthChanged -= ChangeHealthUI;
        _creature.OnArmorChanged -= ChangeArmorUI;
        _creature.OnFrequencyChanged -= ChangeFrequencyUI;
    }

    public void ChangeHealthUI(int health)
    {
        _healthBar.fillAmount = (float)health / (float)_creature.MaxHealth;
        _healthText.text = $"{health}/{_creature.MaxHealth}";
    }

    public void ChangeArmorUI(int armor)
    {
        _armorBar.fillAmount = (float)armor / (float)_creature.MaxArmor;
        _armorText.text = $"{armor}/{_creature.MaxArmor}";
    }

    public void ChangeFrequencyUI(int frequency)
    {
        _frequencySlider.value = (float)frequency / (float)_creature.MaxFrequency;

        if (_creature.GetFrequency() == FrequencyType.Low)
        {
            _frequencyUIs[0].sprite = _lowSprite;
            _frequencyUIs[1].sprite = _emptySprite;
            _frequencyUIs[2].sprite = _emptySprite;
        }
        if(_creature.GetFrequency() == FrequencyType.Middle)
        {
            _frequencyUIs[1].sprite = _middleSprite;
            _frequencyUIs[0].sprite = _emptySprite;
            _frequencyUIs[2].sprite = _emptySprite;
        }
        if(_creature.GetFrequency() == FrequencyType.High)
        {
            _frequencyUIs[2].sprite = _highSprite;
            _frequencyUIs[1].sprite = _emptySprite;
            _frequencyUIs[0].sprite = _emptySprite;
        }
    }
}

