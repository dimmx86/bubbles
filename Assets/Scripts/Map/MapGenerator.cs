using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    private int _width;
    private int _maxIntColor;

    public MapGenerator(int width)
    {
        _width = width;
        _maxIntColor = System.Enum.GetValues(typeof(BallColor)).Length;
    }

    public BallColor[,] GenerationMap(int height)
    {
        BallColor[,] map = new BallColor[_width, height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = (BallColor)Random.Range(0, _maxIntColor);
            }
        }

        return map;
    }
}
