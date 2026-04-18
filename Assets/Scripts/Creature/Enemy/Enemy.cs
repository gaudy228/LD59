using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Enemy : Creature
{
    public Action<Enemy> OnDieEnemy;
    private Player _player;
    [SerializeField] private int _damage;

    protected List<ICommand> _commands = new List<ICommand>();

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    public override void Initialization()
    {
        base.Initialization();
        NewCommand();
    }

    public virtual void NewCommand()
    {
        List<Creature> enemies = new List<Creature>();
        enemies.Add(_player);

        ICommand attak = new ArmorCommand(_damage, enemies, Visual, -100f, 0.2f);
        SetCommand(attak);
    }

    public void SetCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public List<ICommand> GetCommands()
    {
        List<ICommand> commands = new List<ICommand>(_commands);
        _commands.Clear();
        return commands;
    }

    protected override void Die()
    {
        OnDieEnemy?.Invoke(this);
        Destroy(gameObject);
    }
}
