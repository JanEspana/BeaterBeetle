using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SprayDamage : AttackGeneric
{
    bool isAttacking = false;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(rivalTag))
        {
            if (!isAttacking)
            {
                dmg = 4;
                isAttacking = true;
                Debug.Log("Spray hit " + dmg);
                other.gameObject.GetComponent<Player>().TakeDamage(dmg);
            }
            else
            {
                dmg = 0;
            }
            StartCoroutine(Spray(other));
        }
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(3);
        isAttacking = false;
    }
    IEnumerator Spray(Collider other)
    {
        yield return new WaitForSeconds(0.5f);
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        other.gameObject.GetComponent<Player>().TakeDamage(dmg);
        StartCoroutine(ResetAttack());
    }
}
