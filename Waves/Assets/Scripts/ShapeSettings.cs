using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius;
    public NoiseLayer[] noiseLayers;
    
    //[System.Serializable]
    public ShapeSettings(){
        this.planetRadius = 40;
        this.noiseLayers = new NoiseLayer[2];
    }

    public void InitializeNoiseLayers(){
        //Initialize NoiseLayer 1
        NoiseSettings noiseSettings1 = new NoiseSettings(
            (float)Random.Range(0.15f,0.25f), 
            2, 
            (float)Random.Range(0.5f, 0.6f), 
            (float)Random.Range(3.3f, 3.7f), 
            (float)Random.Range(0.2f, 0.3f), 
            Vector3.zero, 
            0);

        //Initialize Noise Layer 2
        NoiseSettings noiseSettings2 = new NoiseSettings(
            (float)Random.Range(0.5f,1), 
            2, 
            (float)Random.Range(0.4f,1), 
            2, 
            (float)Random.Range(0.1f, 0.3f), 
            Vector3.zero, 
            0);

        noiseLayers[0] = new NoiseLayer(noiseSettings1, false);
        noiseLayers[1] = new NoiseLayer(noiseSettings2, true);
    }
    public class NoiseLayer{
        public bool enable = true;
        public bool useFirstLayerAsMask;
        public NoiseSettings noiseSettings;

        public NoiseLayer(NoiseSettings noiseSettings, bool useFirstLayerAsMask){
            this.noiseSettings = noiseSettings;
            this.useFirstLayerAsMask = useFirstLayerAsMask;
        }
    }
}
