using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Enemy : Creature
{
    private int _maxAttack;

    private int _rndAttack;

    public Action<Enemy> OnDieEnemy;
    private Player _player;
    [SerializeField] private int _damage;
    [SerializeField] private int _armor;
    [SerializeField] private int _health;
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _duration;

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
        if(CurHealth < MaxHealth)
        {
            _maxAttack = 3;
            _rndAttack = UnityEngine.Random.Range(0, _maxAttack);
        }
        else if(CurArmor < MaxArmor)
        {
            _maxAttack = 2;
            _rndAttack = UnityEngine.Random.Range(0, _maxAttack);
        }
        else
        {
            _maxAttack = 1;
            _rndAttack = UnityEngine.Random.Range(0, _maxAttack);
        }

        if(_rndAttack == 0)
        {
            List<Creature> enemies = new List<Creature>();
            enemies.Add(_player);
            ICommand attak = new ArmorCommand(_damage, enemies, Visual, _moveDistance, _duration);
            SetCommand(attak);
        }
        if(_rndAttack == 1)
        {
            List<Creature> enemies = new List<Creature>();
            enemies.Add(this);
            ICommand attak = new ArmorCommand(_armor, enemies, Visual, _moveDistance, _duration);
            SetCommand(attak);
        }
        if(_rndAttack == 2)
        {
            List<Creature> enemies = new List<Creature>();
            enemies.Add(this);
            ICommand attak = new HealthCommand(_health, enemies, Visual, _moveDistance, _duration);
            SetCommand(attak);
        }
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
