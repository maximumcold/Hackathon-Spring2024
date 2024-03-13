using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    Animator anim;
    
    private bool is_Hidden = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player is hidden: " + is_Hidden);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player_movement();

    }

    void player_movement(){
        
        // Switches between idle and walk animation
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("isWalking", true);
            // Flip animtor based on direction
            if (Input.GetKey(KeyCode.A))
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Flashlight"))
        {
            is_Hidden = false;
            Debug.Log("Player is hidden: " + is_Hidden);
        }
        if (other.CompareTag("ObjectInteraction"))
        {
            Debug.Log("Press E to interact");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interacting with object");
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Flashlight"))
        {
            is_Hidden = true;
            Debug.Log("Player is hidden: " + is_Hidden);
        }
        
    }
    
}
