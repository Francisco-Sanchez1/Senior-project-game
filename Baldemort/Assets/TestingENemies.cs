using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingENemies : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float moveSpeed = 5f;
    private Rigidbody2D rigidbody;
    public Animator anim;

    private Coroutine freezeCoroutine;
    public Color FrozeColor;
    public Color regularColor;
    public SpriteRenderer mySprite;
    public float frozenTimeFull = 4f;
    private bool isFrozen = false;

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


        if (poisoned == true)
        {
            mySprite.color = new Color(0.5f, 0f, 1f);
        }
        if (currentState == EnemyState.Freeze)
        {
            mySprite.color = FrozeColor;
        }
    }

    void CheckDistance()
    {
        float distanceToPlayer = Vector2.Distance(target.position, transform.position);
        if (currentState == EnemyState.Freeze && !isFrozen)
        {
            anim.SetBool("attack", false);
            anim.SetBool("idle", true);
            isFrozen = true;
            freezeCoroutine = StartCoroutine(IamFrozen());
            rigidbody.velocity = Vector2.zero;


        }
        else if (currentState == EnemyState.stagger)
        {
            mySprite.color = regularColor;
            if (freezeCoroutine != null)
            {
                StopCoroutine(freezeCoroutine);
                isFrozen = false;
                freezeCoroutine = null;
            }
        }

        else if (distanceToPlayer <= chaseRadius && distanceToPlayer > attackRadius && currentState != EnemyState.Freeze)
        {

            if (currentState != EnemyState.stagger && currentState != EnemyState.Freeze)
            {
                rigidbody.velocity = Vector2.zero;
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                rigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("idle", false);
                anim.SetBool("attack", false);
            }

        }
        else if (distanceToPlayer > chaseRadius)
        {
            ChangeState(EnemyState.idle);
            anim.SetBool("idle", true);
            rigidbody.velocity = Vector2.zero;
        }
        else if (distanceToPlayer < attackRadius && currentState != EnemyState.Freeze)
        {
            ChangeState(EnemyState.attack);
            anim.SetBool("attack", true);

        }

    }

    IEnumerator IamFrozen()
    {
        // Wait for the next frame to reset the flag
        yield return new WaitForSeconds(frozenTimeFull);
        anim.SetBool("idle", false);
        mySprite.color = regularColor;
        isFrozen = false;
        ChangeState(EnemyState.walk);
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
