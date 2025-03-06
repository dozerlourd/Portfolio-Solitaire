using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] Slider timerSlider;
    [SerializeField] float timeLimit;
    float timeElapse = 0;

    public bool CheckHasTimeLimit() => timeElapse > timeLimit;

    public void TimerOneTick(float deltaTime)
    {
        if (timerSlider != null && !CheckHasTimeLimit())
        {
            timeElapse += deltaTime;
            timerSlider.value = (1 - (float)(timeElapse / timeLimit));
        }
    }

    public void TimerOneTick()
    {
        if (timerSlider != null && !CheckHasTimeLimit())
        {
            timeElapse += Time.deltaTime;
            timerSlider.value = (1 - (float)(timeElapse / timeLimit));
        }
    }
}
