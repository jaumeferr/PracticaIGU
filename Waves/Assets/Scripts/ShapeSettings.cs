using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius;
    public NoiseLayer noiseLayer;

    //[System.Serializable]
    public ShapeSettings()
    {
        this.planetRadius = 40;
    }

    public void InitializeNoiseLayer()
    {
        //Initialize NoiseLayer 1
        NoiseSettings noiseSettings = new NoiseSettings(
            (float)Random.Range(0.15f, 0.25f),
            2,
            (float)Random.Range(0.5f, 0.6f),
            (float)Random.Range(3.3f, 3.7f),
            (float)Random.Range(0.2f, 0.3f),
            Vector3.zero,
            0);

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
