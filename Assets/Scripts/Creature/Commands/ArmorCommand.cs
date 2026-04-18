using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ArmorCommand : ICommand
{
    private int _armor;
    private List<Creature> _creatures = new List<Creature>();
    private GameObject _owner;
    private float _moveDistance;
    private float _duration;

    public ArmorCommand(int armor, List<Creature> creatures, GameObject owner, float moveDistance, float duration)
    {
        _armor = armor;
        _creatures = creatures;
        _owner = owner;
        _moveDistance = moveDistance;
        _duration = duration;
    }

    public async UniTask AnimationCommand(CancellationToken cancellationToken)
    {

        Vector3 startPosition = _owner.gameObject.transform.position;

        await _owner.gameObject.transform.DOMoveX(startPosition.x + _moveDistance, _duration)
            .SetEase(Ease.OutQuad)
            .AsyncWaitForCompletion();

        Execute();

        await _owner.gameObject.transform.DOMoveX(startPosition.x, _duration)
            .SetEase(Ease.InQuad)
            .AsyncWaitForCompletion();
    }

    public void Execute()
    {
        if (_creatures != null)
        {
            foreach (Creature creature in _creatures)
            {
                creature.ChangeArmor(_armor);
            }
        }
    }
}
