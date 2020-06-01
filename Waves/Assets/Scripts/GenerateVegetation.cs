using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateVegetation : ScriptableObject
{
    [Range(0, 300)]
    public int numTrees;
    private Vector3[] treesPosition; //Cambiar prox a Tree[] ¿¿?
    private Transform terrainSeed;
    private Transform prefabTree;
    Vector3 planetCenter = Vector3.zero;
    Mesh[] planetSurface = new Mesh[6];

    public GenerateVegetation(Transform tree)
    {
        MeshFilter[] surfaceMeshFilters = GameObject.Find("Planet").GetComponent<Planet>().meshFilters;
        for (int i = 0; i < surfaceMeshFilters.Length; i++)
        {
            planetSurface[i]=surfaceMeshFilters[i].sharedMesh;
        }

        this.prefabTree = tree;
        this.numTrees = 40;
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
        Vector3 vertex;

        while (treeCount < numTrees)
        {
            //Vertice aleatorio en el planet
            int rdFace = Random.Range(0,6);
            int vertexCount = planetSurface[rdFace].vertices.Length;
            int rdVertex = Random.Range(0, vertexCount);
            vertex = planetSurface[rdFace].vertices[rdVertex];

            //Comprobar que no se ha usado ese vértice
            if(!vertexUsed(vertex, treeCount)){
                Vector3 dirTree = vertex - planetCenter;
                Instantiate(prefabTree, vertex, Quaternion.FromToRotation(Vector3.up, dirTree), GameObject.Find("TerrainSeed").GetComponent<Transform>());
                treesPosition[treeCount] = vertex;
                treeCount++;
            }
        }    
    }

    private void GenerateGrass(){

    }

    private bool vertexUsed(Vector3 vertex, int count){
        bool used = false;

        for(int i = 0; i < count && !used; i++){
            if(vertex == treesPosition[i]){
                used = true;
            }
        }
        return used;
    }
}
