using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms  //random walk algorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walklenght)
    // ALGORITMO DI GENERAZIONE RANDOMICO di una singola room
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); 

        path.Add(startPosition); //aggiugniamo al percorso la posizone di partenza
        var previousposition = startPosition; //inizializzazione

        for(int i = 0; i < walklenght; i++)
        {
            var newPosition = previousposition + Direction2D.GetRandomCardinalDIrection();
            path.Add(newPosition);
            previousposition = newPosition;
        }
        return path;
    }


    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLenght)
        //ALGROTIMO DI GENERAZIONE CORRIDOI
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDIrection(); //scegliamo direzione iniziale
        var currentPosition = startPosition; // pos iniziale
        corridor.Add(currentPosition);


        for(int i = 0; i < corridorLenght; i++) //iteriamo lungo la direzione corrente fino a corridor lenght 
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }


    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceTosplit, int minWidth, int minHeight)
        //ALGORITMO BSP 
        //per dividere ricorsivamente lo spazio in sottospazi piu piccoli  fino a una dimensione minima
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>(); //dati fifo , first in first out, contiene gli spazi da suddividere
        List<BoundsInt> roomsList = new List<BoundsInt>(); // contiente gli spazi suddivisi
        roomsQueue.Enqueue(spaceTosplit); // lo spazio inizale da suddividere viene aggiunto alla coda


        while(roomsQueue.Count > 0)  // finche ci stanno spazi da suddividere
        {
            var room = roomsQueue.Dequeue(); // room = uno spazio della coda
            if(room.size.y >= minHeight && room.size.x >= minWidth) // controlliamo se lo spazio estratto è abbastanza grande da essere diviso per le dimensioni minime
            {
                if(Random.value < 0.5f) // randomicamente viene scelto se fae split orizzontale o verticale
                {
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);

                    }else if(room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                { 
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room) // largehzza min sottospazi, la coda degli spazi da suddividere, spazio corrente da suddividere. 
    {
        var xSplit = Random.Range(1, room.size.x); // coordinata della divisione
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        // vengono creati due spazi room1 da 0 a xSplit
        // room2 da xSplit a room.size.x.
        roomsQueue.Enqueue(room1);// aggiungiamo i nuovi spazi per ulteriori divisioni
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}


public static class Direction2D
//puoi accedere a Direction2D.cardinalDirectionList e
//Direction2D.GetRandomCardinalDirection() direttamente
//senza dover creare un oggetto di tipo Direction2D.
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    //lista di oggetti, contiene le posizioni cardinali
    {
        new Vector2Int(0,1), //up
        new Vector2Int(1,0), // right
        new Vector2Int(0, -1), //down
        new Vector2Int(-1, 0)// left

    };
    public static List<Vector2Int> diagonallDirectionList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //up-right
        new Vector2Int(1,-1), // right-dpwn
        new Vector2Int(-1, -1), //down-left
        new Vector2Int(-1, 1)// left-up

    };

    public static List<Vector2Int> eightDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //up
        new Vector2Int(1,1), //up-right
        new Vector2Int(1,0), // right
        new Vector2Int(1,-1), // right-dpwn
        new Vector2Int(0, -1), //down
        new Vector2Int(-1, -1), //down-left
        new Vector2Int(-1, 0),// left
        new Vector2Int(-1, 1),// left-up

    };


    public static Vector2Int GetRandomCardinalDIrection()
    // può essere chiamato senza dover istanziare un oggetto della classe Direction2D
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
        //prendiamo un indice a caso dalla lista 
    }
  
}