using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ColorsInBall _colors;
    [SerializeField] private SpriteRenderer _sprite;

    private BallColor _color;
    private Vector2Int _position;


    public BallColor Color => _color;
    public Vector2Int Position => _position;

    public void SetBall(BallColor newColor, Vector2Int newPosition)
    {
        _position = newPosition;
        _color = newColor;
        _sprite.color = _colors.GetColor(newColor);
    }

}
