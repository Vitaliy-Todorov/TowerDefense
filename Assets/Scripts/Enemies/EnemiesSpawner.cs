using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemies;
    [SerializeField]
    private List<EnemiesWaveData> _enemiesWaves;

    private World _world;

    public List<GameObject> Enemies => _enemies;

    public void Construct(World world)
    {
        _world = world;

        foreach (GameObject enemy in _enemies)
        {
            DestroyObject destroyObject = enemy.GetComponent<DestroyObject>();
            if(destroyObject != null)
                destroyObject.Destroy += Remove;
            
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.Construct(_world, _world.GeneralData.EnemiesData[enemyController.Type]);
        }

        StartCoroutine(LaunchWaves());
    }

    private IEnumerator LaunchWaves()
    {
        foreach (EnemiesWaveData enemiesWaveData in _enemiesWaves)
        {
            Coroutine wave = StartCoroutine(WaveStart(enemiesWaveData));

            yield return new WaitForSeconds(enemiesWaveData.TimeWave);
            StopCoroutine(wave);

            yield return new WaitForSeconds(enemiesWaveData.TimeBetweenWaves);
        }
    }

    private IEnumerator WaveStart(EnemiesWaveData enemiesWaveData)
    {
        while (true)
        {
            yield return new WaitForSeconds(enemiesWaveData.TimeBetweenSpawnOfEnemies);

            EnemyStaticData enemyStaticData = _world.StaticDataService.EnemiesStaticData[enemiesWaveData.EnemyType];
            GameObject enemy = Instantiate(enemyStaticData.Prefab, enemiesWaveData.SpawnPoint, Quaternion.identity);
            
            AddEnemy(enemy);
            enemy.GetComponent<EnemyController>().Construct(_world, enemyStaticData.Data);
        }
    }

    private void AddEnemy(GameObject enemy)
    {
        DestroyObject destroyObject = enemy.GetComponent<DestroyObject>();
        if(destroyObject != null)
            destroyObject.Destroy += Remove;
        
        _enemies.Add(enemy);
    }

    private void Remove(GameObject enemies) => 
        _enemies.Remove(enemies);
}