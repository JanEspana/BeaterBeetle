using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviourGeneric : MonoBehaviour
{
    public Player player;
    public float attackCooldown;
    public AudioManager audioManager;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Debug.Log(player);
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }
    public abstract void Attack();
}
