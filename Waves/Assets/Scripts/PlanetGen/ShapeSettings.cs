using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius;
    public NoiseLayer noiseLayer;

    //[System.Serializable]
    public ShapeSettings(float planetRadius)
    {
        this.planetRadius = planetRadius;
    }

    public void InitializeNoiseLayer()
    {
        //Initialize NoiseLayer 1
        NoiseSettings noiseSettings = new NoiseSettings(
            (float)Random.Range(0.15f, 0.25f),  //Strength
            2,                                  //numLayers
            (float)Random.Range(0.5f, 0.6f),    //BaseRoughness
            (float)Random.Range(3.3f, 3.7f),    //Roughness
            (float)Random.Range(0.2f, 0.3f),    //Persistance
            Vector3.zero,                       //Centre    
            0);                                 //minValue

        noiseLayer = new NoiseLayer(noiseSettings);
    }

    public void InitializeVegetationNoiseLayer(){
        //Initialize NoiseLayer 1
        NoiseSettings noiseSettings = new NoiseSettings(
            (float)Random.Range(0.15f, 0.25f),  //Strength
            2,                                  //numLayers
            (float)Random.Range(0.5f, 0.6f),    //BaseRoughness
            (float)Random.Range(3.3f, 3.7f),    //Roughness
            (float)Random.Range(0.2f, 0.3f),    //Persistance
            Vector3.zero,                       //Centre    
            0);                                 //minValue

        noiseLayer = new NoiseLayer(noiseSettings);
    }
    public class NoiseLayer
    {
        public NoiseSettings noiseSettings;

        public NoiseLayer(NoiseSettings noiseSettings)
        {
            this.noiseSettings = noiseSettings;
        }
    }
}
