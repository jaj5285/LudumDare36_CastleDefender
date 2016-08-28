using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    public Camera cameraToLookAt;

    void Start()
    {
        if (Camera.main != null)
        {
            // Use existing main camera
            cameraToLookAt = Camera.main;
        }
    }

    void Update()
    {
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v);
        transform.Rotate(0, 180, 0);
    }
}
