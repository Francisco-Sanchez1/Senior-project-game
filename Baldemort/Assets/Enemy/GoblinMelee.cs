using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMelee : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float moveSpeed = 5f;
    private Rigidbody2D rigidbody;
    public Animator anim;
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
        if (Vector2.Distance(target.position, 
            transform.position) <= chaseRadius 
            && Vector2.Distance(target.position, 
            transform.position) > attackRadius)
        {
            if (currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                rigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("idle", false);
                //anim.SetBool("attack", false);
            }

        }
        else if (Vector2.Distance(target.position,
            transform.position) > chaseRadius)
        {
            ChangeState(EnemyState.idle);
            anim.SetBool("idle", true);
            rigidbody.velocity = Vector2.zero;
        }
        else if (Vector2.Distance(target.position,
            transform.position) < attackRadius)
        {
            ChangeState(EnemyState.attack);
            //anim.SetBool("attack", true);

        }
    }

    private void setAnimFloat(Vector2 setVec)
    {
        anim.SetFloat("moveX", setVec.x);
        anim.SetFloat("movey", setVec.y);
    }
    private void ChangeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                setAnimFloat(Vector2.right);
            }
            else if(direction.x < 0)
            {
                setAnimFloat(Vector2.left);
            }
        }
        else  if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
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
        if(currentState != newState)
        {
            currentState = newState; 
        }
    }


}
