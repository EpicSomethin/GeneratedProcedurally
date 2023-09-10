using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walklength)
    //Vector2Int is a point in 2d space in unity that is represented as an integer,
    //we call this startposition
    //A HashSet is a collection that allows us to represent unique elements
    //if the type that we store it in, implements Equals or GetHashCode()
    //vector2int does override GetHashCode and Equals, this allows us to store it into the hash set data
    //wich allows us to remove duplicates since randomwalk algorithm can step on the same position twice
    // Hash set also gives methods to easily select a subset of data that we pass thorugh in two hashsets of data

    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        // this adds "start position" to this hashset called path
        var PreviousPosition = startPosition;

        for (int i = 0; i < walklength; i++)
        {
            var newPosition = PreviousPosition + RandomDirection2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            PreviousPosition = newPosition;
        }
        return path;
        // this part will be improved by making it go back to the starting position for x amount of times, this will change how big the dungeon will be, this variable will be called random walk amount
    }


    


    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        // uses list instead of hashset, because little duplicates will be made in creation
        //selects a single direction and walk in corridorlength distance, and takes the last
        //position of the path to get the next start position for another corridor to be made
        
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = RandomDirection2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for ( int i = 0; i < corridorLength; i++ ) 
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        
        }
        return corridor;
    }
}

public static class RandomDirection2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //The up direction
        new Vector2Int(1,0),//Right direction
        new Vector2Int(0,-1),//Down Direction
        new Vector2Int(-1,0),//Left direction
    
    
    };
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
    //we are going to call this to create a new random walk position from previous position
}

