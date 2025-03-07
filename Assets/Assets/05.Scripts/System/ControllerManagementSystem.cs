using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManagementSystem : MonoBehaviour
{
    [SerializeField] TileMatchingController tileMatchingController;
    [SerializeField] TileGenerateController tileGenerateController;
    [SerializeField] TileManagementController tileManagementController;
    [SerializeField] TileShuffleController tileShuffleController;
    [SerializeField] MouseInputContoller mouseInputContoller;
    [SerializeField] AudioController audioController;
    [SerializeField] TimerController timerController;

    public TileMatchingController TileMatchingContoller => tileMatchingController;
    public TileGenerateController TileGenerateContoller => tileGenerateController;
    public TileManagementController TileManagementController => tileManagementController;
    public TileShuffleController TileShuffleController => tileShuffleController;
    public MouseInputContoller MouseInputContoller => mouseInputContoller;
    public AudioController AudioController => audioController;
    public TimerController TimerController => timerController;
}
