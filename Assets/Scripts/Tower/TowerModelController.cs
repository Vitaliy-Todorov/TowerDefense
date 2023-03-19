using UnityEngine;

public class TowerModelController : MonoBehaviour
{
    private World _world;

    public void Construct(World world)
    {
        _world = world;
    }
}