using UnityEngine;
using UnityEngine.EventSystems;

public class BuildTower : MonoBehaviour
{
    [SerializeField] 
    private MenuBuildTower _menuBuildTower;
    
    private World _world;
    private StaticDataService _staticDataService;

    private Camera _mainCamera;
    private int _layerGround;

    public void Construct(World world, StaticDataService staticDataService)
    {
        _world = world;
        _staticDataService = staticDataService;
        _menuBuildTower.Construct(this);
        
        _mainCamera = Camera.main;
        _layerGround = LayerMask.NameToLayer("Ground");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickOnGround();
    }

    private void ClickOnGround()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100) && hit.collider.gameObject.layer == _layerGround)
        {
            Vector3 worldPosition = hit.point;
            
            int x = Mathf.RoundToInt(worldPosition.x);
            int y = Mathf.RoundToInt(worldPosition.z);
            
            _menuBuildTower.gameObject.SetActive(true);
            _menuBuildTower.transform.position = Input.mousePosition;
            _menuBuildTower.SetPositionOnGrid(new Vector3(x, 0, y));
        }
    }

    public GameObject BuildModel(ETowerType towerType, Vector3 position)
    {
        GameObject towerGO = Instantiate(_staticDataService.GetTower(towerType).PrefabModel, position, Quaternion.identity);
        towerGO.GetComponent<TowerModelController>()?.Construct(_world);
        return towerGO;
    }

    public GameObject Build(ETowerType towerType, Vector3 position)
    {
        GameObject towerGO = Instantiate(_staticDataService.GetTower(towerType).Prefab, position, Quaternion.identity);
        towerGO.GetComponent<TowerController>().Construct(_world, _world.GeneralData.TowersData[towerType]);
        return towerGO;
    }
}