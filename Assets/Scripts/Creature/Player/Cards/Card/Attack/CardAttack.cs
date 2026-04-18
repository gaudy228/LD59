using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CardAttack : Card
{
    [SerializeField] private int _damage;
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _duration;

    private CancellationTokenSource _battleCancellationTokenSource;

    public override async void Use()
    {
        _enemies = _creatureManager.Enemys;

        List<Creature> enemies = new List<Creature>();

        enemies.Add(_enemies[_indexFirstEnemy]);

        _cardUse = new ArmorCommand(_damage, enemies, _player.Visual, _moveDistance, _duration);


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
}
