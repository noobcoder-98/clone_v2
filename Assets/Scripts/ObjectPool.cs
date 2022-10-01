using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<GameObject> spearPool;
    public GameObject objectToPool;
    public int poolSize;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        spearPool = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < poolSize; i++) {
            temp = Instantiate(objectToPool);
            temp.SetActive(false);
            spearPool.Add(temp);
        }
    }

    public GameObject GetSpear() {
        for (int i = 0; i < poolSize; i++) {
            if (!spearPool[i].activeInHierarchy) {
                return spearPool[i];
            }
        }
        return null;
    }

}
