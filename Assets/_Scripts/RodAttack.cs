using UnityEngine;
using System.Collections;

public class RodAttack : Spell
{
    void Start()
    {
        level = 1;
        power = 5;
        duration = 0.25f;
        timeLeft = duration;
        attackType = AttackType.Fire;
        isActive = false;
        recoverySlower = 1;
        recoveryBooster = 2;
    }


}
