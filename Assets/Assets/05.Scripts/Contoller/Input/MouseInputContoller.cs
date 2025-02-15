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
    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

            mouseWorldPos += Vector3.forward * 5;
            //Instantiate(test, mouseWorldPos, Quaternion.identity);

            Debug.DrawRay(mouseWorldPos, Vector3.forward, Color.red, 100);

            RaycastHit rayHit;
            if(Physics.Raycast(mouseWorldPos, Vector3.forward, out rayHit, 100))
            {
                print(rayHit.collider.tag);

                rayHit.collider.TryGetComponent(out Tile tile);
                if (rayHit.collider.CompareTag("Tile") && tile != null)
                {
                    tile.TileClickedInteraction();
                }
            }
        }
    }
}
