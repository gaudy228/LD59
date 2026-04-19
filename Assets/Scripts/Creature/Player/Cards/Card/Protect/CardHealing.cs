using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CardHealing : Card
{
    [SerializeField] private int _health;
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _duration;

    private CancellationTokenSource _battleCancellationTokenSource;

    public override async void Use()
    {
        List<Creature> creature = new List<Creature>();
        creature.Add(_player);

        _cardUse = new HealthCommand(_health, creature, _player.Visual, _moveDistance, _duration);

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
