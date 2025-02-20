using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "States/AttackSO")]
public class AttackSO : StatesSO
{
    public override void OnStateEnter(EnemyController ec)
    {
        Debug.Log(">:(");
        if (ec.gameObject.GetComponent<AttackBehaviourSlash>() != null)
        {
            AttackBehaviourSlash abs = ec.gameObject.GetComponent<AttackBehaviourSlash>();
            abs.lowCut.SetActive(true);
            abs.crossCut.SetActive(true);
        }
        //fix the position of the enemy
        Rigidbody rb = ec.gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public override void OnStateExit(EnemyController ec)
    {
        if (ec.gameObject.GetComponent<AttackBehaviourSlash>() != null)
        {
            AttackBehaviourSlash abs = ec.gameObject.GetComponent<AttackBehaviourSlash>();
            abs.lowCut.SetActive(false);
            abs.crossCut.SetActive(false);
        }
        //unfix the position of the enemy
        Rigidbody rb = ec.gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
    }
    public override void OnStateUpdate(EnemyController ec)
    {
        ec.attack.Attack();
    }
}
