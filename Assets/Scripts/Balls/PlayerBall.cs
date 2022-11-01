using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] private ColorsInBall _colors;
    [SerializeField] private SpriteRenderer _sprite;
    [Space]
    [SerializeField] private int _speed;

    private BallColor _color;

    public BallColor Color => _color;

    [HideInInspector] public UnityEvent<PlayerBall> OnCollisionBall;

    public void SetColor(BallColor color)
    {
        _color = color;
    }

    public void Fire(Vector2 direction)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
