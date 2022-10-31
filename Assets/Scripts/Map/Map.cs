using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Map 
{
    private Tilemap _tilemap;
    private int _width;
    private int _space;

    private Ball[,] _mapBalls;
    private Queue<Ball> _poolBall;


   public Map(Tilemap tilemap, int width, int space)
    {
        _tilemap = tilemap;
        _space = space;
        _width = width;
    } 
}
