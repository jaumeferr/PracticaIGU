using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateVegetation : MonoBehaviour
{
    [Range(0, 300)]
    public int numTrees;
    public int numRocks;
    public int numGrassGroups;
    private Grass grass;
    private Tree[] trees;
    private Rock[] rocks;


    //INSTANCIAS PREFABS
    private Transform terrainSeed;
    public Transform prefabTree_1;
    public Transform prefabTree_2;

    public Transform prefabRock_1;
    public Transform prefabRock_2;

    public Transform prefabGrass_1;

    public Transform prefabGrass_2;


    //PROPIEDADES PLANETA
    Vector3 planetCenter = Vector3.zero;
    Mesh[] planetSurface = new Mesh[6];

    public void Generate()
    {
        MeshFilter[] surfaceMeshFilters = GameObject.Find("Planet").GetComponent<Planet>().meshFilters;
        for (int i = 0; i < surfaceMeshFilters.Length; i++)
        {
            planetSurface[i] = surfaceMeshFilters[i].sharedMesh;
        }

        trees = new Tree[numTrees];
        rocks = new Rock[numRocks];
        Vector3 planetCenter = Vector3.zero;

        //Limpiar terreno
        this.CleanTerrain();

        //Generar 
        this.GenerateTrees();
        this.GenerateRocks();
        this.GenerateGrass();
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

    //------------------------------------------------------ GENERATION --------------------------------------------------------------------
    private void GenerateTrees()
    {
        int treeCount = 0;
        Vector3 vertex;

        while (treeCount < numTrees)
        {
            //Vertice aleatorio en el planet
            vertex = randomVertex();

            //Comprobar que no se ha usado ese vértice
            if (!vertexUsedTree(vertex, treeCount))
            {
                //Si se acerca a otro árbol, hacer más grande ese árbol
                for (int i = 0; i < treeCount - 1; i++)
                {
                    if (Vector3.Magnitude(vertex - trees[i].obj.GetComponent<Transform>().position) < 7) //AJUSTAR --------------------
                    {
                        //Hacer más grande
                        trees[i].Grow(trees[i].size + 1);

                        //Tamaño max?
                    }
                }
                //Generar árbol tipo 1 o 2
                Vector3 dirTree = vertex - planetCenter;
                int treeType = Random.Range(1, 3);
                Transform tree;

                if (treeType == 1)
                {
                    tree = Instantiate(prefabTree_1, vertex, Quaternion.FromToRotation(Vector3.up, dirTree), GameObject.Find("TerrainSeed").GetComponent<Transform>());
                }
                else
                {
                    tree = Instantiate(prefabTree_2, vertex, Quaternion.FromToRotation(Vector3.up, dirTree), GameObject.Find("TerrainSeed").GetComponent<Transform>());

                }
                trees[treeCount] = new Tree(1, tree);
                treeCount++;
            }
        }
    }

    private void GenerateRocks()
    {
        float size = Random.Range(10, 15);

        int rockCount = 0;
        Vector3 vertex;

        while (rockCount < numRocks)
        {
            //Vertice aleatorio en el planet
            vertex = randomVertex();

            //Comprobar que no se ha usado ese vértice
            if (!vertexUsedTree(vertex, numTrees) && !vertexUsedRock(vertex, rockCount))
            {
                bool grown = false;
                //Si está cerca de otra roca
                for (int i = 0; i < rockCount - 1 && !grown; i++)
                {
                    if (Vector3.Magnitude(vertex - rocks[i].obj.GetComponent<Transform>().position) < 5)
                    {
                        rocks[i].Grow(2);
                        grown = true;
                    }
                }

                //Generar roca tipo 1 o 2
                Vector3 dirRock = vertex - planetCenter;
                int rockType = Random.Range(1, 3);
                Transform rock;

                if (rockType == 1)
                {
                    rock = Instantiate(prefabRock_1, vertex, Quaternion.FromToRotation(Vector3.up, dirRock), GameObject.Find("TerrainSeed").GetComponent<Transform>());
                }
                else
                {
                    rock = Instantiate(prefabRock_2, vertex, Quaternion.FromToRotation(Vector3.up, dirRock), GameObject.Find("TerrainSeed").GetComponent<Transform>());

                }
                rocks[rockCount] = new Rock(size, rock);
                rockCount++;
            }
        }
    }

    private void GenerateGrass()
    {
        this.grass = new Grass(0.2f);

        foreach (TerrainFace tf in GameObject.Find("Planet").GetComponent<Planet>().terrainFaces)
        {
            for (int y = 0; y < tf.resolution; y++)
            {
                for (int x = 0; x < tf.resolution; x++)
                {
                    int i = x + y * tf.resolution;
                    Vector3 vertex = tf.mesh.vertices[i];
                    float prob = grass.CalculateGrassProb(vertex);

                    Vector3 dirGrass = (vertex - planetCenter).normalized;

                    if (prob > this.grass.minGrassValue)
                    {
                        //Instanciar yerba buena
                        int grassType = Random.Range(1, 3);

                        if (grassType == 1)
                        {
                            Instantiate(prefabGrass_1, vertex, Quaternion.FromToRotation(Vector3.up, dirGrass), GameObject.Find("TerrainSeed").GetComponent<Transform>());
                        }
                        else
                        {
                            Instantiate(prefabGrass_2, vertex, Quaternion.FromToRotation(Vector3.up, dirGrass), GameObject.Find("TerrainSeed").GetComponent<Transform>());
                        }
                    }
                }
            }
        }
    }
    //------------------------------------------------------ UTILS --------------------------------------------------------------------

    private bool vertexUsedTree(Vector3 vertex, int count)
    {
        bool used = false;

        for (int i = 0; i < count && !used; i++)
        {
            if (vertex == trees[i].obj.GetComponent<Transform>().position)
            {
                used = true;
            }
        }
        return used;
    }

    private bool vertexUsedRock(Vector3 vertex, int count)
    {
        bool used = false;

        for (int i = 0; i < count && !used; i++)
        {
            if (vertex == rocks[i].obj.GetComponent<Transform>().position)
            {
                used = true;
            }
        }
        return used;
    }

    private Vector3[] createCluster(int maxRadius, Vector3 initialPoint)
    {
        Vector3[] cluster = new Vector3[Random.Range(10, 20)];

        return cluster;
    }

    private Vector3 randomVertex()
    {
        int rdFace = Random.Range(0, 6);
        int vertexCount = planetSurface[rdFace].vertices.Length;
        int rdVertex = Random.Range(0, vertexCount);
        return planetSurface[rdFace].vertices[rdVertex];
    }


    //------------------------------------------------------ CLASSES --------------------------------------------------------------------
    private class Grass
    {
        public Vector3 groupCenter;
        public int radius;
        public GameObject[] objs;
        public ShapeSettings grassShapeSettings;
        public NoiseFilter grassNoiseFilter;
        public float minGrassValue;

        public Grass(int size, Vector3 center, Transform[] ts)
        {
            this.groupCenter = center;
            this.radius = size;

            for (int i = 0; i < ts.Length; i++)
            {
                objs[i] = ts[i].gameObject;
            }
        }

        public Grass(float minGrassValue)
        {
            ShapeSettings grassShapeSettings = new ShapeSettings(GameObject.Find("Planet").GetComponent<Planet>().planetRadius);
            grassShapeSettings.InitializeNoiseLayer();
            this.grassNoiseFilter = new NoiseFilter(grassShapeSettings.noiseLayer.noiseSettings);
            this.minGrassValue = minGrassValue;
        }

        public float CalculateGrassProb(Vector3 point)
        {

            float prob = grassNoiseFilter.Evaluate(point);

            return prob;
        }

    }

    private class Rock
    {
        public float size;
        public GameObject obj;

        public Rock(float size, Transform t)
        {
            this.obj = t.gameObject;
            Resize(size);
        }

        public void Resize(float size)
        {
            this.size = size;
            obj.GetComponent<Transform>().localScale = new Vector3(size, size * 2, size);
        }

        public void Grow(float size)
        {
            this.size = this.size + size;
            obj.GetComponent<Transform>().localScale = new Vector3(obj.GetComponent<Transform>().localScale.x + size, obj.GetComponent<Transform>().localScale.y + size * 2, obj.GetComponent<Transform>().localScale.z + size);
        }
    }

    private class Tree
    {
        public float size;
        public GameObject obj;

        public Tree(float size, Transform t)
        {
            this.size = size;
            this.obj = t.gameObject;
        }

        public void Grow(float size)
        {
            this.size = size;
            obj.GetComponent<Transform>().localScale = new Vector3(size, size, size);
        }
    }
}
