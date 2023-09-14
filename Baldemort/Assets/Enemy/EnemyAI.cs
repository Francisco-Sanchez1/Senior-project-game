using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 100f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWayPoint = 0;
    bool reachEndPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();


        InvokeRepeating("UpdatePath", 0f, .5f);

        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, onPathComplete);
        }
        
    }

    // Update is called once per frame
    void onPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachEndPath= true;
            return;
        }
        else
        {
            reachEndPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }
    }




}
