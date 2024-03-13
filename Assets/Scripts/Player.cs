using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    private bool isHidden = true;

    private bool isInteracting = false;
    private bool isShowingMenu = false;

    public GameObject menuCanvas;
    public TextMeshProUGUI menuText;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delay());
        
        Debug.Log("Player is hidden: " + isHidden);
        anim = GetComponent<Animator>();

        menuCanvas = GameObject.Find("MenuCanvas");
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
   private void Update() {
        playerMovement();
    }

    void playerMovement(){
        
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
        // Move the player based on input
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
                    menuCanvas.SetActive(true);
                    
                    menuText.verticalAlignment = VerticalAlignmentOptions.Top;
                    menuText.text = "You have found a book! It probably contains some vital information about the case! Would be a shame is something were to happen to it...";
                  
                    menuText.text += "\nPress Space to continue";
                    isShowingMenu = true;
                }
            }
        }
        // Options for interacting with the book object (can be moved to a separate method if needed)
        if (isShowingMenu && Input.GetButton("Continue"))
        {
            menuText.text = "1. Leave the book alone, it isn't worth tarnishing your reputation as a lizard lawyer.";
            menuText.text += "\n2. Burn the book, let its forbidden knowledge be lost to time.";
            menuText.text += "\n3. Tamper the evidence by writing someone else's name as the ownwer of the book.";
        }

        if (isShowingMenu && Input.GetButton("1"))
        {
            menuText.text = "You leave the book alone, it isn't worth tarnishing your reputation as a lizard lawyer.";
            delay();
        }
        if (isShowingMenu && Input.GetButton("2"))
        {
            menuText.text = "You burn the book, its secrets are yours now.";
            delay();
            // Add code to destroy the book object that doesn't make it destroy immediately
        }
        if (isShowingMenu && Input.GetButton("3"))
        {
            menuText.text = "You write the name of your best friend into the book, Ezra Thomas Billings the Third.";
            delay();
        }
    }


    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Flashlight")) {
            isHidden = true;
            Debug.Log("Player is hidden: " + isHidden);
        }
    }


   private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Flashlight")) {
            isHidden = false;
            Debug.Log("Player is hidden: " + isHidden);
        }
   }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Flashlight")) {
            isHidden = true;
            Debug.Log("Player is hidden: " + isHidden);
        }

        if (other.CompareTag("Book")) {
            isInteracting = false;
            menuCanvas.SetActive(false);
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(10);

        menuCanvas.SetActive(false);
    }

}