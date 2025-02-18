using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    [SerializeField] string tileName;

    [HideInInspector] public string TileName => tileName;


    public void TileClickedInteraction()
    {
        ClickAnimation();
    }

    void ClickAnimation()
    {
        transform.DOPunchScale(-Vector3.one, 0.5f, 6, 0.3f);
    }
}
