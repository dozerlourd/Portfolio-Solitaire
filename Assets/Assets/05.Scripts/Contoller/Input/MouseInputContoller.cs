using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class MouseInputContoller : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();

            //RaycastHit hit;
            //if(Physics.Raycast(mousePos, Vector3.forward, out hit))
            //{
            //    if(hit.collider.TryGetComponent(out Tile tile))
            //    {
            //        if(tile != null)
            //        {
            //            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //            Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPos);

            //            tile.OnTileClicked(tilemap, tilePosition);
            //        }
            //    }
            //}
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPos);

            TileBase clickedTile = tilemap.GetTile(tilePosition);

            if (clickedTile is Tile tile)
            {
                tile.OnTileClicked(tilemap, tilePosition); // 타일 클릭 함수 호출
            }
        }
    }
}
