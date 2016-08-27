using UnityEngine;
using System.Collections;

public class Flamethrower : Spell
{
    void Start()
    {
        level = 1;
        power = 10;
        duration = 5;
        timeLeft = duration;
        attackType = AttackType.Fire;
        isActive = false;
        recoverySlower = 2;
        recoveryBooster = 1;
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

    public void Activate()
    {
        Debug.Log("Activate!");
        isActive = true;
        GetComponent<Renderer>().enabled = true;
        //this.gameObject.SetActive(true);
    }

    public void Disactivate()
    {
        Debug.Log("Disactivate!");
        isActive = false;
        GetComponent<Renderer>().enabled = false;
        //this.gameObject.SetActive(false);
    }


}
