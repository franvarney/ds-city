using Object = System.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeActive : PooledObject {

    public int id;

    private const float VERTICAL_SPACING = 5.0f;
    private const float HORIZONTAL_SPACING = 7.5f;

    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material placeHolderMaterial;
    private MeshRenderer render;

    private void Awake() {
        render = GetComponent<MeshRenderer>();
    }

    private Vector3 GetSpacedPosition(Vector3 position, Vector3 direction) {
        return new Vector3(
                position.x + HORIZONTAL_SPACING * direction.x,
                position.y + VERTICAL_SPACING * direction.y,
                position.z + HORIZONTAL_SPACING * direction.z
            );
    }

    public CubeActive Spawn(Object space, Vector3 position, Vector3 direction) {
        transform.position = GetSpacedPosition(position, direction);
        transform.tag = "CubePlaceHolder";
        return this;
    }

    public void SetMaterial(string material) {
        switch(material) {
            case "active": render.material = activeMaterial; break;
            case "placeholder": render.material = placeHolderMaterial; break;
            default: render.material = activeMaterial; break;
        }
    }

    public void SetId(int id, Vector3 direction) {
        Debug.Log("direction");
        Debug.Log(id);
        Debug.Log(direction.x);
        Debug.Log(direction.y);
        Debug.Log(direction.z);
        id = (int)(id + Mathf.Max(direction.x, direction.z) + direction.y);
        Debug.Log("inside id");
        Debug.Log(id);
    }
}
