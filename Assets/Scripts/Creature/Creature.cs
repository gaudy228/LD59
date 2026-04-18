using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int CurHealth { get; private set; }
    [field: SerializeField] public int MaxArmor { get; private set; }
    [field: SerializeField] public int StartArmor { get; private set; }
    [field: SerializeField] public int CurArmor { get; private set; }
    [field: SerializeField] public int MaxFrequency { get; private set; }
    [field: SerializeField] public int StartFrequency { get; private set; }
    [field: SerializeField] public int CurFrequency { get; private set; }

    public GameObject Visual;

    public System.Action<int> OnHealthChanged;
    public System.Action<int> OnArmorChanged;
    public System.Action<int> OnFrequencyChanged;

    public virtual void Initialization()
    {
        FullRestore();
    }

    public virtual void FullRestore()
    {
        ChangeHealth(MaxHealth);
        ChangeArmor(StartArmor);
        ChangeFrequency(StartFrequency);
    }

    public virtual void ChangeHealth(int amount)
    {
        int newHealth = CurHealth + amount;
        CurHealth = Mathf.Clamp(newHealth, 0, MaxHealth);

        if (CurHealth <= 0)
        {
            Die();
        }

        OnHealthChanged?.Invoke(CurHealth);
    }

    public virtual void ChangeArmor(int amount)
    {
        int newArmor = CurArmor + amount;

        if (newArmor < 0)
        {
            int damageToHealth = -newArmor;
            CurArmor = 0;
            ChangeHealth(-damageToHealth);
        }
        else
        {
            CurArmor = Mathf.Min(newArmor, MaxArmor);
        }

        OnArmorChanged?.Invoke(CurArmor);
    }

    public virtual void ChangeFrequency(int amount)
    {
        CurFrequency = Mathf.Clamp(CurFrequency + amount, 0, MaxFrequency);
        OnFrequencyChanged?.Invoke(CurFrequency);
    }

    public FrequencyType GetFrequency()
    {
        if((float)CurFrequency / (float)MaxFrequency <= 1f / 3f)
        {
            return FrequencyType.Low;
        }
        else if ((float)CurFrequency / (float)MaxFrequency <= 2f / 3f)
        {
            return FrequencyType.Middle;
        }
        else
        {
            return FrequencyType.High;
        }
    }

    protected abstract void Die();
}
