using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasshopperAnim : MonoBehaviour
{
    public Animator anim;
    public StatesSO currentState;
    bool jumpCharged = false;
    bool kickCharged = false;

    public AttackBehaviourJump abj;

    private void Update()
    {
        currentState = GetComponent<EnemyController>().currentState;
        if (currentState.GetType() == typeof(DieSO))
        {
            anim.SetTrigger("Die");
        }
        else
        {
            if (abj.attackCooldown >= 3)
            {
                jumpCharged = true;
            }
            if (abj.attackCooldown <= 1.6f && abj.isInRange)
            {
                kickCharged = true;
            }
            currentState = GetComponent<EnemyController>().currentState;


            if (abj.attackCooldown <= 0.8f && abj.isInRange && kickCharged)
            {
                anim.SetTrigger("Kick");
                kickCharged = false;
            }
            else if (abj.attackCooldown <= 1.5 && !abj.isInRange && jumpCharged)
            {
                anim.SetTrigger("Jump");
                jumpCharged = false;
            }
        }
    }
}
