using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform spine;
    private float spineAngleY;
    private float spineAngleX;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spineAngleY += Input.GetAxis("Mouse Y") * 500 * -Time.deltaTime;
        spineAngleY = Mathf.Clamp(spineAngleY, -30, 30);
        spineAngleX += Input.GetAxis("Mouse X") * 500 * -Time.deltaTime;
        spineAngleX = Mathf.Clamp(spineAngleX, -30, 30);
        spine.localRotation = Quaternion.Euler(new Vector3(spineAngleY, spineAngleX, 0));
    }
}
