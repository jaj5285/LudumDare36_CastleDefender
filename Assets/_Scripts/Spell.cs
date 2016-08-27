using UnityEngine;
using System.Collections;

public enum AttackType
{
    Physical
    , Fire
    , Tree
    , Rock
}

public class Spell : MonoBehaviour
{
    public bool isActive;
    public int level;
    public float power;
    public float duration;
    public float timeLeft;
    public AttackType attackType;
    public float recoverySlower; // the higher this number, the slower the recovery
    public float recoveryBooster; 

    void Awake() {
        recoveryBooster = 1; 
        recoverySlower = 1;
        level = 1;
        isActive = false;
        GetComponent<Renderer>().enabled = false;
        //this.gameObject.SetActive(false);
    }
}
