using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionHandler : MonoBehaviour
{
    [SerializeField] private string itemTag;
    [SerializeField] private ParticleSystem itemFlash;
    [SerializeField] [Tooltip("How many points is this item worth?")] private int points;

    private GameObject item;
    private LevelManager levelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
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
