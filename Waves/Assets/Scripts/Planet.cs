using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    //[Range(2, 256)]
    public int resolution = 100;
    public float planetRadius = 40.0f;

    [HideInInspector]
    public ShapeSettings shapeSettings;

    ShapeGenerator shapeGenerator;

    [SerializeField, HideInInspector]
    public MeshFilter[] meshFilters;
    public TerrainFace[] terrainFaces;
    public Color planetColor = Color.black;
    EnemySpawner enemySpawner;

    private void OnValidate()
    {
        GeneratePlanet();
    }

    void Initialize()
    {
        //InitializeShapeSettings();
        shapeSettings = new ShapeSettings(planetRadius);
        shapeSettings.InitializeNoiseLayer();

        shapeGenerator = new ShapeGenerator(shapeSettings);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.tag = "PlanetFace";
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);

            meshFilters[i].gameObject.SetActive(true);
            if (!meshFilters[i].GetComponent<MeshCollider>())
            {
                meshFilters[i].gameObject.AddComponent<MeshCollider>().convex = true;
            }
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }

    void GenerateColours()
    {
        //rdPlanetColor = new Color((float)Random.Range(0, 255), (float)Random.Range(0, 255), (float)Random.Range(0, 255));

        GameObject[] faces = GameObject.FindGameObjectsWithTag("PlanetFace");
        foreach (GameObject face in faces)
        {
            face.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_EMISSION");
            face.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", planetColor);
        }
    }
}
