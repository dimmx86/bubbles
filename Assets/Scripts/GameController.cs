using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private InputController _input;
    [SerializeField] private MapController _map;
    [SerializeField] private PlayerBall _prefabPlayerBall;
    [SerializeField] private Transform _firePosition;

    private PlayerBall _playerBall;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => _map.IsReady);

        _playerBall = Instantiate(_prefabPlayerBall, _firePosition.position, Quaternion.identity, _firePosition);
    }
}


