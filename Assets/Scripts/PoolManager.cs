using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    List<PooledObject> availableObjects = new List<PooledObject>();
    PooledObject prefab;

    protected static string GetObjectName(string name) {
        return name + "Pool";
    }

    public PooledObject GetObject() {
        int lastAvailableIndex = availableObjects.Count - 1;
        PooledObject obj;

        if (lastAvailableIndex >= 0) {
            obj = availableObjects[lastAvailableIndex];
            availableObjects.RemoveAt(lastAvailableIndex);
            obj.gameObject.SetActive(true);
        } else {
            obj = Instantiate<PooledObject>(prefab);
            obj.transform.SetParent(transform, false);
            obj.Pool = this;
        }

        return obj;
    }

    public void AddObject(PooledObject obj) {
        obj.gameObject.SetActive(false);
        availableObjects.Add(obj);
    }

    public static PoolManager GetPool(PooledObject prefab) {
        GameObject obj;
        PoolManager pool;
        string objName = GetObjectName(prefab.name);

        if (Application.isEditor) {
            obj = GameObject.Find(objName);

            if (obj) {
                pool = obj.GetComponent<PoolManager>();
                if (pool)
                    return pool;
            }
        }
        
        obj = new GameObject(objName);
        pool = obj.AddComponent<PoolManager>();
        pool.prefab = prefab;
        
        return pool;
    }
}
