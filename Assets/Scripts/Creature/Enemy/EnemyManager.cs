using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyWaveSO> _enemyWaveSOs = new List<EnemyWaveSO>();
    [SerializeField] private List<Button> _buttonsNewWave = new List<Button>();
    [SerializeField] private GameObject _wavePanel;
    [SerializeField] private GameObject _winPanel;
    private int _curWave;
    private bool _lastWave = false;

    private BattleManager _battleManager;

    public List<Enemy> Enemys { get; private set; } = new List<Enemy>();
    
    private IObjectResolver _objectResolver;

    public event Action OnNewWave;
    public event Action OnWinGame;

    [Inject]
    private void Construct(BattleManager battleManager, IObjectResolver objectResolver)
    {
        _battleManager = battleManager;
        _objectResolver = objectResolver;
    }

    private void OnEnable()
    {
        _battleManager.OnEndFight += NewEnemyAttacks;
        _battleManager.OnEndFight += UpdateEnemy;
    }

    private void OnDisable()
    {
        _battleManager.OnEndFight -= NewEnemyAttacks;
        _battleManager.OnEndFight -= UpdateEnemy;
    }

    public void Initialization()
    {
        NewWave();
    }

    public void NewWave()
    {
        for (int i = 0; i < _enemyWaveSOs[_curWave].Enemies.Count; i++)
        {
            SpawnEnemy(_enemyWaveSOs[_curWave].Enemies[i]);
        }

        foreach (var creature in Enemys)
        {
            creature.Initialization();
        }

        UpdateEnemy();

        if(_curWave > 0)
        {
            _buttonsNewWave[_curWave].interactable = false;
        }

        _curWave++;

        if(_curWave < _enemyWaveSOs.Count && _curWave > 1)
        {
            _buttonsNewWave[_curWave].interactable = true;
        }
        else
        {
            //_lastWave = true;
        }
    }

    private void UpdateEnemy()
    {
        _battleManager.SetEnemies(Enemys);
    }

    private void NewEnemyAttacks()
    {
        foreach (var enemy in Enemys)
        {
            enemy.NewCommand();
        }
    }

    private void SpawnEnemy(Enemy enemyPrefab)
    {
        Enemy enemy = _objectResolver.Instantiate(enemyPrefab, transform);
        Enemys.Add(enemy);
        enemy.OnDieEnemy += DieEnemy;
    }

    private void DieEnemy(Enemy enemy)
    {
        enemy.OnDieEnemy -= DieEnemy;
        Enemys.Remove(enemy);
        UpdateEnemy();
        EndGame();
    }

    private void EndGame()
    {
        if (_lastWave)
        {
            //OnWinGame?.Invoke();
        }
        if (Enemys.Count <= 0)
        {
            OnWinGame?.Invoke();
            //OnNewWave?.Invoke();
            //_wavePanel.SetActive(true);
        }
    }
}
