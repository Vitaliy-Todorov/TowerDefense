using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class World : MonoBehaviour
{
    public GeneralData GeneralData;
    public StaticDataService StaticDataService;
    
    public Transform Counter;
    public TMP_Text ScoreText;

    public GameObject CentralTower;

    public SpawnOfWorld SpawnOfWorld;
    public EnemiesSpawner EnemiesSpawner;
    public BuildTower BuildTower;
}
