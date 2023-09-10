using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator


{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;

    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    protected override void RunProceduralGenerator()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        // this is for creating room at the end  of a corridor

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindALLDeadEnds(floorPositions);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);
        // this contains both the corridor positions and room positions

        tilemapVisualiser.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualiser);
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                // for every dead end, if it doesnt already contain a room, create a room
                var room = RunRandomWalk(SimpleRandomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindALLDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighborCount = 0;
            foreach (var direction in RandomDirection2D.cardinalDirectionsList)
            {
                if(floorPositions.Contains(position+ direction))
                    neighborCount++;
              
            }
            if (neighborCount == 1)
                deadEnds.Add(position);
            //this looks for all surrounding tiles on a dead end,
            // and if it contains only one tile ( the tile leading it to the dead end)
            // then it is counted as a dead end, and so this position is added to the Hashset
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        //this generates rooms based on yet another configuration to see
        //if we want all potential rooms to be generated

        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        // this randomly sorts our potentialRoomPosition hashset and take from it our amount of rooms to be created
        // from the roomToCreatCount and makes it into a list

        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(SimpleRandomWalkParameters, roomPosition);
            // this generates rooms using the random walk algoirthm, at the positions 
            // selected by our linq library random sort
            roomPositions.UnionWith(roomFloor); // to avoid repetitions in our hashsets
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition); // this adds the beggining of a corridor to the hashset

        for (int i = 0; i < corridorCount; i++)
        {

            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);// this adds the end of a corridor to the hashset

            floorPositions.UnionWith(corridor);

        
        }

        
    }
}
