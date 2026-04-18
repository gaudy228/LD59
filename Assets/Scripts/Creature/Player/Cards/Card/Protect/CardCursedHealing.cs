using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CardCursedHealing : Card
{
    private const int _remainderDivisor = 2;

    [SerializeField] private float _moveDistance;
    [SerializeField] private float _duration;

    private CancellationTokenSource _battleCancellationTokenSource;

    public override async void Use()
    {
        List<Creature> creature = new List<Creature>();
        creature.Add(_player);

        int health = (_player.MaxHealth - _player.CurHealth) / _remainderDivisor;

        _cardUse = new HealthCommand(health, creature, _player.Visual, _moveDistance, _duration);

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

    public override bool CanBuy()
    {
        return base.CanBuy() && _player.GetFrequency() != FrequencyType.Middle;
    }
}
