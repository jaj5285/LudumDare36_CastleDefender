using UnityEngine;
using System.Collections;

public class Flamethrower : Attack
{
    public static Flamethrower _instance;

    void Start()
    {
        _instance = this;
        level = 0;
        upgradeCost = 50;
        power = 2;
        duration = 5;
        timeLeft = duration;
        attackType = AttackType.Fire;
        isContinuousAttack = true;
        isActive = false;
        recoverySlower = 2;
        recoveryBooster = 1;
    }

    public void Upgrade(int level)
    {
        switch (level)
        {
            case 1:
                level = 1;
                upgradeCost = 100;
                power = 2;
                duration = 5;
                timeLeft = duration;
                recoverySlower = 2;
                recoveryBooster = 1;
                break;
            case 2:
                level = 2;
                upgradeCost = 250;
                power = 3;
                duration = 8;
                timeLeft = duration;
                recoverySlower = 2;
                recoveryBooster = 1.5f;
                break;
            default:
                Debug.Log("No upgrade level selected!");
                break;
        }
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
