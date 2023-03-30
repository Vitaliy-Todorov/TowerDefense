using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService
{
        private const string GeneralStaticDataPath = "Data/GeneralStaticData";
        private const string TowerDataPath = "Data/Towers";
        private const string EnemyDataPath = "Data/Enemies";

        private readonly GeneralStaticData _generalStaticData;
        private Dictionary<ETowerType, TowerStaticData> _towers;
        private Dictionary<EEnemyType, EnemyStaticData> _enemies;

        public GeneralStaticData GeneralStaticData => _generalStaticData;
        public Dictionary<ETowerType, TowerStaticData> TowersStaticData => _towers;
        public Dictionary<EEnemyType, EnemyStaticData> EnemiesStaticData => _enemies;


        public StaticDataService()
        {
            _generalStaticData = Resources.Load<GeneralStaticData>(GeneralStaticDataPath);
            _towers=
                Resources
                .LoadAll<TowerStaticData>(TowerDataPath)
                .ToDictionary(towerData => towerData.Type, towerData => towerData);
            _enemies=
                Resources
                .LoadAll<EnemyStaticData>(EnemyDataPath)
                .ToDictionary(enemyData => enemyData.Type, enemyData => enemyData);
        }

        public TowerStaticData GetTower(ETowerType towerType) =>
            _towers.TryGetValue(towerType, out TowerStaticData towerStaticData)
            ? towerStaticData
            : null;
        
        public EnemyStaticData GetEnemy(EEnemyType enemyType) =>
            _enemies.TryGetValue(enemyType, out EnemyStaticData enemyStaticData)
            ? enemyStaticData
            : null;
}