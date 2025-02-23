using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    [SerializeField] Texture2D tileImage;
    [SerializeField] string tileName;
    bool isClickable = true;

    [HideInInspector] public string TileName => tileName;
    AudioController audioController;

    Material mat;

    Tweener twPunchScale;
    Tweener twCorrectAnim;
    Tweener twWrongAnim;

    bool wasPlayedEndAnimation = false;

    private void Awake() => mat = GetComponent<MeshRenderer>().material;

    private void Start()
    {
        ClickAnimation();
        isClickable = true;
        SetTileTexture();
    }

    private void Update()
    {
        if (GameManager.isGameEnd && !wasPlayedEndAnimation)
        {
            CorrectAnimation();
            wasPlayedEndAnimation = true;
        }
    }

    public void TileClickedInteraction()
    {
        if (GameManager.isGameEnd) return;

        if (isClickable)
        {
            StartCoroutine(CheckClickable());
            audioController.PlayClickSound();
            ClickAnimation();
        }
    }

    void ClickAnimation()
    {
        twPunchScale = transform.DOPunchScale(-Vector3.one * 0.7f, 0.5f, 6, 0.3f);
    }

    public void CorrectBoard()
    {
        TileManagementController.boardCount--;
    }

    public void CorrectAnimation()
    {
        twCorrectAnim = transform.DOScale(0, 0.3f).SetEase(Ease.InBack);
    }

    public void WrongAnimation()
    {
        twWrongAnim = transform.DOPunchRotation(Vector3.forward * 20, 0.6f, 5, 1);
    }

    private IEnumerator CheckClickable()
    {
        isClickable = false;
        yield return new WaitForSeconds(1);
        isClickable = true;
        mat.SetFloat("_IsTileClicked", 1);
    }

    public void ResetTileClicked()
    {
        mat.SetFloat("_IsTileClicked", 0);
    }

    public void SetAudioController(AudioController _audioController) => audioController = _audioController;

    void SetTileTexture() => mat.SetTexture("_MainTex", tileImage);
}
