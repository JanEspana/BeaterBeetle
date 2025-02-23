using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourMelee : AttackBehaviourGeneric
{
    public GameObject leftPunch, rightPunch;
    public bool actualPunch;
    bool isAttacking;
    public override void Attack()
    {
        if (attackCooldown <= 0)
        {
            if (actualPunch && player.HP > 0)
            {
                BasicAttack(leftPunch, leftPunch.GetComponent<BoxCollider>());
            }
            else if (!actualPunch && player.HP > 0)
            {
                BasicAttack(rightPunch, rightPunch.GetComponent<BoxCollider>());
            }
            else
            {
                gameObject.GetComponent<EnemyController>().GoToState<IdleSO>();
            }
            attackCooldown = 1f;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
    void BasicAttack(GameObject punch, BoxCollider collider)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            collider.enabled = true;
            punch.transform.position -= punch.transform.right * 0.5f;
            StartCoroutine(ResetPosition(punch, collider));
        }
    }
    IEnumerator ResetPosition(GameObject punch, BoxCollider collider)
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = false;
        punch.transform.position += punch.transform.right * 0.5f;
        isAttacking = false;
        actualPunch = !actualPunch;
    }
}
