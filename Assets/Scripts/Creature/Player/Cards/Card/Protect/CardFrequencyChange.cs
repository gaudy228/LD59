using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CardFrequencyChange : Card
{
    private const int _middleFrequency = 50;

    [SerializeField] private FrequencyType _frequencyType;

    [SerializeField] private float _moveDistance;
    [SerializeField] private float _duration;

    private CancellationTokenSource _battleCancellationTokenSource;

    public override async void Use()
    {
        _enemies = _creatureManager.Enemys;
        List<ICommand> commands = new List<ICommand>();

        foreach (var enemy in _enemies)
        {
            List<Creature> enemiesNew = new List<Creature>();
            enemiesNew.Add(enemy);
            int frequency = 0;

            if (_frequencyType == FrequencyType.Low)
            {
                frequency =  -enemy.CurFrequency;
            }
            if(_frequencyType == FrequencyType.Middle)
            {
                frequency = _middleFrequency - enemy.CurFrequency;
            }
            if(_frequencyType == FrequencyType.High)
            {
                frequency = enemy.MaxFrequency;
            }

            _cardUse = new FrequencyCommand(frequency, enemiesNew, _player.Visual, _moveDistance, _duration);
            commands.Add(_cardUse);
        }

        if (_battleCancellationTokenSource != null)
        {
            _battleCancellationTokenSource.Cancel();
            _battleCancellationTokenSource.Dispose();
        }

        _battleCancellationTokenSource = new CancellationTokenSource();

        _player.UseCard = false;

        await _cardUse.AnimationCommand(_battleCancellationTokenSource.Token);

        for (int i = 0; i < commands.Count - 1; i++)
        {
            commands[i].Execute();
        }

        _player.UseCard = true;
    }
}
