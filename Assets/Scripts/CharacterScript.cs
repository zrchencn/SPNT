using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Insert Character Controller")]
    private CharacterController controller;
    
    [SerializeField]
    [Tooltip("Insert Animator Controller")]
    private Animator playerAnimator;

    public static event Action onPlayerDeath;
    
    private Vector3 velocity;
    private bool grounded;
    private bool hit;
    private float gravity = -17f;
    private float groundCastDist = 1.5f;
    public float forwardRunSpeed = 8f;
    public float sidestepSpeed = 50f;
    public float jumpHeight = 90f;

    private int health = 3;
    private Rigidbody rigidbody;
    private LevelManager levelManager;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioClip music_danger;
    [SerializeField] private AudioClip clip_jump;
    [SerializeField] private AudioClip clip_hurt;
    [SerializeField] private AudioClip clip_die;
    private AudioSource source;
    PostProcessVolume vol;
    private Vignette vig;
    private float vig_timer = 0f;


    
    // Start is called before the first frame update
    void Start()
    {
        vig = ScriptableObject.CreateInstance<Vignette>();
        vig.enabled.Override(true);
        vig.intensity.Override(0f);
        vig.color.Override(Color.red);
        vol = PostProcessManager.instance.QuickVolume(6, 100f, vig);
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Grounded
        Transform playerTransform = transform;
        grounded = Physics.Raycast(playerTransform.position, Vector3.down, groundCastDist);
        
        // Ground Movement
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 movement = (playerTransform.right * x) + (playerTransform.forward * 1);
        controller.Move(movement * (forwardRunSpeed * Time.deltaTime));

        // Jumping
        velocity.y += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump") && grounded)
        {
            source.clip = clip_jump;
            source.volume = 0.9f;
            source.Play();
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
        if (health <= 0 || playerTransform.position.y <= -20)
        {
            vig_timer += 0.01f;
            vig.intensity.Interp(0.4f, 1, vig_timer);
            if (music.pitch > 0)
                music.pitch -= 0.01f;
            if (!levelManager.isGameOver())
            {
                source.clip = clip_die;
                source.volume = 0.5f;
                source.Play();
               levelManager.endGame();
            }
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Fatal"))
        {
            source.clip = clip_hurt;
            source.volume = 0.5f;
            source.Play();
            health--;
            if (health == 1)
            {
                vig.intensity.Override(0.4f);
                music.Stop();
                music.volume = 1.0f;
                music.clip = music_danger;
                music.Play();
            }
        }
    }
}
