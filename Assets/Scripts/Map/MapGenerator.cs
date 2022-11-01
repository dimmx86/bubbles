using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    private int _width;
    private int _countColor;

    public MapGenerator(int width)
    {
        _width = width;
        _countColor = System.Enum.GetValues(typeof(BallColor)).Length;
    }

    public BallColor[,] GenerationMap(int height)
    {
        BallColor[,] map = new BallColor[height, _width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                map[y, x] = (BallColor)Random.Range(0, _countColor);
            }
        }

        return map;
    }
}
