using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPCController : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float stopDistance = 1f;

    [SerializeField]
    private NPCCombatController combatController;

    [SerializeField]
    private Animator characterAnimator;

    Path path;
    Vector2 movement;
    private float moveLimiter = 0.7f;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rigidbody2D;

    [SerializeField]
    bool followTarget;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rigidbody2D.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (combatController.IsMovmentLocked)
            return;

        if (!followTarget)
            return;

        float distanceToTarget = Vector2.Distance(rigidbody2D.position, target.position);

        if (distanceToTarget < stopDistance)
            return;

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        movement = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2D.position).normalized;

        float distance = Vector2.Distance(rigidbody2D.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        } 
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (movement.x != 0 && movement.y != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            movement *= moveLimiter;
        }

        rigidbody2D.velocity = movement * speed * Time.deltaTime;

        //Hand animation stuff
        characterAnimator.SetFloat("Horizontal", movement.x);
        characterAnimator.SetFloat("Vertical", movement.y);
        characterAnimator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.y < 0)
        {
            // Debug.Log("DOWN");
            characterAnimator.SetInteger("LastWalkDirection", 0);
        }

        if (movement.y > 0)
        {
            //Debug.Log("UP");
            characterAnimator.SetInteger("LastWalkDirection", 1);
        }

        if (movement.x < 0)
        {
            // Debug.Log("LEFT");
            characterAnimator.SetInteger("LastWalkDirection", 2);
        }

        if (movement.x > 0)
        {
            //Debug.Log("RIGHT");
            characterAnimator.SetInteger("LastWalkDirection", 3);
        }

        if (movement.magnitude > 0.0f)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    [YarnCommand("SetFollowTargetOn")]
    public void SetFollowTargetOn()
    {
        followTarget = true;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    [YarnCommand("SetFollowTargetOff")]
    public void SetFollowTargetOff()
    {
        followTarget = false;
        CancelInvoke();
    }
} 
