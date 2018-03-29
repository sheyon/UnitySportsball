using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float playerSpeed = .25f;
    public float step = .15f;                                       //Clamped to 1.0f; 0.0f = No movement; 1.0f = Instant movement

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.cyan, Mathf.Infinity);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (transform.position.x + moveHorizontal * playerSpeed, 0, transform.position.z + moveVertical * playerSpeed);
        Vector3 rotationalMovement = new Vector3(moveHorizontal, 0, moveVertical);

        transform.position = movement;

        if (rotationalMovement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationalMovement), step);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //TO KEEP THE PLAYER FROM ROTATING UNCONTROLLABLY ON COLLISION
        if (other.gameObject.CompareTag("Ball"))
        {
            rb.freezeRotation = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //RESET CONSTRAINTS
        if (other.gameObject.CompareTag("Ball"))
        {
            rb.constraints = RigidbodyConstraints.None |
                RigidbodyConstraints.FreezePositionY |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationZ;
        }
    }

}