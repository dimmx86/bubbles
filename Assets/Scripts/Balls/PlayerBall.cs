using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ColorsInBall _colors;
    [SerializeField] private SpriteRenderer _sprite;
    [Space]
    [SerializeField] private int _speed;

    private BallColor _color;

    public BallColor Color => _color;

    [HideInInspector] public UnityEvent<Vector3> OnCollisionBall;

    public void SetColor(BallColor color)
    {
        _color = color;
        _sprite.color = _colors.GetColor(color);
    }

    public void Fire(Vector2 point)
    {
        var direction = new Vector3(point.x - transform.position.x, point.y - transform.position.y, 0);
        direction.Normalize();
        _rb.velocity = direction * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Ball>(out Ball ball))
        {
           OnCollisionBall?.Invoke(transform.position);
        }
    }
}
