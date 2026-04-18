using UnityEngine;
using VContainer;
using System.Collections.Generic;

public abstract class Card : MonoBehaviour
{
    protected const int _indexFirstEnemy = 0;
    [field: SerializeField] public int ManaCost {  get; private set; }
    [field: SerializeField] public int ChangeFrequency { get; private set; }

    protected Player _player;
    protected ICommand _cardUse;
    protected EnemyManager _creatureManager;
    protected BattleManager _battleManager;
    protected List<Enemy> _enemies = new List<Enemy>();
    

    [Inject]
    private void Construct(Player player, EnemyManager creatureManager, BattleManager battleManager)
    {
        _player = player;
        _creatureManager = creatureManager;
        _battleManager = battleManager;
    }

    public abstract void Use();

    public virtual bool CanBuy()
    {
        return ManaCost <= _player.CurMana && _player.UseCard && !_battleManager.InFight;
    }

    public void Buy()
    {
        if (CanBuy())
        {
            Use();
            _player.ChangeMana(-ManaCost);
            _player.ChangeFrequency(ChangeFrequency);
            Destroy(gameObject);
        }
    }
}