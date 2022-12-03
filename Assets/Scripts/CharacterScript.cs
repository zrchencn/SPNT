using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Insert Character Controller")]
    private CharacterController controller;

    private Vector3 velocity;
    private bool grounded;
    private bool hit;
    private float gravity = -15f;
    private float groundCastDist = 1.5f;
    private float collisionTime = 2f;
    public float forwardRunSpeed = 7f;
    public float sidestepSpeed = 50f;
    public float jumpHeight = 90f;
    private float collisionTime = 2f;
    
    private float health = 100f;
    private Rigidbody rigidbody;
    private LevelManager levelManager;
    private bool touchedElectricity;
    public float health = 100f;

    private Rigidbody rigidbody;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        touchedElectricity = false;
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
            collisionTime = 2.0f;
        }

        // Health and Death
        if (touchedElectricity || collisionTime <= 0)
        {
            StartCoroutine(DeathByElectric());
            touchedElectricity = false;
        }
        else if (health <= 0 || playerTransform.position.y <= -20)
        {
            collisionTime -= Time.deltaTime;
        }

    private IEnumerator DeathByElectric()
    {
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        //gameObject.SetActive(false);
        //GetComponent<MeshRenderer>().enabled = false;
        transform.localScale = new Vector3(0, 0, 0);
        forwardRunSpeed = 0;
        levelManager.kill();
        
        yield return new WaitForSeconds(1.0f);

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