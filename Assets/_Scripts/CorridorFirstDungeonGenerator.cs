using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomwalkDungeonGenerator // a sua volta classe derivata
{
    [SerializeField]
    private int corridorLenght = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    public float roomPercent = 0.8f;
    



    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPosition = new HashSet<Vector2Int>();


        createCorridors(floorPositions, potentialRoomPosition);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPosition);

        List<Vector2Int> deadEnds = FindAllDeadends(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        Wallgenerator.CreateWall(floorPositions, tilemapVisualizer);
    }




    private List<Vector2Int> FindAllDeadends(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighbourCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if (floorPositions.Contains(position + direction))
                    neighbourCount++;
            }
            if (neighbourCount == 1)
                deadEnds.Add(position); // scorre tutte le position del corridor e se c'è un solo vicino allora la considera dead end
        }
        return deadEnds;
    }


    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach(var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkSOParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }



    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPosition)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPosition.Count * roomPercent); // quantità di stanze che vuoi 

        List<Vector2Int> roomsToCreate = potentialRoomPosition.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        //lista con numero casuale di posizioni potenziali delle stanze prelevate dal set potentialRoomPosition.
        //le posizioni vengono mescolate random con OrderBy...
        //prendiamo un numero di posizioni pari a roomToCreateCount utilizzando Take(roomToCreateCount).

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkSOParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void createCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPosition)
    {
        var currentPosition = startPosition;
        potentialRoomPosition.Add(currentPosition);

        for(int i = 0; i < corridorCount; i++) // corridor count è il numero di corrioi che vogliamo
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLenght);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPosition.Add(currentPosition); // il rpossimo corridoio inziia dove il vecchio finisce,
                                                        // riempiamo il potenital room position qua di possibili posizioni a fine corridoi dove piazzare le nuove roms
            floorPositions.UnionWith(corridor); // posizoni del corridoio unite alla mappa del pavimento
        }
    }
}
