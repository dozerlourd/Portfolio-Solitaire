using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewAnimatedTile", menuName = "Tiles/AnimatedTile")]
public class Tile : MonoBehaviour
{
    [SerializeField] Sprite tileSprite;


    public void OnTileClicked(Vector3Int position)
    {
        
    }
}
