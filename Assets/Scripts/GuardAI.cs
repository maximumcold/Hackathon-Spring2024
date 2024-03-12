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
    private Transform[] waypoints = new Transform[5]; // Array to hold waypoints

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
            rotateGuard();
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

    public void SetNextPoint()
    {
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
        rotateGuard();
    }

    public void StopChasing()
    {
        chasing = false;
    }

    private void rotateGuard()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void StartChasing()
    {
        chasing = true;
    }

    public void ToggleWaiting()
    {
        waiting = !waiting;
    }
}
