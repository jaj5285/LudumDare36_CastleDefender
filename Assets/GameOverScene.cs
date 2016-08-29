using UnityEngine;
using System.Collections;

public class GameOverScene : MonoBehaviour {
    
	void Update () {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.X) || Input.GetButton("XboxX") || Input.GetButton("XboxA") || Input.GetButton("Jump"))
        {
            Application.LoadLevel("Map2");
        }
	}
}
