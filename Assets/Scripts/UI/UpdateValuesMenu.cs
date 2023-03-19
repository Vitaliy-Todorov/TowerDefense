using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValuesMenu : MonoBehaviour
{
    [SerializeField] 
    private World _world;
    
    [SerializeField]
    private Button _updateHealth;
    [SerializeField]
    private Button _updateRestoringHealth;
    [SerializeField]
    private Button _updateDamage;
    [SerializeField]
    private Button _updateShootingSpeed;
    
    [SerializeField]
    private TMP_Text _costHealthText;
    [SerializeField]
    private TMP_Text _costRestoringHealthText;
    [SerializeField]
    private TMP_Text _costDamageText;
    [SerializeField]
    private TMP_Text _costShootingSpeedText;
    
    [SerializeField]
    private float _costHealth;
    [SerializeField]
    private float _costRestoringHealth;
    [SerializeField]
    private float _costDamage;
    [SerializeField]
    private float _costShootingSpeed;
    
    void Start()
    {
        _costHealthText.text = _costHealth.ToString();
        _costRestoringHealthText.text = _costRestoringHealth.ToString();
        _costDamageText.text = _costDamage.ToString();
        _costShootingSpeedText.text = _costShootingSpeed.ToString();
        
        _updateHealth.onClick.AddListener(UpdateHealth);
        _updateRestoringHealth.onClick.AddListener(UpdateRestoringHealth);
        _updateDamage.onClick.AddListener(UpdateDamage);
        _updateShootingSpeed.onClick.AddListener(UpdateShootingSpeed);
    }

    private void UpdateHealth()
    {
        if((_world.GeneralData.Score - _costHealth) < 0)
            return;

        _world.GeneralData.Score -= _costHealth;
        _world.GeneralData.TowersData[ETowerType.CentralTower].Health += 5;
        
        float costFactor = 2;
        _costHealth += costFactor;
        _costHealthText.text = _costHealth.ToString();
    }

    private void UpdateRestoringHealth()
    {
        if((_world.GeneralData.Score - _costRestoringHealth) < 0)
            return;

        _world.GeneralData.Score -= _costRestoringHealth;
        _world.GeneralData.TowersData[ETowerType.CentralTower].RestoringHealth += 1;
        
        float costFactor = 1;
        _costRestoringHealth += costFactor;
        _costRestoringHealthText.text = _costRestoringHealth.ToString();
    }

    private void UpdateDamage()
    {
        if((_world.GeneralData.Score - _costDamage) < 0)
            return;

        _world.GeneralData.Score -= _costDamage;
        _world.GeneralData.TowersData[ETowerType.CentralTower].Dameg += 1;
        
        float costFactor = 2;
        _costDamage *= costFactor;
        _costDamageText.text = _costDamage.ToString();
    }
    
    private void UpdateShootingSpeed()
    {
        if((_world.GeneralData.Score - _costShootingSpeed) < 0)
            return;

        if (_world.GeneralData.TowersData[ETowerType.CentralTower].ShootingSpeed > 0.5)
        {
            _world.GeneralData.Score -= _costShootingSpeed;
            _world.GeneralData.TowersData[ETowerType.CentralTower].ShootingSpeed -= 0.6f;
        
            float costFactor = 2;
            _costShootingSpeed += costFactor;
            _costShootingSpeedText.text = _costShootingSpeed.ToString();
        }
        else 
            Destroy(_updateShootingSpeed.gameObject);
    }
}