using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManagementSystem : MonoBehaviour
{
    [SerializeField] TileMatchingController tileMatchingController;
    [SerializeField] TileGenerateController tileGenerateController;
    [SerializeField] TileShuffleController tileShuffleController;
    [SerializeField] MouseInputContoller mouseInputContoller;
    [SerializeField] AudioController audioController;

    public TileMatchingController TileMatchingContoller => tileMatchingController;
    public TileGenerateController TileGenerateContoller => tileGenerateController;
    public TileShuffleController TileShuffleController => tileShuffleController;
    public MouseInputContoller MouseInputContoller => mouseInputContoller;
    public AudioController AudioController => audioController;
}
