using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : AttackGeneric
{
    public bool blockable;
    public Vector3 originalPosition;
    public void Awake()
    {
        originalPosition = transform.position;
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == rivalTag)
        {
            other.gameObject.GetComponent<Character>().hasKnockback = false;
            
            if (blockable)
            {
                if (rivalTag == "Player" && !other.gameObject.GetComponent<Player>().isBlocking)
                {
                    other.gameObject.GetComponent<Character>().TakeDamage(dmg);
                }
                else if (rivalTag == "Enemy")
                {
                    other.gameObject.GetComponent<Character>().TakeDamage(dmg);
                }
            }
            else
            {
                if (rivalTag == "Player" || rivalTag == "Enemy")
                {
                    other.gameObject.GetComponent<Character>().TakeDamage(dmg);
                }
            }
            Debug.Log("Punch");
        }
    }
}
