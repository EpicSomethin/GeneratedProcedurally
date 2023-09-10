using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    //abstract makes it so we can create editor for this class, so we can work with other generator algorithms
    [SerializeField] // this exposes this variable in the editor if made public, through the inspector
    protected TilemapVisualiser tilemapVisualiser = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;// a variable type used to represent a place in 2D space in the viewport

    public void GenerateDungeon()
    {
        tilemapVisualiser.clear();
        RunProceduralGenerator();


    }

    protected abstract void RunProceduralGenerator();
}
