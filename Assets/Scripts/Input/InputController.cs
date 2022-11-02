using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    [SerializeField] private InputPanel _inputPanel;

    [HideInInspector] public UnityEvent<Vector3> OnFire;

    
    private void OnEnable()
    {
        _inputPanel.OnUp.AddListener(Fire);
    }

    private void Fire(Vector3 point)
    {
        OnFire?.Invoke(point);
    }

    private void OnDisable()
    {
        _inputPanel.OnUp.RemoveListener(Fire);
    }
}
