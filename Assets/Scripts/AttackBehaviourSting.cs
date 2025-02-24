using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourSting : AttackBehaviourGeneric
{
    public Rigidbody rb;
    public bool isGrounded = false;
    Transform targetPosition;
    public override void Attack()
    {
        if (attackCooldown <= 0 && player.HP > 0)
        {
            targetPosition = player.transform;
            transform.LookAt(targetPosition);
            rb.AddForce(transform.forward * 1000);
            attackCooldown = 3;
        }
        else if (player.HP > 0 && attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }
    IEnumerator RecoverPosition()
    {
        yield return new WaitForSeconds(2);
        //move up
        isGrounded = false;
        rb.velocity = new Vector3(0, 2, 0);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !isGrounded)
        {
            player.TakeDamage(3);
        }
        isGrounded = true;
        rb.velocity = Vector3.zero;
        StartCoroutine(RecoverPosition());
    }
}
