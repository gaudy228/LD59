using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    [SerializeField] private BattleManager _battleManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private Player _player;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<BootstrapGame>();
        builder.RegisterInstance(_battleManager);
        builder.RegisterInstance(_player);
        builder.RegisterInstance(_enemyManager);
        builder.RegisterInstance(_cardManager);
    }
}
