using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusGame
{
    StartPosition,
    Fire,
    CollisionBall,
}

public class GameController : MonoBehaviour
{
    [SerializeField] private InputController _input;
    [SerializeField] private MapController _map;
    [SerializeField] private PlayerBall _prefabPlayerBall;
    [SerializeField] private Transform _firePosition;

    private PlayerBall _playerBall;
    private bool _isFinished;
    private StatusGame _statusGame;
    private StatusGame _beforeStatusGame;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => _map.IsReady);

        _playerBall = Instantiate(_prefabPlayerBall, _firePosition);

        StartCoroutine(GameCycle());

    }

    private IEnumerator GameCycle()
    {
        _isFinished = false;
        while (!_isFinished)
        {
            StartCycle();
            yield return new WaitUntil(() => _statusGame == StatusGame.CollisionBall);
        }

        yield return new WaitForEndOfFrame();//temp

    }

    private void SwitchStatusGame(StatusGame newStatus)
    {
        _beforeStatusGame = _statusGame;
        _statusGame = newStatus;
    }

    private void StartCycle()
    {
        if (_map.TryGetRandomColor(out BallColor color))
        {
            _playerBall.transform.position = _firePosition.position;
            _playerBall.gameObject.SetActive(true);
            _playerBall.SetColor(color);

            _input.OnFire.AddListener(OnFire);
            SwitchStatusGame(StatusGame.StartPosition);
        }
        else
        {
            Finished(true);
        }
    }

    private void Finished(bool isWin)
    {
        _isFinished = true;
    }

    private void OnFire(Vector3 point)
    {
        _playerBall.Fire(point);
        _playerBall.OnCollisionBall.AddListener(OnCollisionBall);
        SwitchStatusGame(StatusGame.Fire);
    }

    private void OnCollisionBall(Vector3 position)
    {
        _playerBall.OnCollisionBall.RemoveListener(OnCollisionBall);
        _playerBall.gameObject.SetActive(false);
        SwitchStatusGame(StatusGame.CollisionBall);
    }
}


