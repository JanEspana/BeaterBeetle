using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisAnim : AnimGeneric
{
    public AttackBehaviourSlash abs;
    bool isMoving = false;
    public override void SpecificAnim()
    {
        if (abs.attackCooldown >= 3)
        {
            isMoving = false;
        }
        currentState = GetComponent<EnemyController>().currentState;
        if (currentState.GetType() == typeof(AttackSO) && abs.attackCooldown <= 0.5f && !isMoving)
        {
            if (abs.actualCut)
            {
                anim.SetTrigger("LowCut");
            }
            else
            {
                anim.SetTrigger("CrossCut");
            }
            Debug.Log("Attack");
            isMoving = true;
        }
    }
}