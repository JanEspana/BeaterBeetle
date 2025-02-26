using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    public StatesSO currentState;
    public Chase chase;
    public AttackBehaviourGeneric attack;
    public GameObject target;
    public Material mat;
    public float stun;
    public int knockback = 5;
    public bool isDistance = true;
    public bool isAnt = false;
    public bool isFlyingEnemy;
    public bool foodIsAlive = false;
    public AudioClip deathClip;
    // Start is called before the first frame update

    public void Awake()
    {
        attack = GetComponent<AttackBehaviourGeneric>();
        if (isAnt && GameObject.FindGameObjectWithTag("Food") != null)
        {
            foodIsAlive = true;
            target = GameObject.FindGameObjectWithTag("Food");
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }
    void Start()
    {
        currentState.OnStateEnter(this);
    }
    private void FixedUpdate()
    {
        currentState.OnStateUpdate(this);
    }

    public void GoToState<T>() where T : StatesSO
    {
        if (currentState.states.Find(state => state is T))
        {
            currentState.OnStateExit(this);
            currentState = currentState.states.Find(state => state is T);
            currentState.OnStateEnter(this);

        }
    }
    public override void CheckIfAlive(bool hasKnockback)
    {
        if (!isFlyingEnemy)
        {
            if (HP <= 0)
            {
                Die();
            }
            else if (hasKnockback)
            {
                GoToState<KnockbackSO>();
            }
            else
            {
                GoToState<StunSO>();
            }
        }
        else
        {
            if (HP <= 0)
            {
                Die();
            }
        }
    }
    public override void Die()
    {
        if (!isAnt)
        {
            audioManager.PlaySFX(deathClip);
        }
        HP = 0;
        GoToState<DieSO>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !foodIsAlive)
        {
            GoToState<AttackSO>();
        }
        else if (other.gameObject.tag == "Food" && isAnt)
        {
            GoToState<AttackSO>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !foodIsAlive && !isFlyingEnemy)
        {
            if (attack.attackCooldown < 0 && other.gameObject.GetComponent<Movement>().isGrounded && !isDistance)
            {
                GoToState<ChaseSO>();
            }
            else
            {
                StartCoroutine(EndAttack());
            }
        }
        else if (other.gameObject.tag == "Food" && isAnt && foodIsAlive)
        {
            target = GameObject.FindGameObjectWithTag("Food");
            GoToState<ChaseSO>();
        }
    }
    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(attack.attackCooldown);
        GoToState<ChaseSO>();
    }
}