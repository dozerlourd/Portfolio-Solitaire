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
    [SerializeField] ControllerManagementSystem controllerManagementSystem;

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2Int tile1Vec2Int = TileInfo.WorldToGrid(controllerManagementSystem.TileMatchingContoller.board[0, 0].transform.position);
            Vector2Int tile2Vec2Int = TileInfo.WorldToGrid(controllerManagementSystem.TileMatchingContoller.board[0, 1].transform.position);

            print(tile1Vec2Int);
            print(tile2Vec2Int);

            controllerManagementSystem.TileMatchingContoller.CanConnect(tile1Vec2Int, tile2Vec2Int);

            //RaycastHit rayHit;
            //if(Physics.Raycast(mouseWorldPos, Vector3.forward, out rayHit, 100))
            //{
            //    rayHit.collider.TryGetComponent(out Tile tile);
            //    if (rayHit.collider.CompareTag("Tile") && tile != null)
            //    {
            //        tile.TileClickedInteraction();
            //    }
            //}
        }
    }
}
