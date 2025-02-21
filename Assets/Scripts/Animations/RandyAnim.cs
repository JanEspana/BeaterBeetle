using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandyAnim : AnimGeneric
{
    public AttackBehaviourMelee abm;
    public override void SpecificAnim()
    {
        if (currentState.GetType() == typeof(AttackSO) && abm.attackCooldown <= 0.1f)
        {
            if (abm.actualPunch)
            {
                anim.SetTrigger("LeftPunch");
            }
            else
            {
                anim.SetTrigger("RightPunch");
            }
        }
    }
}
