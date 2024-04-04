using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected Tilemapvisualiser tilemapVisualizer = null;  //non accessibili a classi esterne, ma solo interne e derivate.
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
    //indichi che il metod è sia astratto che protetto.
    //Questa classe non è responsabile della impletenzaione concreta del metod
}
