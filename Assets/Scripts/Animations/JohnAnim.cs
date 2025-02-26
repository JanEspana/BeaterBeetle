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
    bool punch, horn, dash;

    private void Update()
    {
        if (player.HP > 0)
        {
            anim.SetBool("isWalking", isWalking());
            if (am.isAttacking)
            {
                if (am.special && horn)
                {
                    anim.SetTrigger("Horn");
                    horn = false;
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
            else if (am.special)
            {
                horn = true;
            }

            
        }
        else
        {
            anim.SetTrigger("Die");
        }

        if (isWalking())
        {
            if (player.gameObject.GetComponent<Movement>().dashCooldown >= 1 && !dash)
            {
                anim.SetTrigger("Dash");
                dash = true;
            }
            else
            {
                dash = false;
            }
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