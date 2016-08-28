using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TP_Status : MonoBehaviour {

    public static TP_Status _instance;
    public float health = 100;

    public bool hasFlamethrower = false;
    
    void Awake () {
        _instance = this;
	}
}
