using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    Vector3 position;
    float scale;
    Transform tfTree;

    public void GenerateTree(){

    }

    public bool AlreadyScaled()
    {
        return scale > 1;
    }

    public void Grow(){
        tfTree.localScale *= 2;
        this.scale = 2;
    }
}