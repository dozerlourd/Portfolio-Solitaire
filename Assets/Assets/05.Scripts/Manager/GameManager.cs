using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ControllerManagementSystem controllerManagementSystem;
    public static bool isGameEnd = false;
    TimerController timerController;

    private void Awake()
    {
        isGameEnd = false;
        timerController = controllerManagementSystem.TimerController;
    }

    private void Start()
    {
        controllerManagementSystem.TileGenerateContoller.GenerateTiles();
    }

    private void Update()
    {
        if(controllerManagementSystem.TimerController.CheckHasTimeLimit())
        {
            isGameEnd = true;
        }
        else
        {
            controllerManagementSystem.TimerController.TimerOneTick();
        }
    }
}
