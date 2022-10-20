using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Insert Character Controller")]
    private CharacterController controller;
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private bool grounded;
    [SerializeField]
    private float groundCastDist = 1.4f;
    [SerializeField]
    private float gravity = -20f;
    [SerializeField]
    private float forwardRunSpeed = 7f;
    [SerializeField]
    private float sidestepSpeed = 7f;
    [SerializeField]
    private float jumpHeight = 100f;

    private enum PlayerState
    {
        WALKING,
        JUMPING,
        SLIDING,
        DEAD
    }

    private PlayerState current_state = PlayerState.WALKING;
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
        velocity.y += gravity * Time.deltaTime;

        switch (current_state)
        {
            case PlayerState.WALKING:
                move();
                playerAnimator.SetBool("is_jumping", false);
                // Jumping
                if (Input.GetButtonDown("Jump") && grounded)
                {
                    current_state = PlayerState.JUMPING;
                    velocity.y = Mathf.Sqrt(jumpHeight);
                }
                break;
            case PlayerState.JUMPING:
                move();
                playerAnimator.SetBool("is_jumping", true);
                if (grounded)
                {
                    current_state = PlayerState.WALKING;
                }
                break;
            case PlayerState.SLIDING:
                break;
            case PlayerState.DEAD:
                playerAnimator.SetBool("is_jumping", false);
                playerAnimator.SetBool("is_dead", true);
                break;
            
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void move()
    {
        // Movement
        float x = Input.GetAxisRaw("Horizontal");
        velocity.z = forwardRunSpeed;
        velocity.x = x * sidestepSpeed;
    }
    void OnCollisionEnter(Collision collision)
    {            current_state = PlayerState.DEAD;

        if (collision.gameObject.CompareTag("harmful"))
        {
            current_state = PlayerState.DEAD;
        }
    }
}


