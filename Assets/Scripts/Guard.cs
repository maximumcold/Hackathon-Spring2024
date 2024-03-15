using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Guard : MonoBehaviour
{
    private Transform playerTransform;
    public bool chasing = false;
    public bool waiting = false;
    private Vector2 direction;
    public float walkSpeed = 5f;
    private int currentTarget;
    [SerializeField]
    public Transform[] waypoints = new Transform[2]; // Array to hold waypoints
    AudioSource audioSource;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (chasing)
        {
            if (!audioSource.isPlaying){
                audioSource.Play();
            }
            Debug.Log("Chasing");
            direction = playerTransform.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * walkSpeed, Space.World); // Move towards player position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * walkSpeed);
            if (direction.y > 0 || direction.y < 0)
            {
                GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY; // Fix: Flip the guard horizontally
            }
            if (direction.x > 0 || direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX; // Fix: Flip the guard horizontally
            }
        }
        else
        {
            direction = waypoints[currentTarget].position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * walkSpeed, Space.World);
            if (Vector2.Distance(transform.position, waypoints[currentTarget].position) < 0.1f)
            {
                SetNextPoint();
                RotateGuard();
                if (direction.y > 0 || direction.y < 0)
                {
                    GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY; // Fix: Flip the guard horizontally
                }
                if (direction.x > 0 || direction.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX; // Fix: Flip the guard horizontally
                }
            }
            

            // Set up animation for depending if guard is going left or right or up and down
            GetComponent<Animator>().SetBool("walkingRight", direction.x > 0);
            GetComponent<Animator>().SetBool("walkingLeft", direction.x < 0);
            GetComponent<Animator>().SetBool("walkingUp", direction.y > 0);
            GetComponent<Animator>().SetBool("walkingDown", direction.y < 0);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            chasing = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }


    IEnumerator WaitAtPoint(){
        waiting = true;
        yield return new WaitForSeconds(2);
        RotateGuard(); // Fix: Correct method name to follow naming conventions
        waiting = false;
    }
    public void SetNextPoint()
    {
        //StartCoroutine(WaitAtPoint());

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

