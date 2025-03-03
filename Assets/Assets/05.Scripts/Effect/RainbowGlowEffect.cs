using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowGlowEffect : MonoBehaviour
{
    Material material;

    float elapsedTime;
    float lifeTime = 0.5f;

    float deltaTime;

    [SerializeField] float originUVValue = 0.6f;
    [SerializeField] float originAlphaValue = 0.95f;

    private void Awake() => material = GetComponent<MeshRenderer>().material;

    private void Start() => gameObject.SetActive(false);

    private void OnEnable()
    {
        material.SetFloat("_UVValue", originUVValue);
        material.SetFloat("_AlphaValue", originAlphaValue);
    }

    private void Update()
    {
        if(elapsedTime < lifeTime)
        {
            deltaTime = Time.deltaTime;
            IncreaseUVValue(deltaTime);
            DecreaseAlphaValue(deltaTime);
            elapsedTime += deltaTime;
        }
        else
        {
            gameObject.SetActive(!gameObject.activeSelf);
            elapsedTime = 0;
        }
    }

    void IncreaseUVValue(float deltaTime)
    {
        material.SetFloat("_UVValue", material.GetFloat("_UVValue") + deltaTime);
    }

    void DecreaseAlphaValue(float deltaTime)
    {
        material.SetFloat("_AlphaValue", material.GetFloat("_AlphaValue") - deltaTime * 2);
    }
}
