using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeGenerator
{
    protected Map map;
    protected System.Random pseudoRandom;

    public MazeGenerator(Map map) {
        this.map = map;
        this.pseudoRandom = map.GetRandomGenerator();
    }

    public abstract void generate();
}
