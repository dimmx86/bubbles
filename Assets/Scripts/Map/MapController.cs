using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Ball _prefabBall;
    [Space]
    [SerializeField] private int _width = 9;
    [SerializeField] private int _space = 5;
    [SerializeField] private int _sizeGenerator = 10;

    private Transform _parentBall;
    private Map _map;
    private MapGenerator _generator;
    private bool _isReady = false;
    private int _lastLine;

    public bool IsReady => _isReady;

    private void Awake()
    {
        _parentBall = _tilemap.transform;
        _lastLine = _space;

        _generator = new MapGenerator(_width);
        
        _map = new Map(_tilemap, _width, _space);

        AddNewBalls(_generator.GenerationMap(_sizeGenerator));

        _isReady = true;
    }

    private void AddNewBalls(BallColor[,] balls)
    {
        for (int y = 0; y < balls.GetLength(0); y++)
        {
            _lastLine++;

            for (int x = 0; x < balls.GetLength(1); x++)
            {
                Ball ball = Instantiate(_prefabBall, _parentBall);
                Vector2Int position = new Vector2Int(x, _lastLine);
                _map.AddBall(ball, position, balls[y, x]);
            }

        }
    }
}
