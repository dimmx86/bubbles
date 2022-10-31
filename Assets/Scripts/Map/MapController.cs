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


    private Map _map;
    private MapGenerator _generator;
    private bool _isReady = false;

    public bool IsReady => _isReady;

    private void Awake()
    {
        _generator = new MapGenerator(_width);
        
        _map = new Map(_tilemap, _width, _space);

        _isReady = true;
    }
}
