using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandyAnim : AnimGeneric
{
    public AttackBehaviourMelee abm;
    public override void SpecificAnim()
    {
        if (currentState.GetType() == typeof(AttackSO) && abm.attackCooldown <= 0.25f)
        {
            if (abm.actualPunch)
            {
                anim.SetTrigger("LeftPunch");
                Debug.Log("LeftPunch");
            }
            else
            {
                anim.SetTrigger("RightPunch");
                Debug.Log("RightPunch");
            }
        }
    }
}
