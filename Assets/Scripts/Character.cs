using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    public float HP;
    public bool hasKnockback;
    public bool isInvincible = false;
    public GameObject slider;
    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            HP -= dmg;
            if ((gameObject.GetComponent<EnemyController>() != null && !gameObject.GetComponent<EnemyController>().isAnt) || gameObject.GetComponent<Player>() != null)
            {
                slider.GetComponent<Slider>().value = HP / 10;
            }
            CheckIfAlive(hasKnockback);
        }
    }
    public abstract void CheckIfAlive(bool hasKnockback);

    public abstract void Die();
}
