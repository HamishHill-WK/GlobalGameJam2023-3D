using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerMovementThrd : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    [Header("Movement")]
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    [Header("Ground Check")]
    public Transform ground_check;
    public float ground_distance = 0.4f;
    public LayerMask ground_mask;



    
    Vector3 velocity;
    bool isGrounded;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Sprint"))
        {
            Debug.Log("aaaaa");
            speed = 18f;
        } else
        {
            speed = 12;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        isGrounded = Physics.CheckSphere(ground_check.position, ground_distance, ground_mask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0f;
        }

        Vector3 move = transform.right * horizontal + transform.forward * vertical; 
            

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

       
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
       

       
    
    }
  
}
