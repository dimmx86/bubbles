using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Map 
{
    private Tilemap _tilemap;
    private int _width;
    private int _space;

    private List<Ball[]> _mapBalls;//_mapBalls[position.y][position.x]
    private Queue<Ball> _poolBall;


   public Map(Tilemap tilemap, int width, int space)
    {
        _mapBalls = new List<Ball[]>();
        _poolBall = new Queue<Ball>();
        _tilemap = tilemap;
        _space = space;
        _width = width;
    } 

    public void AddBall(Ball ball, Vector2Int position, BallColor color)
    {
        if (position.x >= _width || position.x < 0 || position.y < 0)
        {
            Debug.LogError("Incorrect position: " + position);
            return;
        }

        while (_mapBalls.Count <= position.y)
        {
            _mapBalls.Add(new Ball[_width]);
        }

        if (_mapBalls[position.y][position.x] != null)
        {
            DestroyBall(position);
        }

        ball.transform.position = _tilemap.GetCellCenterWorld(new Vector3Int(position.x, position.y));
        _mapBalls[position.y][position.x] = ball;
        ball.gameObject.SetActive(true);
        ball.SetBall(color, position);
    }

    public void DestroyBall(Vector2Int position)
    {
        _mapBalls[position.y][position.x].gameObject.SetActive(false);
        _poolBall.Enqueue(_mapBalls[position.y][position.x]);
        _mapBalls[position.y][position.x] = null;
    }

    public bool TryGetBall(out Ball ball)
    {
        ball = null;
        if (_poolBall.Count < 1)
        {
            return false;
        }
        else
        {
            ball = _poolBall.Dequeue();
            return true;
        }
    }

    public List<BallColor> ColorsInGame()
    {
        List<BallColor> colors = new List<BallColor>();

        foreach (var line in _mapBalls)
        {
            foreach (var item in line)
            {
                if (item != null)
                {
                    colors.Add(item.Color);
                }
            }
        }

        return colors;
    }

    public bool TryGetSameBalls(Vector2Int startPosition, BallColor color, int minBalls, out List<Vector2Int> positionSameBalls)
    {
        positionSameBalls = new List<Vector2Int>();
        Queue<Vector2Int> tempPositions = new Queue<Vector2Int>();
        bool[,] isPassed = NewPassedMap();

        tempPositions.Enqueue(startPosition);

        do
        {
            var position = tempPositions.Dequeue();
            if (position.x < 0 || position.x >= _width || position.y < 0 || position.y >= _mapBalls.Count)
            {
                continue;
            }
            if (_mapBalls[position.y][position.x] != null && isPassed[position.y, position.x] == false)
            {
                if (_mapBalls[position.y][position.x].Color == color)
                {
                    positionSameBalls.Add(position);

                    var neighborPositions = NeighborHex(position);
                    foreach (var item in neighborPositions)
                    {                        
                      tempPositions.Enqueue(item);
                    }
                }
            }
            isPassed[position.y, position.x] = true;
        } while (tempPositions.Count > 0);

        if (positionSameBalls.Count < minBalls)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool[,] NewPassedMap()
    {
        bool[,] isPassed = new bool[_mapBalls.Count, _width];
        for (int y = 0; y < _mapBalls.Count; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                isPassed[y, x] = false;
            }
        }

        return isPassed;
    }

    private Vector2Int[] NeighborHex(Vector2Int position)
    {
        Vector2Int[] result = new Vector2Int[6];

        result[0] = new Vector2Int(position.x, position.y + 1);
        result[1] = new Vector2Int(position.x, position.y - 1);
        result[2] = new Vector2Int(position.x + 1, position.y);
        result[3] = new Vector2Int(position.x - 1, position.y);

        if (position.y % 2 == 0)
        {
            result[4] = new Vector2Int(position.x - 1, position.y + 1);
            result[5] = new Vector2Int(position.x - 1, position.y - 1);
        }
        else
        {
            result[4] = new Vector2Int(position.x + 1, position.y + 1);
            result[5] = new Vector2Int(position.x + 1, position.y - 1);
        }

        return result;
    }

}
