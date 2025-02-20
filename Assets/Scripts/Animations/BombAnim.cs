using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnim : AnimGeneric
{
    public AttackBehaviourDistance abd;
    public override void SpecificAnim()
    {
        currentState = GetComponent<EnemyController>().currentState;
        if (currentState.GetType() == typeof(AttackSO) && abd.attackCooldown <= 0)
        {
            anim.SetTrigger("Attack");
        }
    }
}
