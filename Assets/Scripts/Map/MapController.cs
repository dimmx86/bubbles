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
    [Space]
    [SerializeField] private float _delayDestroyBall = 0.1f;
    [SerializeField] private int _minMathBall = 3;

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

    public void AddBall(BallColor color, Vector3 worldPosition)
    {
        Ball ball = Instantiate(_prefabBall, _parentBall);
        Vector2Int position = (Vector2Int)_tilemap.WorldToCell(worldPosition);
        if (position.x < 0)
        {
            position.x = 0;
        }
        else if (position.x >= _width)
        {
            position.x = _width - 1;
        }
        print(position);
        _map.AddBall(ball, position, color);
        if (_map.TryGetSameBalls(position,color,_minMathBall, out List<Vector2Int> positionSameBalls))
        {
            StartCoroutine(DestroyBalls(positionSameBalls));
        }
    }

    private IEnumerator DestroyBalls(List<Vector2Int> positions)
    {
        var delay = new WaitForSeconds(_delayDestroyBall);
        foreach (var item in positions)
        {
            yield return delay;
            _map.DestroyBall(item);
        }
    }

    public bool TryGetRandomColor(out BallColor color)
    {
        List<BallColor> colors = _map.ColorsInGame();
        if (colors.Count < 1)
        {
            color = BallColor.Red;
            return false;
        }
        else
        {
            color = colors[Random.Range(0, colors.Count)];
            return true;
        }
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
