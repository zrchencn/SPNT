using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Insert Character Controller")]
    private CharacterController controller;

    private Vector3 velocity;
    private bool grounded;
    private float gravity = -9.8f;
    private float groundCastDist = 0.5f;
    public float forwardRunSpeed = 50f;
    public float sidestepSpeed = 30f;
    public float jumpHeight = 30f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Grounded
        Transform playerTransform = transform;
        grounded = Physics.Raycast(playerTransform.position, Vector3.down, groundCastDist);

        // Ground Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = (playerTransform.right * x) + (playerTransform.forward * z);
        controller.Move(movement * (forwardRunSpeed * Time.deltaTime));
        // Joystick Ground Movement
        float xJ = Input.GetAxis("LeftJoystick Horizontal");
        float zJ = Input.GetAxis("LeftJoystick Vertical");
        Vector3 movementJ = (playerTransform.right * xJ) + (playerTransform.forward * zJ);
        controller.Move(movementJ * (forwardRunSpeed * Time.deltaTime));
        

        // Jumping
        if ((Input.GetButton("Jump")) && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        
    }
}
