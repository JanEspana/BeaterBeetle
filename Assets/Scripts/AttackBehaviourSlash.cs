using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourSlash : AttackBehaviourGeneric
{
    public GameObject lowCut, crossCut, warningSphere;
    public bool actualCut, isAttacking;
    public override void Attack()
    {
        if (attackCooldown <= 0)
        {
            if (actualCut && player.HP > 0)
            {
                Slash(lowCut, lowCut.GetComponent<BoxCollider>());
                Debug.Log("LowCut");
            }
            else if (!actualCut && player.HP > 0)
            {
                Slash(crossCut, crossCut.GetComponent<BoxCollider>());
                Debug.Log("CrossCut");
            }
            else if (player.HP <= 0)
            {
                gameObject.GetComponent<EnemyController>().GoToState<IdleSO>();
            }
            attackCooldown = 3;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
    void Slash(GameObject cut, BoxCollider collider)
    {
        if (!isAttacking)
        {
            
            isAttacking = true;
            collider.enabled = true;
            cut.transform.position += cut.transform.forward * 0.75f;
            StartCoroutine(ResetCut(cut, collider));
        }
    }
    IEnumerator ResetCut(GameObject cut, BoxCollider collider)
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = false;
        cut.transform.position -= cut.transform.forward * 0.75f;
        isAttacking = false;
        actualCut = Random.Range(0, 2) == 0;
    }
}
