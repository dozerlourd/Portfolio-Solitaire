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
    [SerializeField] AudioClip clickClip;

    private void Update()
    {
        if(GameManager.isGameEnd) return;

        if(Mouse.current.leftButton.wasPressedThisFrame && InputInfo.IsApplyMouseInput)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

            //Vector2Int tile1Vec2Int = TileInfo.WorldToGrid(controllerManagementSystem.TileMatchingContoller.board[0, 0].transform.position);
            //Vector2Int tile2Vec2Int = TileInfo.WorldToGrid(controllerManagementSystem.TileMatchingContoller.board[0, 1].transform.position);

            RaycastHit rayHit;
            if (Physics.Raycast(mouseWorldPos, Vector3.forward, out rayHit, 100))
            {
                rayHit.collider.TryGetComponent(out Tile tile);
                if (rayHit.collider.CompareTag("Tile") && tile != null)
                {
                    Vector2Int tileVec2Int = TileGenerationInfo.WorldToGrid(rayHit.point);
                    controllerManagementSystem.TileMatchingContoller.SetMatchingVec2Int(tileVec2Int);
                    tile.TileClickedInteraction();
                }
            }
        }
    }
}
