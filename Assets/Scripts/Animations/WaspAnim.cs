using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspAnim : MonoBehaviour
{
    public Animator anim;
    public StatesSO currentState;
    public AttackBehaviourSting abs;
    bool isAttacking = false;
    void Update()
    {
        currentState = GetComponent<EnemyController>().currentState;
        if (currentState.GetType() == typeof(DieSO))
        {
            anim.SetTrigger("Die");
        }
        else
        {
            if (abs.attackCooldown >= 3)
            {
                isAttacking = true;
            }

            if (abs.attackCooldown <= 0.5f && isAttacking)
            {
                anim.SetTrigger("Attack");
                isAttacking = false;
            }

            if (abs.isGrounded)
            {
                anim.SetBool("isStuck", true);
            }
            else
            {
                anim.SetBool("isStuck", false);
            }
        }
    }
}
