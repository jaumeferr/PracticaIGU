using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [Range(0,300)]
    public int numTrees;
    public Transform prefabTree;

    GenerateVegetation vegetation;
    void Initialize() 
    {
        vegetation = new GenerateVegetation(prefabTree, numTrees);
        vegetation.GenerateTrees();
    }
}
