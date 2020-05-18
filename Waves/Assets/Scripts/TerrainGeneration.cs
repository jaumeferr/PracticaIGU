using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    GenerateVegetation vegetation;

    public Transform tree;

   private void Awake() 
    {
        vegetation = new GenerateVegetation(tree);
        vegetation.Generate();
    }

}
