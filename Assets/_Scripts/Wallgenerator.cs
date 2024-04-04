using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Wallgenerator //static perche non dipende da dati sepcifici delle istanze, le operazioni sono indipendenti. 
{
   public static void CreateWall(HashSet<Vector2Int> floorPositions, Tilemapvisualiser tilemapvisualiser)
    {
        var basicWallPosition = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
        var cornerWallPosition = FindWallsInDirections(floorPositions, Direction2D.diagonallDirectionList);
        CreateBasicWall(tilemapvisualiser, basicWallPosition,floorPositions);
        CreateCornerWalls(tilemapvisualiser, cornerWallPosition, floorPositions);

    }


    private static void CreateBasicWall(Tilemapvisualiser tilemapvisualiser, HashSet<Vector2Int> basicWallPosition, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPosition)
        {
            string neighbourgsBynaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                var neighbourposition = position + direction;
                if (floorPositions.Contains(neighbourposition)) //se c'è una cella adiacente 
                {
                    neighbourgsBynaryType += "1";
                }
                else
                {
                    neighbourgsBynaryType += "0";
                }
            }
            tilemapvisualiser.PaintSingleBasicWall(position, neighbourgsBynaryType);

        }
    }
    private static void CreateCornerWalls(Tilemapvisualiser tilemapvisualiser, HashSet<Vector2Int> cornerWallPosition, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPosition)
        {
            string neighboursBynaryType = "";
            foreach (var direction in Direction2D.eightDirectionList)
            {
                var neighbourposition = position + direction;
                if (floorPositions.Contains(neighbourposition))
                {
                    neighboursBynaryType += "1";
                }
                else
                {
                    neighboursBynaryType += "0";
                }

            }
            tilemapvisualiser.PaintSingleCornerWall(position, neighboursBynaryType);
        }
    }



    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach(var position in floorPositions)
        {
            foreach(var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition) == false) // se non esiste non c'è il pavimento adiacente e quindi c'è il vuoto e ci va il muro
                    wallPositions.Add(neighbourPosition); // questa posizione la identifico come la posizione per metterci un muro
            }
        }
        return wallPositions;
    }
}
