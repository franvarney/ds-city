using System;
using UnityEngine;

public class PooledObject : MonoBehaviour {

    [NonSerialized] PoolManager poolInstanceForPrefab;

    public PoolManager Pool { get; set; }

    public T GetPooledInstance<T> () where T : PooledObject {
		if (!poolInstanceForPrefab) {
			poolInstanceForPrefab = PoolManager.GetPool(this);
		}
		return (T)poolInstanceForPrefab.GetObject();
	}

	public void ReturnToPool () {
		if (Pool) 
			Pool.AddObject(this);
		else
			Destroy(gameObject);
	}
}