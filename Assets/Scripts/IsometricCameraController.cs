using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{
    [SerializeField]
    private Camera c;
    [SerializeField] 
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(30, 315, 0);
        c.orthographic = true;
        c.orthographicSize = 10;
        c.nearClipPlane = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z-10);
    }
}
