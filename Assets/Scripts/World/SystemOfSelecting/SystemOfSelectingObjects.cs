using UnityEngine;
using UnityEngine.EventSystems;

public class SystemOfSelectingObjects
{
    
    private World _world;

    private SelectedObject _selectedObject;

    private Camera _mainCamera;

    public SystemOfSelectingObjects(World world)
    {
        _world = world;
        _world.UpdateWorld += Update;
        
        _mainCamera = Camera.main;
    }
    
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _selectedObject?.RemoveSelection();
            
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 100))
            {
                _selectedObject = hit.collider.GetComponent<SelectedObject>();
                _selectedObject?.Select();
            }
        }
    }
}