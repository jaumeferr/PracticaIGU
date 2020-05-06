using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateVegetation : ScriptableObject
{
    [Range(0, 300)]
    int numTrees;
    private Vector3[] treesPosition;
    Transform prefabTree;

    public GenerateVegetation(Transform prefabTree, int numTrees)
    {
        this.prefabTree = prefabTree;
        this.numTrees = numTrees;
    }
    public void GenerateTrees()
    {
        Vector3 planetCenter = Vector3.zero;
        int treeCount = 0;
        treesPosition = new Vector3[numTrees];

        while (treeCount < numTrees)
        {
            //Generar angulos a,b aleatorios
            //Futuro --> Generar zonas con especial concentración de árboles.
            float a = Random.Range(0.0f, 2 * Mathf.PI);
            float b = Random.Range((-1) * Mathf.PI / 2, Mathf.PI / 2);

            //Set new tree position when a,b are given
            float x = Mathf.Cos(b) * Mathf.Cos(a);
            float y = Mathf.Cos(b) * Mathf.Sin(a);
            float z = Mathf.Sin(b);

            Vector3 dir = new Vector3(x, y, z);

            //Raycast a algún punto de la superficie del planeta
            RaycastHit hit;

            if (Physics.Raycast(planetCenter, dir, out hit))
            {
                bool tooNear = false;

                if (treesPosition[0] != null)
                {
                    //Comprobar que el arbol no está muy cerca de otros árboles
                    for (int i = 0; i < treesPosition.Length && !tooNear; i++)
                    {
                        if ((treesPosition[i] - hit.point).magnitude < 2)
                        {
                            tooNear = true;
                        }
                    }
                }

                if (!tooNear)
                {
                    //Colocar árbol en la posición indicada
                    //Añadir colliders, rigidbody -> isKinematic
                    Debug.Log("Tree: " + hit.point);
                    Instantiate(prefabTree, hit.point + hit.point.normalized, Quaternion.identity);
                    //Orientar árbol verticalmente

                    treesPosition[treeCount] = hit.point + hit.point.normalized;
                    treeCount++;
                }
            }
        }
    }
}
