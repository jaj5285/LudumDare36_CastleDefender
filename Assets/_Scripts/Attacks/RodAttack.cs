using UnityEngine;
using System.Collections;

public class RodAttack : Attack
{
    void Start()
    {
        level = 1;
        power = 2;
        duration = 0.25f;
        timeLeft = duration;
        attackType = AttackType.Fire;
        isContinuousAttack = false;
        isActive = false;
        recoverySlower = 1;
        recoveryBooster = 2;
    }


}
