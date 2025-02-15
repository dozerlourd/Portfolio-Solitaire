using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    [SerializeField] string tileName;

    [HideInInspector] public string TileName => tileName;


    public void TileClickedInteraction()
    {

    }
}
