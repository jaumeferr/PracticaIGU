using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public float strength;
    [Range (1,8)]
    public int numLayers;
    public float baseRoughness;
    public float roughness;
    public float persistance;
    public Vector3 centre;
    public float minValue;

    public NoiseSettings(float strength, int numLayers, float baseRoughness, float roughness, float persistance, Vector3 centre, float minValue){
        this.strength = strength;
        this.numLayers = numLayers;
        this.baseRoughness = baseRoughness;
        this.roughness = roughness;
        this.persistance = persistance;
        this.centre = centre;
        this.minValue = minValue;
    }
}
