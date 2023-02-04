using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_char : MonoBehaviour
{
    public float speed = 5;
    public float gravity = -5;

    float velocityY = 0;
   public float ziplama_hizi = 10;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        velocityY += gravity * Time.deltaTime;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;
        Debug.Log(input);
        Vector3 temp = input;//Vector3.zero;
        /*
        if (input.z == 1)
        {
            temp += transform.forward;
        }
        else if (input.z == -1)
        {
            temp += transform.forward * -1;
        }

        if (input.x == 1)
        {
            temp += transform.right;
        }
        else if (input.x == -1)
        {
            temp += transform.right * -1;
        }
        */
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocityY += ziplama_hizi ;
            }
        }
        Vector3 velocity = temp * speed;
        velocity.y = velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0;
        }
    }
}
