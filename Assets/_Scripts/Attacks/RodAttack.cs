using UnityEngine;
using System.Collections;

public class RodAttack : Attack
{
    void Start()
    {
        myName = "Rod";
        level = 1;
        power = 2;
        upgradeCost = 50;
        duration = 0.25f;
        timeLeft = duration;
        attackType = AttackType.Fire;
        isContinuousAttack = false;
        isActive = false;
        recoverySlower = 1;
        recoveryBooster = 2;
    }
    public override void Upgrade(int myLevel)
    {
        switch (myLevel)
        {
            case 2:
                level = 2;
                upgradeCost = 250;
                power = 10;
                recoverySlower = 2;
                recoveryBooster = 1.5f;
                break;
            default:
                Debug.Log("No upgrade level selected!");
                break;
        }
    }


}
