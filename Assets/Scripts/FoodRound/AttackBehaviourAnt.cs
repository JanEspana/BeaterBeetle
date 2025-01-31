using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourAnt : AttackBehaviourGeneric
{
    public EnemyController enemyController;
    private void Awake()
    {
        enemyController = gameObject.GetComponent<EnemyController>();
    }
    public override void Attack()
    {
        if (player.HP > 0)
        {
            if (!enemyController.foodIsAlive && !player.isBlocking)
            {
                player.GetComponent<Player>().TakeDamage(1);

            }
            else if (enemyController.foodIsAlive)
            {
                enemyController.target.GetComponent<FoodScript>().CheckIfAlive(false);
            }
            enemyController.stun = 1;
            enemyController.knockback = 4;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            enemyController.GoToState<KnockbackSO>();
        }
        else
        {
            gameObject.GetComponent<EnemyController>().GoToState<IdleSO>();
        }
    }
}
