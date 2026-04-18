using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CardBlastWave : Card
{
    [SerializeField] private int _damage;
    [SerializeField] private int _bigDamage;
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _duration;

    private CancellationTokenSource _battleCancellationTokenSource;

    public override async void Use()
    {
        if (_creatureManager.Enemys.Count > 0)
        {
            _enemies = _creatureManager.Enemys;

            List<Creature> enemies = new List<Creature>(_enemies);

            int damage = _player.GetFrequency() == FrequencyType.Middle ? _bigDamage : _damage;



            _cardUse = new ArmorCommand(damage, enemies, _player.Visual, _moveDistance, _duration);



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
}
