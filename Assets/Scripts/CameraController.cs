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
    // Use this for initialization

	void Start () {
        if(!useOffsetValues)
            offset = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = target.position - offset;
        transform.LookAt(target);
	}
}
