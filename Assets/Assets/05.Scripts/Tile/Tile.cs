using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewAnimatedTile", menuName = "Tiles/AnimatedTile")]
public class Tile : TileBase
{
    [SerializeField] Sprite tileSprite;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = tileSprite;
        tileData.color = Color.white;
    }
}
