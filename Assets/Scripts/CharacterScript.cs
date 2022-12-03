using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Insert Character Controller")]
    private CharacterController controller;
    
    [SerializeField]
    [Tooltip("Insert Animator Controller")]
    private Animator playerAnimator;

    [SerializeField] [Tooltip("Insert Death Particle Explosion")]
    private ParticleSystem deathExplosion;

    public static event Action onPlayerDeath;
    
    private Vector3 velocity;
    private bool grounded;
    private bool hit;
    private float gravity = -17f;
    private float groundCastDist = 1.5f;
    public float forwardRunSpeed = 8f;
    public float sidestepSpeed = 50f;
    public float jumpHeight = 90f;
    
    private float health = 100f;
    private Rigidbody rigidbody;
    private LevelManager levelManager;
    private bool touchedElectricity;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        health = 100f;
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
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
        }
        controller.Move(velocity * Time.deltaTime);
        playerAnimator.SetBool("is_jumping", !grounded);

        
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
        // if (health <= 0 && touchedElectricity)
        // {
        //     Instantiate(deathExplosion, transform.position, Quaternion.identity);
        //     gameObject.SetActive(false);
        //     touchedElectricity = false;
        // }
        if (health <= 0 || playerTransform.position.y <= -20)
        {
            if (!levelManager.isGameOver())
            {
                levelManager.endGame();
            }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Fatal"))
        {
            health = 0;
            touchedElectricity = true;
        }
    }
}
