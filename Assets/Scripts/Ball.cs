using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ColorsInBall _colors;
    [SerializeField] private SpriteRenderer _sprite;

    private BallColor _color;

    public BallColor Color => _color;

    public void SetColor(BallColor color)
    {
        _color = color;
        _sprite.color = _colors.GetColor(color);
    }

}
