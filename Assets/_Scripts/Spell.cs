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

    void Awake()
    {
        recoveryBooster = 1;
        recoverySlower = 1;
        level = 1;
        Disactivate();
    }

    void Update()
    {
        // Lower timeLeft while active
        if (isActive)
        {
            Debug.Log("time left:" + timeLeft);

            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Debug.Log(timeLeft);
                Disactivate();
            }
        }

        // Regain fuel(duration) when not active
        if (!isActive && (timeLeft < duration))
        {
            timeLeft += (Time.deltaTime / recoverySlower * recoveryBooster); // takes 2x as long to recover
            Debug.Log(timeLeft);
        }
    }

    public void ToggleActivatation()
    {
        if (!isActive)
        {
            Activate();
        }
        else
        {
            Disactivate();
        }
    }

    public virtual void Activate()
    {
        Debug.Log("Activate!");
        isActive = true;
        GetComponent<Renderer>().enabled = true;
        //this.gameObject.SetActive(true);
    }

    public virtual void Disactivate()
    {
        Debug.Log("Disactivate!");
        isActive = false;
        GetComponent<Renderer>().enabled = false;
        //this.gameObject.SetActive(false);
    }

}
