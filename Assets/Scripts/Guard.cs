using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Guard : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerTransform;
    public bool chasing = false;
    public bool waiting = false;
    private Vector2 direction;
    public float walkSpeed = 5f;
    private int currentTarget;
    [SerializeField]
    public Transform[] waypoints = new Transform[2]; // Array to hold waypoints
    
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;

    }
    void Update()
    {
        if (chasing)
        {
            Debug.Log("Chasing");
            direction = playerTransform.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * walkSpeed, Space.World); // Move towards player position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * walkSpeed);
        }

        else
        {
            // Move towards current waypoint
            direction = waypoints[currentTarget].position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * walkSpeed, Space.World);
            if (Vector2.Distance(transform.position, waypoints[currentTarget].position) < 0.1f)
            {
                SetNextPoint();
        
            }
        }
    }


            // Check if reached waypoint

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("player")) // Fix: Use CompareTag instead of ==
        {
            chasing = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player")) // Fix: Use CompareTag instead of ==
        {
            SceneManager.LoadScene("GameOver");
        }
    }
     private IEnumerator WaitAtPoint()
    {
        waiting = true;
        yield return new WaitForSeconds(2);
        RotateGuard(); // Fix: Correct method name to follow naming conventions
        waiting = false;
    }
    public void SetNextPoint()
    {
        StartCoroutine(WaitAtPoint());

        // Pick the next waypoint in order
        currentTarget++;

        // If reached the last waypoint, start descending
        if (currentTarget >= waypoints.Length)
        {
            currentTarget = waypoints.Length - 2;
        }

        // If reached the first waypoint, start ascending
        if (currentTarget < 0)
        {
            currentTarget = 1;
        }
    }
    public void StopChasing()
    {
        chasing = false;
    }
    private void RotateGuard() // Fix: Correct method name to follow naming conventions
    {
        transform.Rotate(0, 0, 180);
    }

    public void StartChasing()
    {
        chasing = true;
    }


}

