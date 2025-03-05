using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    protected Material material;

    protected float elapsedTime;

    protected float deltaTime;

    [SerializeField] protected float lifeTime = 0.5f;
    [SerializeField] protected float originUVValue = 0.9f;
    [SerializeField] protected float originAlphaValue = 0.95f;

    protected void Awake() => material = GetComponent<MeshRenderer>().material;

    protected void Start() => gameObject.SetActive(false);

    protected void OnEnable()
    {
        material.SetFloat("_UVValue", originUVValue);
        material.SetFloat("_AlphaValue", originAlphaValue);
    }
}
