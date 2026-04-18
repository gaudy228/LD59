using UnityEngine;
using UnityEngine.UI;

public class CreatureUI : MonoBehaviour
{
    [SerializeField] protected Creature _creature;

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _armorBar;
    [SerializeField] private Slider _frequencySlider;

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
    }

    public void ChangeArmorUI(int armor)
    {
        _armorBar.fillAmount = (float)armor / (float)_creature.MaxArmor;
    }

    public void ChangeFrequencyUI(int frequency)
    {
        _frequencySlider.value = (float)frequency / (float)_creature.MaxFrequency;
    }
}

