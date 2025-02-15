using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManagementSystem : MonoBehaviour
{
    [SerializeField] TileMatchingContoller tileMatchingContoller;
    [SerializeField] TileGenerateContoller tileGenerateContoller;
    [SerializeField] MouseInputContoller mouseInputContoller;

    public TileMatchingContoller TileMatchingContoller => tileMatchingContoller;
    public TileGenerateContoller TileGenerateContoller => tileGenerateContoller;
    public MouseInputContoller MouseInputContoller => mouseInputContoller;
}
