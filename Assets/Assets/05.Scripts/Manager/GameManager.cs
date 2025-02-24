using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ControllerManagementSystem controllerManagementSystem;
    public static bool isGameEnd = false;
    TimerController timerController;

    [SerializeField] GameObject Image_GameOverText;
    [SerializeField] GameObject Image_LevelClearText;
    Coroutine Co_gameRoutine;

    [SerializeField] AudioClip levelClearClip;
    [SerializeField] AudioClip gameoverClip;

    private void Awake()
    {
        isGameEnd = false;
        Image_GameOverText.SetActive(false);
        Image_LevelClearText.SetActive(false);
        timerController = controllerManagementSystem.TimerController;
    }

    private void Start()
    {
        controllerManagementSystem.TileGenerateContoller.GenerateTiles();
        Co_gameRoutine = StartCoroutine(GameRoutine());
    }

    private IEnumerator GameRoutine()
    {
        while(!controllerManagementSystem.TimerController.CheckHasTimeLimit() && TileManagementController.boardCount != 0)
        {
            float deltaTime = Time.deltaTime;
            controllerManagementSystem.TimerController.TimerOneTick(deltaTime);
            yield return new WaitForSeconds(deltaTime);
        }

        yield return new WaitForSeconds(0.8f);

        if (TileManagementController.boardCount > 0) //Inspect the board for tiles. Break and return false if one exists
        {
            Image_GameOverText.SetActive(true);
            controllerManagementSystem.AudioController.PlayVFXSound(gameoverClip);
        }
        else
        {
            Image_LevelClearText.SetActive(true);
            controllerManagementSystem.AudioController.PlayVFXSound(levelClearClip);
        }

        StopCoroutine(Co_gameRoutine);
        isGameEnd = true;
    }
}
