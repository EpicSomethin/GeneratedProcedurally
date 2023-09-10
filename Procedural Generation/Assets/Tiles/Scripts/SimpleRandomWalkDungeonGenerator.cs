using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; // this is used to access elements easier in things like arrays and hashsets
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator 

{
    [SerializeField]
    protected SimpleRandomWalkData SimpleRandomWalkParameters;
   

    protected override void RunProceduralGenerator()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(SimpleRandomWalkParameters, startPosition);
        tilemapVisualiser.clear();
        tilemapVisualiser.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualiser);
        foreach (var position in floorPositions) 
        {
            Debug.Log(position);
        // this is to see which positions have been added to the floor positions hashset
        }
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData parameters, Vector2Int position)//passed im the data, so we can use this method inside child classes of this generator
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path);
            //this adds the positions from the random walk algorithm that are created,
            //to the floor positions hashset
            // so each iteration of the random walk algorithm
            // is added onto one another ignoring any duplicates
            if(parameters.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            // this sets the starting position of the random walk algoirthm after each iteration, on a path
            // already created by the RWA (random walk algorithm) making the dungeon more expansive

        }
        return floorPositions;
    }

 
}
