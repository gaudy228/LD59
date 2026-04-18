using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<Card> _allCards = new List<Card>();

    [SerializeField] private int _countCards;

    private List<Card> _curCards = new List<Card>();

    private IObjectResolver _objectResolver;

    private BattleManager _battleManager;
    private EnemyManager _enemyManager;

    [Inject]
    private void Construct(BattleManager battleManager, EnemyManager enemyManager, IObjectResolver objectResolver)
    {
        _battleManager = battleManager;
        _enemyManager = enemyManager;
        _objectResolver = objectResolver;
    }

    private void OnEnable()
    {
        _battleManager.OnEndFight += SpawnCards;
        _enemyManager.OnNewWave += SpawnCards;
    }

    private void OnDisable()
    {
        _battleManager.OnEndFight -= SpawnCards;
        _enemyManager.OnNewWave -= SpawnCards;
    }

    public void Initialization()
    {
        SpawnCards();
    }

    private void SpawnCards()
    {
        foreach (var card in _curCards)
        {
            if (card != null)
            {
                Destroy(card.gameObject);
            }
        }
        _curCards.Clear();

        for (int i = 0; i < _countCards; i++)
        {
            Card card = _objectResolver.Instantiate(_allCards[Random.Range(0, _allCards.Count)], transform);
            _curCards.Add(card);
        }
    }
}
