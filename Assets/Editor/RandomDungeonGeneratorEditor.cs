using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
// la classe RandomDungeonGeneratorEditor  è un editor
// personalizzato della classe Abstract dungoen generator, e per tutte le classi derivate 


public class RandomDungeonGeneratorEditor : Editor 
{
    AbstractDungeonGenerator generator;


    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target; //assegniamo il target alla variabile
    }


    public override void OnInspectorGUI() //per l'interfaccia , UI
    {
        base.OnInspectorGUI(); //unity mantien le peroprietà serializzate e poi tu aggiungi quello che vuoi
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }
}
