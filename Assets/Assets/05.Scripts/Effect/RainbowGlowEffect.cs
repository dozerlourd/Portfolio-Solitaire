using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowGlowEffect : Effect
{
    private void Update()
    {
        if(elapsedTime < lifeTime)
        {
            deltaTime = Time.deltaTime;
            IncreaseUVValue(deltaTime);
            DecreaseAlphaValue(deltaTime * 1.5f);
            elapsedTime += deltaTime;
        }
        else
        {
            gameObject.SetActive(!gameObject.activeSelf);
            elapsedTime = 0;
        }
    }

    void IncreaseUVValue(float speed)
    {
        material.SetFloat("_UVValue", material.GetFloat("_UVValue") + speed);
    }

    void DecreaseAlphaValue(float speed)
    {
        material.SetFloat("_AlphaValue", material.GetFloat("_AlphaValue") - speed);
    }
}
