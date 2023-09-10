using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
  public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualiser tilemapVisualiser)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, RandomDirection2D.cardinalDirectionsList);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualiser.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> DirectionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {//searches each floorposition that is generated from the algorithm
            foreach (var direction in DirectionsList)
            {// and searches each surrounding direction for a Vector2Int value not
                // in the hashset floorpositions, and saves it as a potential wall in the wallposition hashset
                var neighborPosition = position + direction;
                if(floorPositions.Contains(neighborPosition)== false)
                    wallPositions.Add(neighborPosition);
                

                
            }
        }
        return wallPositions;    }
}
