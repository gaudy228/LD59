using UnityEngine;
using VContainer;

public class Player : Creature
{
    [field: SerializeField] public int MaxMana { get; private set; }
    [field: SerializeField] public int CurMana { get; private set; }

    public bool UseCard = true;

    private BattleManager _battleManager;
    private EnemyManager _enemyManager;

    public System.Action<int> OnManaChanged;
    public System.Action OnDiePlayer;

    [Inject]
    private void Construct(BattleManager battleManager, EnemyManager enemyManager)
    {
        _battleManager = battleManager;
        _enemyManager = enemyManager;
    }

    private void OnEnable()
    {
        _battleManager.OnEndFight += FullMana;
        _enemyManager.OnNewWave += FullMana;
    }

    private void OnDisable()
    {
        _battleManager.OnEndFight -= FullMana;
        _enemyManager.OnNewWave -= FullMana;
    }

    public override void FullRestore()
    {
        base.FullRestore();
        ChangeMana(MaxMana);
    }

    public void FullMana()
    {
        ChangeMana(MaxMana);
    }

    public virtual void ChangeMana(int amount)
    {
        CurMana = Mathf.Clamp(CurMana + amount, 0, MaxMana);
        OnManaChanged?.Invoke(CurMana);
    }

    protected override void Die()
    {
        OnDiePlayer?.Invoke();
    }
}
