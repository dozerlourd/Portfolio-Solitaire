using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    [HideInInspector] public string TileName => tileName;
    public Coroutine clickRoutine;

    [SerializeField] Texture2D tileImage;
    [SerializeField] string tileName;

    AudioController audioController;
    Material mat;

    Quaternion originQuater;
    Vector3 originScale;

    bool wasPlayedEndAnimation = false;
    bool isClickable = true;

    Tweener twPunchScale;
    Tweener twCorrectAnim;
    Tweener twWrongAnim;

    private void Awake() => mat = GetComponent<MeshRenderer>().material;

    private void Start()
    {
        originQuater = Quaternion.identity;
        originScale = transform.localScale;

        ClickAnimation(); //Spawn animation
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
            clickRoutine = StartCoroutine(CheckClickable());
            audioController.PlayClickSound();
            ClickAnimation();
        }
    }

    void ClickAnimation()
    {
        twPunchScale = transform.DOPunchScale(-transform.localScale * 0.7f, 0.5f, 6, 0.3f);
    }

    public void CorrectBoardCount()
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
        mat.SetFloat("_IsTileClicked", 1);
        yield return new WaitForSeconds(1);
        isClickable = true;

        ResetTransform();
    }

    public void ResetTileClicked() => mat.SetFloat("_IsTileClicked", 0);

    public void SetAudioController(AudioController _audioController) => audioController = _audioController;

    void SetTileTexture() => mat.SetTexture("_MainTex", tileImage);

    void ResetTransform()
    {
        twPunchScale.Pause();
        twCorrectAnim.Pause();
        twWrongAnim.Pause();

        transform.DORotateQuaternion(originQuater, 0.2f);
        transform.DOScale(originScale, 0.2f);
    }
}
