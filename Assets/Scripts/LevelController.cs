using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] chunks;
    [SerializeField] 
    private Transform player_transform;
    private Queue<GameObject> active_chunks = new Queue<GameObject>();
    private const float chunk_width = 20f;
    private float chunk_zpos;
    private float boundary_zpos;
    
    // Start is called before the first frame update
    void Start()
    {
        chunk_zpos = 0f;
        boundary_zpos = 30f;
        active_chunks.Enqueue(Instantiate(chunks[0],
            new Vector3(0, 0, chunk_zpos), Quaternion.identity));
        chunk_zpos += chunk_width;
        active_chunks.Enqueue(Instantiate(chunks[0],
            new Vector3(0, 0, chunk_zpos), Quaternion.identity));
        chunk_zpos += chunk_width;

        for (int i = 0; i < 3; i++)
        {
            active_chunks.Enqueue(Instantiate(chunks[Random.Range(1, chunks.Length)],
                new Vector3(0, 0, chunk_zpos), Quaternion.identity));
            chunk_zpos += chunk_width;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player_transform.position.z > boundary_zpos)
        {
            Destroy(active_chunks.Dequeue());
            active_chunks.Enqueue(Instantiate(chunks[Random.Range(1, chunks.Length)],
                new Vector3(0, 0, chunk_zpos), Quaternion.identity));
            chunk_zpos += chunk_width;
            boundary_zpos += chunk_width;
        }
    }
}
