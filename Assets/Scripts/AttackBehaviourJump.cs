using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourJump : AttackBehaviourGeneric
{
    public Rigidbody rb;
    bool isGrounded = false;
    public bool isInRange = false;
    float jumpForceX, jumpForceY;
    Transform targetPosition;
    public override void Attack()
    {
        if (!isInRange)
        {
            if (attackCooldown <= 0 && player.HP > 0 && isGrounded)
            {
                targetPosition = player.transform;
                transform.LookAt(targetPosition);
                jumpForceX = Vector3.Distance(transform.position, targetPosition.position) * 50;
                jumpForceY = 350;
                rb.AddForce(transform.up * jumpForceY);
                rb.AddForce(transform.forward * jumpForceX);
                attackCooldown = 3;
                isGrounded = false;
            }
            else if (player.HP > 0 && attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (attackCooldown <= 0 && player.HP > 0 && isGrounded)
            {
                player.TakeDamage(1);
                attackCooldown = 1.6f;
            }
            else if (player.HP > 0 && attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrounded && collision.gameObject.tag == "Player")
        {
            player.TakeDamage(2);
        }
        isGrounded = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }
}