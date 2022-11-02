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

}
