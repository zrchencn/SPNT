using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionHandler : MonoBehaviour
{
    [SerializeField] private string itemTag;

    [SerializeField] private ParticleSystem itemFlash;

    private GameObject item;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            Instantiate(itemFlash, item.transform.position, Quaternion.identity);
            Destroy(item);
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
