using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Vector2Int Size = Vector2Int.one;
    
    public Renderer MainRenderer;
    private Color _normalColor;

    private void Awake()
    {
        _normalColor = MainRenderer.material.color;
    }

    public void SetTransparent(bool available)
    {
        if (available) 
            MainRenderer.material.color = Color.green;
        else
            MainRenderer.material.color = Color.red;
    }

    public void SetNormal() => 
        MainRenderer.material.color = _normalColor;

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = new Color(.3f, 100f, 100f, .5f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
