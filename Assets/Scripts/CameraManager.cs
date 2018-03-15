using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private Vector3 BOTTOM_POSITION = new Vector3(-30, -30, -30);
    private Quaternion BOTTOM_ROTATION = Quaternion.Euler(-30, 45, 0);
    private Vector3 TOP_POSITION = new Vector3(-30, 30, -30);
    private Quaternion TOP_ROTATION = Quaternion.Euler(30, 45, 0);

    [SerializeField] private float speed = 2.5f;
    private Vector3 mouseDownPosition;
    private Vector3 mouseUpPosition;

    void Awake() {
    }


    void Start () {
		
	}

    /*void LateUpdate() {
        if (Input.GetMouseButtonDown(0))
            mouseDownPosition = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
            mouseUpPosition = Input.mousePosition;

        if (MouseWasDragged()) {
            if (mouseDownPosition.y > mouseUpPosition.y)
                transform.LerpTransform(TOP_POSITION, TOP_ROTATION, speed * Time.deltaTime);
            else 
                transform.LerpTransform(BOTTOM_POSITION, BOTTOM_ROTATION, speed * Time.deltaTime);
        }
    }*/

    bool MouseWasDragged() {
        return mouseDownPosition.y != 0 && mouseUpPosition.y != 0;
    }
}
