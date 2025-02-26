using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornAttack : AttackGeneric
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == rivalTag)
        {
            if (!(rivalTag == "Enemy" && other.gameObject.GetComponent<EnemyController>().isFlyingEnemy))
            {
                other.gameObject.GetComponent<Character>().hasKnockback = true;
                audioManager.PlaySFX(audioManager.beetleHornHit);
            }
            other.gameObject.GetComponent<Character>().TakeDamage(dmg);
        }
    }
}