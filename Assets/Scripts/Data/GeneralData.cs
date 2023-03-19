using System.Collections.Generic;
using TMPro;

public class GeneralData
{
    private TMP_Text _scoreText;
    private float _score;
    public float Score
    {
        get => _score;
        set
        {
            _scoreText.text = value.ToString();
            _score = value;
        }
    }
    
    public Dictionary<ETowerType,TowerData> TowersData;
    public Dictionary<EEnemyType, EnemyData> EnemiesData;

    public GeneralData(World world)
    {
        _scoreText = world.ScoreText;

        TowersData = new Dictionary<ETowerType, TowerData>();
        EnemiesData = new Dictionary<EEnemyType, EnemyData>();
    }
}
