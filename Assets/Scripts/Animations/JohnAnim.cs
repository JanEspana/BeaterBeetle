using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class John : MonoBehaviour
{
    public Animator anim;
    public Player player;
    public AttackManager am;
    bool isAttacking;
    bool punch;

    private void Update()
    {
        anim.SetBool("isWalking", isWalking());
        if (am.isAttacking)
        {
            if (am.special && am.specialCooldown <= 0)
            {
                anim.SetTrigger("Horn");
            }
            else if (!am.special && punch)
            {
                if (am.actualPunch)
                {
                    anim.SetTrigger("LeftPunch");
                }
                else
                {
                    anim.SetTrigger("RightPunch");
                }
                punch = false;
            }
        }
        else if (!am.special)
        {
            punch = true;
        }
    }
    bool isWalking()
    {
        if (player.gameObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            return true;
        }
        return false;
    }
}