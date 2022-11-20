using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Insert Character Controller")]
    private CharacterController controller;

    public static event Action onPlayerDeath;
    
    private Vector3 velocity;
    private bool grounded;
    private bool hit;
    private float gravity = -15f;
    private float groundCastDist = 1.5f;
    public float forwardRunSpeed = 7f;
    public float sidestepSpeed = 50f;
    public float jumpHeight = 70f;
    public float health = 100f;

    private Rigidbody rigidbody;
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        // Grounded
        Transform playerTransform = transform;
        grounded = Physics.Raycast(playerTransform.position, Vector3.down, groundCastDist);
        
        // Ground Movement
        float x = Input.GetAxis("Horizontal");
        Vector3 movement = (playerTransform.right * x) + (playerTransform.forward * 1);
        controller.Move(movement * (forwardRunSpeed * Time.deltaTime));

        // Jumping
        velocity.y += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight);
            //rigidbody.AddForce(playerTransform.up * jumpHeight, ForceMode.Impulse);
        }
        controller.Move(velocity * Time.deltaTime);

        
        // Crouching
        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            controller.height = 2.75f;
        }
        else
        {
            controller.height = 3.41f;
        }
        
        // Object Collision
        hit = Physics.Raycast(playerTransform.position, Vector3.forward, 0.1f);
        
        // Grounded is true if you are standing on top of an object
        if (Physics.Raycast(playerTransform.position, Vector3.forward, 0.1f))
        {
            grounded = true;
        }

        // Health and Death
        if (health <= 0 || playerTransform.position.y <= -20)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
