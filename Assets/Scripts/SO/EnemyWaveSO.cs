using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "EnemyWave")]
public class EnemyWaveSO : ScriptableObject
{
    [field: SerializeField] public List<Enemy> Enemies = new List<Enemy>();
}
