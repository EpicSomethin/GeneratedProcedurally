using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualiser : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    //creates a refernace to our tilemap, called floorTilemap

    [SerializeField]
    private TileBase floorTile, wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        //IEneumareble is a generic form of a collection that can be looked through, we can pass
        //through here anything: a list, a hashset or any other collection
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> Positions, Tilemap Tilemap, TileBase Tile)
    {
        foreach (var position in Positions)
        {
            PaintSingleTile(Tilemap, Tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        // this upcasts our position which is vector2int to vector3int, 
        //giving us our tilePosition
        tilemap.SetTile(tilePosition, tile);
    }
    public void clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        //this prevents tiles from stacking on top of eachother each time we generte a new level
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }
}
