using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourAnt : AttackBehaviourGeneric
{
    public EnemyController enemyController;
    private void Awake()
    {
        enemyController = gameObject.GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public override void Attack()
    {
        audioManager.PlaySFX(audioManager.antBite);
        if (player.HP > 0)
        {
            if (!enemyController.foodIsAlive && !player.isBlocking)
            {
                player.TakeDamage(1);

            }
            else if (enemyController.foodIsAlive)
            {
                enemyController.target.GetComponent<FoodScript>().TakeDamage(1);
            }
            enemyController.stun = 1;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            enemyController.knockback = 4;
            enemyController.GoToState<KnockbackSO>();
        }
        else
        {
            gameObject.GetComponent<EnemyController>().GoToState<IdleSO>();
        }
    }
}
