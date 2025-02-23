using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextImgAnimation : MonoBehaviour
{
    void OnEnable()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
    }
}
