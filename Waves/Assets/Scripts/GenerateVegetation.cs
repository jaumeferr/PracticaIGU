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
    Vector3 planetCenter = Vector3.zero;

    public GenerateVegetation(Transform tree)
    {
        this.prefabTree = tree;
        this.numTrees = 2;
    }
    public void Generate()
    {
        treesPosition = new Vector3[numTrees];
        Vector3 planetCenter = Vector3.zero;

        //Limpiar terreno
        this.CleanTerrain();

        //Generar todos los puntos de posiciamiento de árboles
        this.GenerateTrees();
    }

    private void CleanTerrain()
    {

        //Limpiar el mapa de posibles árboles generados
        Transform terrainSeed = GameObject.Find("TerrainSeed").GetComponent<Transform>();
        for (int i = 0; i < terrainSeed.childCount; i++)
        {
            Destroy(terrainSeed.GetChild(i).gameObject);
        }
    }

    private void GenerateTrees()
    {
        int treeCount = 0;
        
        //Index of closest tree in trees array
        int closeTree;

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

            //Comprobar que la posición es válida respecto a la posición de los otros árboles.
            RaycastHit treePos;
            
            Debug.DrawLine(planetCenter, dirTree * 100, Color.yellow, 30);

            Ray treeRay = new Ray(planetCenter, dirTree);

            if (Physics.Raycast(treeRay, out treePos))
            {
                //Comprobar que no está cerca de los otros árboles
                bool tooClose = false;
                if (treeCount > 0)
                {
                    for (int i = 0; i < treeCount && !tooClose; i++)
                    {
                        if ((treePos.point - treesPosition[i]).magnitude < 3)
                        {
                            tooClose = true;

                            /*
                            if(!treesPosition[i].AlreadyScaled()){
                                closeTree = i;
                            }
                            */
                        }
                    }
                }

                //Si es el primero o no está muy cerca lo podemos generar
                if (!tooClose)
                {
                    treesPosition[treeCount] = treePos.point;
                    Instantiate(prefabTree, treePos.point, Quaternion.FromToRotation(Vector3.up, dirTree), GameObject.Find("TerrainSeed").GetComponent<Transform>());    

                } else{
                    /*Hacer árbol cercano más grande
                    treesPosition[i].Grow();
                    */
                    
                }
            }

            else
            {
                Debug.Log("Returns false");
            }

            treeCount++;

        }
    }
}
