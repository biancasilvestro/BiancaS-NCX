using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SimpleRandomWalkParameters_",menuName = "PCG/SimpleRandomWalkData")]

//create asset per creare facilmente istanze, oggetti
// il nome predefinito del file è SimpleRandomWalkParameters_ + numero incrementale
//l'altro è il percorso nel menu dell'editor

public class SimpleRandomWalkSO : ScriptableObject //asset che modifico nell'editor
{
    public int iterations = 10, walkleght = 10;
    public bool startRandomlyEachIteration = true;

}
