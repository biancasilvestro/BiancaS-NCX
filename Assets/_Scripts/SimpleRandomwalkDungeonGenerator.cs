using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class SimpleRandomwalkDungeonGenerator : AbstractDungeonGenerator //SimpleRandomWAlkDungeonGenerator
    // random walk generator class
{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkSOParameters; 

   



    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkSOParameters, startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        Wallgenerator.CreateWall(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floowPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkleght);
            floowPositions.UnionWith(path);
            if (parameters.startRandomlyEachIteration)
                currentPosition = floowPositions.ElementAt(Random.Range(0, floowPositions.Count));
        }
        return floowPositions;
    }

   
}
