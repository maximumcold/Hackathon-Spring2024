using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    private float interactionCooldown = 0.5f; // Adjust as needed
    private float lastInteractionTime;
    Animator anim;
    
    private bool is_Hidden = true;

    private bool is_Interacting = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player is hidden: " + is_Hidden);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
   private void Update() {
        player_movement();


    //     if (is_Interacting && Input.GetKeyDown(KeyCode.E)) {
    //         Debug.Log("Interacting with object");
    //         // Call the interaction logic  for the object
    //         // Perform your interaction logic here
    // }
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
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        // Check if enough time has passed since the last interaction
        if (Time.time - lastInteractionTime < interactionCooldown)
        {
            return; // Exit the method if the cooldown is still active
        }

        // Check if the "E" button is pressed
        if (Input.GetButton("Interact"))
        {
            if (other.CompareTag("Book"))
            {
                if (other.TryGetComponent(out BookObject interactableObject))
                {
                    interactableObject.Interact();
                    lastInteractionTime = Time.time; // Update the last interaction time
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Flashlight")) {
            is_Hidden = true;
            Debug.Log("Player is hidden: " + is_Hidden);
        }
    }
   private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Flashlight")) {
            is_Hidden = false;
            Debug.Log("Player is hidden: " + is_Hidden);
        }
   }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Flashlight")) {
            is_Hidden = true;
            Debug.Log("Player is hidden: " + is_Hidden);
        }

        if (other.CompareTag("Book")) {
            is_Interacting = false;
        }
    }


}