using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 15f;
    public float accelerateRate = 1.5f;
    readonly float gravity = 9.8f;
    CharacterController characterController;
    Vector3 Player_Move;
    bool isAccelerating;
    float originalSpeed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        originalSpeed = moveSpeed;
    }

    void Update()
    {
        PlayerMove();
    }


    void PlayerMove()
    {

        if (characterController.isGrounded)
        {
            Player_Move = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * moveSpeed;

            if (Input.GetAxis("Jump") == 1)
            {
                Player_Move.y += jumpSpeed;
            }

            if (!isAccelerating && Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = accelerateRate * originalSpeed;
                isAccelerating = true;
            }

            if (isAccelerating && Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed = originalSpeed;
                isAccelerating = false;
            }
        }


        Player_Move.y -= gravity * Time.deltaTime;
        characterController.Move(Player_Move * Time.deltaTime);
    }
}
