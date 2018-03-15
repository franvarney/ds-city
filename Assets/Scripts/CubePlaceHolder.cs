using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlaceHolder : PooledObject {

    public int id;

    public void Spawn(RaycastHit hit, float spacingMultiplier) {
        Vector3 parentPosition = hit.transform.position;
        Vector3 newPosition = new Vector3(parentPosition.x + spacingMultiplier * 1.5f, parentPosition.y, parentPosition.z);
        transform.position = newPosition;
    }
}
