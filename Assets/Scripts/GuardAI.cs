using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : MonoBehaviour
{
    private Transform playerTransform;
    private bool chasing = false;
    private bool waiting = false;
    private Vector2 direction;
    private float walkSpeed = 5f;
    private int currentTarget;
    private Transform[] waypoints = new Transform[2]; // Array to hold waypoints

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;

        // Find and assign waypoints
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = GameObject.Find("p" + (i + 1)).transform;
        }
    }

    private void Update()
    {
        if (chasing)
        {
            direction = playerTransform.position - transform.position;
        }

        if (!waiting)
        {
            // Move towards current waypoint
            direction = waypoints[currentTarget].position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * walkSpeed, Space.World);

            // Check if reached waypoint
            if (Vector2.Distance(transform.position, waypoints[currentTarget].position) < 0.1f)
            {
                SetNextPoint();
        
            }
        }
    }


    private IEnumerator WaitAtPoint()
    {
        waiting = true;
        yield return new WaitForSeconds(2);
        rotateGuard();
        waiting = false;
    }
    // Add a pause at each point before moving to the next
    public void SetNextPoint()
    {
        StartCoroutine(WaitAtPoint());
        
        // Pick a random waypoint different from the current one
        int nextPoint;
        do
        {
            nextPoint = Random.Range(0, waypoints.Length);
        } while (nextPoint == currentTarget);

        currentTarget = nextPoint;
    }

    public void Chase()
    {
        direction = playerTransform.position - transform.position;
        
    }

    public void StopChasing()
    {
        chasing = false;
    }
    private void rotateGuard()
    {
        transform.Rotate(0, 0, 180);
    }

    public void StartChasing()
    {
        chasing = true;
    }
}
