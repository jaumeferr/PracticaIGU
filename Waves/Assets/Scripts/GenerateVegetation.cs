using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateVegetation : ScriptableObject
{
    [Range(0, 300)]
    public int numTrees;
    private Vector3[] treesPosition;
    private Transform terrainSeed;
    private Transform prefabTree;

    public GenerateVegetation(Transform tree)
    {
        this.prefabTree = tree;
        this.numTrees = 20;
    }
    public void Generate()
    {
        //Limpiar el mapa de posibles árboles generados
        Transform terrainSeed = GameObject.Find("TerrainSeed").GetComponent<Transform>(); 
        for (int i = 0; i < terrainSeed.childCount; i++)
        {
            Destroy(terrainSeed.GetChild(i).gameObject);
        }

        Vector3 planetCenter = Vector3.zero;
        int treeCount = 0;
        treesPosition = new Vector3[numTrees];

        while (treeCount < numTrees)
        {
            //Generar angulos a,b aleatorios
            //Futuro --> Generar zonas con especial concentración de árboles.
            float alfa = Random.Range(0.0f, 2 * Mathf.PI);
            float beta = Random.Range((-1) * Mathf.PI / 2, Mathf.PI / 2);

            //Set new tree position when a,b are given
            float treeX = Mathf.Cos(beta) * Mathf.Cos(alfa);
            float treeY = Mathf.Cos(beta) * Mathf.Sin(alfa);
            float treeZ = Mathf.Sin(beta);

            Vector3 dirTree = new Vector3(treeX, treeY, treeZ);

            //Raycast a algún punto de la superficie del planeta
            RaycastHit treePos;
            //Debug.DrawRay(planetCenter, dirTree * 100, Color.red, 1000);

            if (Physics.Raycast(planetCenter, dirTree, out treePos))
            {
                
                //Comprobar que no está cerca de los otros árboles
                bool tooClose = false;
                if(treeCount > 0){
                    for (int i = 0; i < treeCount && !tooClose; i++)
                    {
                        if((treePos.point - treesPosition[i]).magnitude > 10){
                            tooClose = true;
                        }
                    }
                }

                //Si es el primero o no está muy cerca lo podemos generar
                if(!tooClose){
                    Instantiate(prefabTree, treePos.point, Quaternion.FromToRotation(Vector3.up, dirTree), GameObject.Find("TerrainSeed").GetComponent<Transform>());
                    treesPosition[treeCount] = treePos.point;
                    treeCount ++;
                }
            }
        }

        int idasd = 0;
    }
}
