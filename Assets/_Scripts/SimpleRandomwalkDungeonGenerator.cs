using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class SimpleRandomwalkDungeonGenerator : AbstractDungeonGenerator 
    // random walk generator class
    //classe derivata della classe Abstract..., puo ereditare tutti i suoi membri, sia per implemetarli che sovrascriverli

{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkSOParameters;

    protected override void RunProceduralGeneration()  //fai override perche stai implemetando il metod che avevi chiamato nella classe astratta
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkSOParameters, startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        Wallgenerator.CreateWall(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
        //GENERA LE POSIZIONI DEL FLOOR (casuali)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); //per memorizzare le posizioni
        //collezione di posizioni univoche all'interno di uno spazio bidimensionale, rappresenta una cella della mappa

        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkleght);
            floorPositions.UnionWith(path);
            if (parameters.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            //il percorso inizia casualmente da una delle posizioni già generate finora.
        }
        return floorPositions;
    }

   
}
