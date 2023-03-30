using System;
using TMPro;
using UnityEngine;

public class World : MonoBehaviour
{
    public event Action UpdateWorld;
    
    public GeneralData GeneralData;
    public StaticDataService StaticDataService;
    public SystemOfSelectingObjects SystemOfSelectingObjects;

    public GridGraph GridGraph;

    public Transform Counter;
    public TMP_Text ScoreText;

    public GameObject CentralTower;

    public SpawnOfWorld SpawnOfWorld;
    public EnemiesSpawner EnemiesSpawner;
    public BuildTower BuildTower;
    
    private void Update() => 
        UpdateWorld?.Invoke();
}
