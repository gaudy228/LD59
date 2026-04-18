using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CardStripYourArmor : Card
{
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _duration;

    private CancellationTokenSource _battleCancellationTokenSource;

    public override async void Use()
    {
        if (_creatureManager.Enemys.Count > 0)
        {
            _enemies = _creatureManager.Enemys;

            List<Creature> enemies = new List<Creature>();

            enemies.Add(_enemies[_indexFirstEnemy]);

            int armor = _enemies[_indexFirstEnemy].GetFrequency() == FrequencyType.Low ? -_enemies[_indexFirstEnemy].CurArmor : 0;


            _cardUse = new ArmorCommand(armor, enemies, _player.Visual, _moveDistance, _duration);



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
