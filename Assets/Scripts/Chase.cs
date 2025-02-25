using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public float speed;
    public float range;
    NavMeshAgent agent;
    Rigidbody rb;
    public AudioClip walkClip;
    public AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void ChaseTarget(Transform target, Transform self)
    {
        /*Vector3 targetPos = new Vector3(target.position.x, self.position.y, self.position.z);
        rb.MovePosition(Vector3.MoveTowards(self.position, targetPos, speed * Time.deltaTime));
        Vector3 direction = target.position - self.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotation.x = 0;
        self.rotation = rotation;
        rb.velocity = direction.normalized * speed;*/
        //usa el AI para perseguir al jugador
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(target.position);
        audioManager.PlaySFX(walkClip);
        //cambia la velocidad del agente
    }
    public void StopChase()
    {
        rb.velocity = Vector3.zero;
        agent.SetDestination(transform.position);
    }
}
