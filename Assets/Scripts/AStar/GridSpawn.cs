using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class GridSpawn
{
    public static void SpawnOfWorld(World world)
    {
        foreach (TileMarker tileMarker in world.GridGraph.TileGrid)
        {
            Object.Destroy(tileMarker.gameObjectToTile);
            GameObject prefabTile = world.StaticDataService.GeneralStaticData.GetTile(tileMarker.TileType);
            tileMarker.gameObjectToTile = Object.Instantiate(prefabTile, tileMarker.transform);
        }
    } 
}
