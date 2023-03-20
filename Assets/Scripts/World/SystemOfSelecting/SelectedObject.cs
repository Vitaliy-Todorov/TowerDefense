using UnityEngine;

public class SelectedObject : MonoBehaviour
{
    [SerializeField] 
    private GameObject uiOnObject;
    
    public void Select() => 
        uiOnObject.SetActive(true);

    public void RemoveSelection() => 
        uiOnObject.SetActive(false);
}