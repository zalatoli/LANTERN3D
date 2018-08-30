using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public CharacterController controller;
    public float moveSpeed;
    public float jumpForce;
    public float terminalVelocity;

    public Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;
    //Pivot is used to controller player's forward direction irrespective
    //of camera position
    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = -1f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
            if (moveDirection.y > terminalVelocity)
                moveDirection.y = terminalVelocity;
        }
        controller.Move(moveDirection * Time.deltaTime);

        
        //Move the player in different directions based on camera direction
        //If the player is trying to move in a direction rotate them to the 
        //pivot rotation
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        //Mathf.Abs is used to return a 1 or 0 depending on whether there is any input at all
        //where normally the Get Axis functions return a range of values including negatives
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));


    }
}
