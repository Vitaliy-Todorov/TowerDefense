using System.Collections.Generic;
using UnityEngine;

public class SpawnOfWorld : MonoBehaviour
{
    [SerializeField] 
    private World _world;
    [SerializeField] 
    private List<TowerController> _towers;

    private void Awake()
    {
        _world.StaticDataService = new StaticDataService();
        _world.GeneralData = new GeneralData(_world);
        
        _world.EnemiesSpawner.Construct(_world);

        _world.BuildTower.Construct(_world, _world.StaticDataService);

        foreach (var (towerType, towerStaticData) in _world.StaticDataService.TowersStaticData)
            _world.GeneralData.TowersData.Add(towerType, towerStaticData.Data);
        
        foreach (var (enemyType, towerStaticData) in _world.StaticDataService.EnemiesStaticData)
            _world.GeneralData.EnemiesData.Add(enemyType, towerStaticData.Data);

        _world.CentralTower.GetComponent<TowerController>()
            .Construct(_world, _world.GeneralData.TowersData[ETowerType.CentralTower]);
        
        foreach (TowerController tower in _towers) 
            tower.Construct(_world, _world.GeneralData.TowersData[tower.TowerType]);
    }
}