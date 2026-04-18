using UnityEngine;
using VContainer;

public class PlayerWinLose : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    private EnemyManager _enemyManager;
    private Player _player;

    [Inject]
    private void Construct(EnemyManager enemyManager, Player player)
    {
        _enemyManager = enemyManager;
        _player = player;
    }

    private void OnEnable()
    {
        _enemyManager.OnWinGame += Win;
        _player.OnDiePlayer += Lose;
    }

    private void OnDisable()
    {
        _enemyManager.OnWinGame -= Win;
        _player.OnDiePlayer += Lose;
    }

    private void Win()
    {
        _winPanel.SetActive(true);
    }

    private void Lose()
    {
        _losePanel.SetActive(true);
    }
}
