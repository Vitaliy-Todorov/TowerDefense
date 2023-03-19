using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public delegate void Remove(GameObject gameObject);
    public new event Remove Destroy;
    
    private void OnDestroy() => 
        Destroy?.Invoke(gameObject);
}