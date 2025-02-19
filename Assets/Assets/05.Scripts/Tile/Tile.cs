using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    [SerializeField] string tileName;

    [HideInInspector] public string TileName => tileName;

    Tweener twPunchScale;
    Tweener twCorrectAnim;
    Tweener twWrongAnim;

    private void Start()
    {
        
    }

    public void TileClickedInteraction()
    {
        ClickAnimation();
    }

    void ClickAnimation()
    {
        twPunchScale = transform.DOPunchScale(-Vector3.one, 0.5f, 6, 0.3f).SetAutoKill(false);
    }

    public void CorrectAnimation()
    {
        twCorrectAnim = transform.DOScale(0, 0.3f).SetEase(Ease.InBack);
    }

    public void WrongAnimation()
    {
        twWrongAnim = transform.DOPunchRotation(Vector3.forward * 20, 0.6f, 5, 1);
    }
}
