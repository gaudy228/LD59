using VContainer.Unity;

public class BootstrapGame : IStartable
{
    private EnemyManager _creatureManager;
    private BattleManager _battleManager;
    private CardManager _cardManager;
    private Player _player;

    public BootstrapGame(EnemyManager creatureManager, BattleManager battleManager, Player player, CardManager cardManager)
    {
        _creatureManager = creatureManager;
        _battleManager = battleManager;
        _player = player;
        _cardManager = cardManager;
    }

    public void Start()
    {
        _player.Initialization();
        _creatureManager.Initialization();
        _cardManager.Initialization();
        _battleManager.Initialization();
    }
}
