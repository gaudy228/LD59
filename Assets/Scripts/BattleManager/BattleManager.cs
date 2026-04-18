using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Button _startFight;

    private List<Enemy> _enemies = new List<Enemy>();

    private List<ICommand> _commands = new List<ICommand>();

    private CancellationTokenSource _battleCancellationTokenSource;

    public event Action OnEndFight;

    public bool InFight { get; private set; } = false;

    public void Initialization()
    {

    }

    public async void StartFight()
    {
        InFight = true;
        _startFight.interactable = false;

        if (_battleCancellationTokenSource != null)
        {
            _battleCancellationTokenSource.Cancel();
            _battleCancellationTokenSource.Dispose();
        }

        _battleCancellationTokenSource = new CancellationTokenSource();
        try
        {
            foreach (var enemy in _enemies)
            {
                var enemyCommands = enemy.GetCommands();
                foreach (var command in enemyCommands)
                {
                    _commands.Add(command);
                }
            }

            foreach (var command in _commands)
            {
                _battleCancellationTokenSource.Token.ThrowIfCancellationRequested();

                await ExecuteCommand(command);
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("О нет че то с файтом не так");
        }
        finally
        {
            _commands.Clear();
            _battleCancellationTokenSource.Dispose();
            _battleCancellationTokenSource = null;

            OnEndFight?.Invoke();
            _startFight.interactable = true;
            InFight = false;
        }
    }

    private async UniTask ExecuteCommand(ICommand command)
    {
        await command.AnimationCommand(_battleCancellationTokenSource.Token);

        await UniTask.Delay(200, cancellationToken: _battleCancellationTokenSource.Token);
    }

    public void SetCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void SetEnemies(List<Enemy> enemies)
    {
        _enemies = new List<Enemy>(enemies);
    }
}
