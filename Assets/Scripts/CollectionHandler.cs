using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionHandler : MonoBehaviour
{
    [SerializeField] private string itemTag;
    [SerializeField] private ParticleSystem itemFlash;
    [SerializeField] [Tooltip("How many points is this item worth?")] private int points;
    [SerializeField] private AudioClip clip_collect;
    private AudioSource source;

    private GameObject item;
    private LevelManager levelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            source.clip = clip_collect;
            source.volume = 0.5f;
            source.Play();
            Instantiate(itemFlash, item.transform.position, Quaternion.identity);
            Destroy(item);
            levelManager.addToScore(points);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (item == null && col.gameObject.tag == itemTag)
        {
            item = col.gameObject;
        }
    }
}
