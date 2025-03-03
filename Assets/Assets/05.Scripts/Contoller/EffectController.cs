using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingEffect
{
    public GameObject[] pool;
    int currentIndex = 0;

    public GameObject PopObject()
    {
        if (pool == null) return null;

        int index = currentIndex++ % (pool.Length - 1);

        for (int i = 0; i < pool.Length; i++)
        {
            if(pool[index].gameObject.activeInHierarchy == true)
            {
                index = currentIndex++ % (pool.Length - 1);
            }
            else { break; }
        }

        return pool[index];
    }
}

public class EffectController : MonoBehaviour
{
    [SerializeField] FolderSystem folderSystem;

    [SerializeField] GameObject rainbowGlowEffectPrefab;
    [SerializeField] int rainbowGlowEffectCount = 0;

    PoolingEffect rainbowGlowEffects;

    public PoolingEffect RainbowGlowEffects => rainbowGlowEffects;

    void Awake()
    {
        rainbowGlowEffects = InstantiatePool(rainbowGlowEffectPrefab, rainbowGlowEffectCount, folderSystem.EffectFolder);
    }

    PoolingEffect InstantiatePool(GameObject prefab, int num, Transform parentTr = null)
    {
        PoolingEffect tempPool = new PoolingEffect();
        tempPool.pool = new GameObject[num];

        for (int i = 0; i < tempPool.pool.Length; i++)
        {
            tempPool.pool[i] = Instantiate(prefab, Vector3.zero, Quaternion.identity, parentTr);
        }


        return tempPool;
    }
}
