using UnityEngine;
using System.Collections;

public class Flamethrower : Attack
{
    void Start()
    {
        level = 1;
        power = 2;
        duration = 5;
        timeLeft = duration;
        attackType = AttackType.Fire;
        isContinuousAttack = true;
        isActive = false;
        recoverySlower = 2;
        recoveryBooster = 1;
    }

    public override void Activate()
    {
        if (timeLeft > 1)
        {
            isActive = true;
            GetComponent<Renderer>().enabled = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public override void Disactivate()
    {
        isActive = false;
        GetComponent<Renderer>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}
