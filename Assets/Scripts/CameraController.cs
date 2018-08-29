using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //If false, the camera will be offset by its 
    //relative position to the target in the scene, rather
    //than the offset entered into the script itself
    public bool useOffsetValues;
    public Transform target;
    //How far the camera should be from the world
    public Vector3 offset;

    public float rotateSpeed;
    // Use this for initialization

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;

	void Start () {
        if(!useOffsetValues)
            offset = target.position - transform.position;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
    // LateUpdate is called at the end of each frame before the next
	void LateUpdate () {
        //Get x position of mouse & rotate the target object
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        //Get Y position of mouse and rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        //pivot.Rotate(-vertical, 0, 0);
        if (invertY)
            pivot.Rotate(vertical, 0, 0);
        else
            pivot.Rotate(-vertical, 0, 0);

        //Limit the up/down camera rotation
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //Move the camera based on the current rotation of the target 
        //and the original offset
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        if (transform.position.y < (target.position.y - 0.5f))
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);

        transform.LookAt(target);
	}
}
