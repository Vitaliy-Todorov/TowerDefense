using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesWaveData", menuName = "Data/EnemiesWaveData")]
public class EnemiesWaveData : ScriptableObject
{
    public EEnemyType EnemyType;
    public float TimeBetweenWaves;
    public float TimeWave;
    public Vector3 SpawnPoint;
    public float TimeBetweenSpawnOfEnemies;
}