using System.Collections;
using UnityEngine;

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

    private Vector3 velocity;
    private bool grounded;
    private bool hit;
    private float gravity = -17f;
    private float groundCastDist = 1.5f;
    public float forwardRunSpeed = 8f;
    public float sidestepSpeed = 50f;
    public float jumpHeight = 90f;
    private float collisionTime = 2f;
    
    private float health = 100f;
    private Rigidbody rigidbody;
    private LevelManager levelManager;
    private bool touchedElectricity = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
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
        // Starting position is lower to detect low obstacles
        Vector3 lowCharacter = new Vector3(playerTransform.position.x, playerTransform.position.y - 1f,
            playerTransform.position.z);
        hit = Physics.Raycast(lowCharacter, Vector3.forward, 1.5f);

        if (hit)
        {
            collisionTime -= Time.deltaTime;
        }
        else
        {
            collisionTime = 2f;
        }

        // Health and Death
        if (touchedElectricity)
        {
            StartCoroutine(DeathByElectric());
        }
        else if (health <= 0 || playerTransform.position.y <= -20 || collisionTime <= 0)
        {
            if (!levelManager.isGameOver())
            {
                levelManager.endGame();
            }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private IEnumerator DeathByElectric()
    {
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        
        yield return new WaitForSeconds(2f);

        if (!levelManager.isGameOver())
        {
            levelManager.endGame();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Fatal"))
        {
            touchedElectricity = true;
        }
    }
}
