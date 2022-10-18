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
    private float groundCastDist = 0.05f;
    public float forwardRunSpeed = 10f;
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
        
        // Debugging
        if (Physics.Raycast(playerTransform.position, Vector3.down, groundCastDist))
        {
            Debug.DrawRay(playerTransform.position, Vector3.down, Color.blue);
        }
        else
        {
            Debug.DrawRay(playerTransform.position, Vector3.down, Color.red);
        }

        // Ground Movement
        float x = Input.GetAxis("Horizontal");
        velocity.z = forwardRunSpeed * Time.deltaTime;
        velocity.x = x * sidestepSpeed * Time.deltaTime;

        // Jumping
        if (Input.GetButton("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
