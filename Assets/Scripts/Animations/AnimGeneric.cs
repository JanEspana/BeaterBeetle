using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimGeneric : MonoBehaviour
{
    public Animator anim;
    public StatesSO currentState;

    void Awake()
    {
        currentState = GetComponent<EnemyController>().currentState;
    }
    private void Update()
    {
        currentState = GetComponent<EnemyController>().currentState;
        if (currentState.GetType() == typeof(DieSO))
        {
            anim.SetTrigger("Die");
        }
        else
        {
            if (currentState.GetType() == typeof(ChaseSO))
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                SpecificAnim();
            }
        }
    }
    public abstract void SpecificAnim();
}
