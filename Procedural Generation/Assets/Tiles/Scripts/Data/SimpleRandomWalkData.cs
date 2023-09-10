using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_",menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkData : ScriptableObject
{
    //scriptableObject
    //allows us to create through the create menu in the inspector
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
