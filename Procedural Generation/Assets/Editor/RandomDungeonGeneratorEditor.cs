using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
// this will make all the child classes have a custom button to generate their own dungeon
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target;
        //this upcasts the type object to be type AbstractDungeonGenerator
        // this makes a referance to our generator that our custom inspector is an editor of
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
        //this creates a button in the GUI of AbstractDungeonGenerator and its child classes
        //in the editor, to allow creation of dungeons without starting the unity scene for each child
    }
}
