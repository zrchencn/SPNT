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
    private float gravity = -9.8f;
    private float groundCastDist = 0.5f;
    public float forwardRunSpeed = 50f;
    public float sidestepSpeed = 30f;
    public float jumpHeight = 30f;
    public float health = 100f;
    
    
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
        if (Input.GetButton("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        // Crouching
        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            controller.height = 2.75f;
        }
        else
        {
            controller.height = 5.5f;
        }
        
        // Object Collision
        hit = Physics.Raycast(playerTransform.position, Vector3.forward, 0.1f);
        
        // Health and Death
        if (health <= 0 || playerTransform.position.y <= -20)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
