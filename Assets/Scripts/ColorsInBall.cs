using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ColorsInBall", menuName = "ScriptableObjects/ColorsInBall", order = 1)]
public class ColorsInBall : ScriptableObject
{
    [SerializeField] private ColorInBall[] _colors;

    public Color GetColor(BallColor ballColor)
    {
        foreach (var item in _colors)
        {
            if (item.BallColor == ballColor)
            {
                return item.Color;
            }
        }

        Debug.LogError("Color: " + ballColor + "not found");
        return Color.clear;
    }


    [System.Serializable]
    private struct ColorInBall
    {
        public BallColor BallColor;
        public Color Color;
    }
}