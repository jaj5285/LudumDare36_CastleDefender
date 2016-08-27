using UnityEngine;
using System.Collections;

public class BendGrass : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_obsx", transform.position.x);
        Shader.SetGlobalFloat("_obsy", transform.position.z);
    }
}

