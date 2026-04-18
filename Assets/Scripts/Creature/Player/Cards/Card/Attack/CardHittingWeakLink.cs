using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class CardHittingWeakLink : Card
{
    [SerializeField] private int _damage;
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _duration;

    private CancellationTokenSource _battleCancellationTokenSource;

    public override async void Use()
    {
        _enemies = _creatureManager.Enemys;

        List<Creature> enemies = new List<Creature>();

        Enemy enemy = GetEnemyWithLowestHp(_enemies);

        enemies.Add(enemy);

        if(enemy.GetFrequency() == FrequencyType.High)
        {
            _cardUse = new HealthCommand(_damage, enemies, _player.Visual, _moveDistance, _duration);
        }
        else
        {
            _cardUse = new ArmorCommand(_damage, enemies, _player.Visual, _moveDistance, _duration);
        }


        if (_battleCancellationTokenSource != null)
        {
            _battleCancellationTokenSource.Cancel();
            _battleCancellationTokenSource.Dispose();
        }

        _battleCancellationTokenSource = new CancellationTokenSource();

        _player.UseCard = false;

        await _cardUse.AnimationCommand(_battleCancellationTokenSource.Token);

        _player.UseCard = true;
    }

    public Enemy GetEnemyWithLowestHp(List<Enemy> enemies)
    {
        if (enemies == null || enemies.Count == 0)
            return null;

        return enemies.OrderBy(e => e.CurHealth).First();
    }
}
