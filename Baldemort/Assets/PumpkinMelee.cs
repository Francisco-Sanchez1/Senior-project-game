using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMelee : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float moveSpeed = 5f;
    private Rigidbody2D rigidbody;
    public Animator anim;


    public float sumRadius;
    public float circleRadius;
    public GameObject enemyPrefabSummon;
    public float attackDecisionInterval = 5f; // Interval for making attack decisions
    private float nextAttackDecisionTime;
    public float detectionRadius;
    public float Rechaseradius;
    // Start is called before the first frame update
    void Start()

    {
        currentState = EnemyState.idle;
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }




    void CheckDistance()
    {
        float distanceToPlayer = Vector2.Distance(target.position, transform.position);
        if (distanceToPlayer <= chaseRadius && distanceToPlayer > attackRadius && (distanceToPlayer > detectionRadius || distanceToPlayer < Rechaseradius))
        {
                if (currentState != EnemyState.stagger)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    ChangeAnim(temp - transform.position);
                    rigidbody.MovePosition(temp);
                    ChangeState(EnemyState.walk);
                    anim.SetBool("idle", false);
                    anim.SetBool("attack", false);
                    anim.SetBool("attackRange", false);
            }

        }
        else if (distanceToPlayer > chaseRadius)
        {
            ChangeState(EnemyState.idle);
            anim.SetBool("idle", true);
            anim.SetBool("attack", false);
            anim.SetBool("attackRange", false);
            rigidbody.velocity = Vector2.zero;
        }
        else if (distanceToPlayer < attackRadius)
        {
            ChangeState(EnemyState.attack);
            anim.SetBool("attack", true);
            anim.SetBool("attackRange", false);
            anim.SetBool("idle", true);

        }
        else if (distanceToPlayer <= detectionRadius && distanceToPlayer > Rechaseradius)
        {
            rigidbody.velocity = Vector2.zero;
            ChangeState(EnemyState.attackRange);
            anim.SetBool("attackRange", true);
            anim.SetBool("attack", false);
            anim.SetBool("idle", true);
        }
        else if (distanceToPlayer < Rechaseradius)
        {
            ChangeState(EnemyState.walk);
            anim.SetBool("idle", false);
            anim.SetBool("attack", false);
            anim.SetBool("attackRange", false);
        }

    }

    private void setAnimFloat(Vector2 setVec)
    {
        anim.SetFloat("moveX", setVec.x);
        anim.SetFloat("movey", setVec.y);
    }
    private void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                setAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                setAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                setAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                setAnimFloat(Vector2.down);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }


}
