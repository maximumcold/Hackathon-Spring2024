using System.Collections;
using System.Collections.Generic;
using LLMUnity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    private float interactionCooldown = 0.5f; // Adjust as needed
    private float lastInteractionTime;
    private float duration;
    float triggerStartTime;
    Animator anim;

    public Guard[] guards;
    
    private bool isHidden = true;
    private bool isInteracting = false;
    private bool isInTriggerSpace = false;
    private bool isShowingMenu = false;
    private bool interactedBook = false;
    private bool interactedGun = false;
    private bool interactedTape = false;
    private bool interactedJudgeDesk = false;

    public GameObject menuCanvas;
    public TextMeshProUGUI menuText;

    private LoadLLMScript LoadLLMScript;

    // Start is called before the first frame update
    void Start()
    {
        LoadLLMScript = GetComponent<LoadLLMScript>();
        StartCoroutine(delay());
        guards = FindObjectsOfType<Guard>();

        Debug.Log("Player is hidden: " + isHidden);
        anim = GetComponent<Animator>();

        menuCanvas = GameObject.Find("MenuCanvas");
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
   private void Update() {
        PlayerMovement();
        if(isInTriggerSpace)
        {
            if (Time.time - triggerStartTime > duration)
            {
                Debug.Log("Player is hidden has been in trigger space for " + duration + " seconds");
            }
        }
    }

    void PlayerMovement()
    {
        // Switches between idle and walk animation
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("isWalking", true);

            // Flip animator based on direction
            if (Input.GetKey(KeyCode.A))
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            // Check for collisions before applying movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;

            transform.position += movement * speed * Time.deltaTime;
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Guard"))
        {

            if (other.TryGetComponent(out Guard guard))
            {
               guard.chasing = true;
               guard.waiting = true;
            }
           
        }
        // Check if enough time has passed since the last interaction
        if (Time.time - lastInteractionTime < interactionCooldown)
        {
            return;
        }


        // Check if the "E" button is pressed
        if (other.CompareTag("Book"))
        {
            if (Input.GetButton("Interact"))
            {
                if(!interactedBook){

                    if (other.TryGetComponent(out BookObject interactableObject))
                    {
                        lastInteractionTime = Time.time; // Update the last interaction time
                        menuCanvas.SetActive(true);
                        
                        menuText.verticalAlignment = VerticalAlignmentOptions.Top;
                        menuText.text = "You have found a book! It probably contains some vital information about the case! Would be a shame is something were to happen to it...";
                    
                        menuText.text += "\nPress Space to continue";
                        isShowingMenu = true;
                    }
                }
                else
                {   menuCanvas.SetActive(true);
                    menuText.text = "You have already interactred with this object. Maybe you can find more evidence to tamper with...";
                    delay();
                }
            }
        
        // Options for interacting with the book object (can be moved to a separate method if needed)
            if (isShowingMenu && Input.GetButton("Continue") && !interactedBook)
            {
                menuText.text = "1. Leave the book alone, it isn't worth tarnishing your reputation as a lizard lawyer.";
                menuText.text += "\n2. Burn the book, let its forbidden knowledge be lost to time.";
                menuText.text += "\n3. Tamper the evidence by writing someone's name as the owner of the book.";
            }

            if (isShowingMenu && Input.GetButton("1") && !interactedBook)
            {
                menuText.text = "You leave the book alone, it isn't worth tarnishing your reputation as a lizard lawyer.";
                delay();
            }
            if (isShowingMenu && Input.GetButton("2")  && !interactedBook)
            {
                menuText.text = "You burn the book, its secrets are yours now.";
                LoadLLMScript.bookBurned = true;
                LoadLLMScript.ObjectMessages();
                interactedBook = true;
                delay();
            }
            if (isShowingMenu && Input.GetButton("3") && !interactedBook)
            {
                menuText.text = "You write the name of your best friend into the book, Ezra Thomas Billings the Third.";
                LoadLLMScript.bookTampered = true;
                LoadLLMScript.ObjectMessages();
                interactedBook = true;
                delay();
            }
        }
        if (other.CompareTag("Gun")){

            if (Input.GetButton("Interact")){

                if (!interactedGun){

                    if (other.TryGetComponent(out GunObject interactableObject))
                    {
                        lastInteractionTime = Time.time; // Update the last interaction time
                        menuCanvas.SetActive(true);
                        menuText.text = "You have found a gun! It probably contains some vital information about the case! Would be a shame is something were to happen to it...";
                        menuText.text += "\nPress Space to continue";
                        isShowingMenu = true;
                    }
                }
                else
                {   menuCanvas.SetActive(true);
                    menuText.text = "You have already interactred with this object. Maybe you can find more evidence to tamper with...";
                    delay();
                }
            }
            if (isShowingMenu && Input.GetButton("Continue") && !interactedGun)
            {
                menuText.text = "1. Leave the gun alone, it isn't worth tarnishing your reputation as a lizard lawyer.";
                menuText.text += "\n2. Wipe the gun clean of fingerprints.";
                menuText.text += "\n3. Plant someone else's fingerprints on the gun.";
            }

            if (isShowingMenu && Input.GetButton("1") && !interactedGun)
            {
                menuText.text = "You leave the gun alone, it isn't worth tarnishing your reputation as a lizard lawyer.";
                delay();
            }

            if (isShowingMenu && Input.GetButton("2") && !interactedGun)
            {
                menuText.text = "You wipe the gun clean of fingerprints.";
                LoadLLMScript.gunFingerPrintsCleared = true;
                LoadLLMScript.ObjectMessages();
                interactedGun = true;
                delay();
            }

            if (isShowingMenu && Input.GetButton("3") && !interactedGun)
            {
                menuText.text = "You plant someone else's fingerprints on the gun.";
                LoadLLMScript.gunFigerPrintsReplaced = true;
                LoadLLMScript.ObjectMessages();
                interactedGun = true;
                delay();
            }
        }

        if(other.CompareTag("Tape"))
        {
            if (Input.GetButton("Interact"))
            {
                if (!interactedTape){

                    if (other.TryGetComponent(out TapeObject interactableObject))
                    {
                        lastInteractionTime = Time.time; // Update the last interaction time
                        menuCanvas.SetActive(true);
                        menuText.text = "You have found a tape! It probably contains some vital information about the case! Would be a shame is something were to happen to it...";
                        menuText.text += "\nPress Space to continue";
                        isShowingMenu = true;
                    }
                }
                else
                {   menuCanvas.SetActive(true);
                    menuText.text = "You have already interactred with this object. Maybe you can find more evidence to tamper with...";
                    delay();
                }
            }
            if (isShowingMenu && Input.GetButton("Continue") && !interactedTape)
            {
                menuText.text = "1. Leave the tape alone, it isn't worth tarnishing your reputation as a lizard lawyer.";
                menuText.text += "\n2. Erase the tape.";
                menuText.text += "\n3. Replace the tape with a video of the Teletubbies.";
            }
            if (isShowingMenu && Input.GetButton("1") && !interactedTape)
            {
                menuText.text = "You leave the tape alone, it isn't worth tarnishing your reputation as a lizard lawyer.";
                delay();
            }
            if (isShowingMenu && Input.GetButton("2") && !interactedTape)
            {
                menuText.text = "You erase the tape.";
                LoadLLMScript.tapeErased = true;
                LoadLLMScript.ObjectMessages();
                interactedTape = true;
                delay();
            }
            if (isShowingMenu && Input.GetButton("3") && !interactedTape)
            {
                menuText.text = "You replace the tape with a video of the Teletubbies.";
                LoadLLMScript.tapeReplaced = true;
                LoadLLMScript.ObjectMessages();
                interactedTape = true;
                delay();
            }
        }
        if (other.CompareTag("JudgeDesk")){

            if (Input.GetButton("Interact"))
            {
                if (!interactedJudgeDesk){

                    if (other.TryGetComponent(out JudgeDeskObject interactableObject))
                    {
                        lastInteractionTime = Time.time; // Update the last interaction time
                        menuCanvas.SetActive(true);
                        menuText.text = "You have found the Judge's desk! There is a bowl of food on it and a piece of paper.";
                        menuText.text += "\nPress Space to continue";
                        isShowingMenu = true;
                    }
                }
                else
                {   menuCanvas.SetActive(true);
                    menuText.text = "You have already interactred with this object. Maybe you can find more evidence to tamper with...";
                    delay();
                }
            }
            if (isShowingMenu && Input.GetButton("Continue") && !interactedJudgeDesk)
            {
                menuText.text = "1. Leave the food and paper alone, it isn't yours after all.";
                menuText.text += "\n2. Poison the food.";
                menuText.text += "\n3. Leave a bribe for the judge.";
            }
            if (isShowingMenu && Input.GetButton("1") && !interactedJudgeDesk)
            {
                menuText.text = "You leave the food and paper alone, it isn't yours after all.";
                delay();
            }
            if (isShowingMenu && Input.GetButton("2") && !interactedJudgeDesk)
            {
                menuText.text = "You poison the food.";
                LoadLLMScript.judgePoisoned = true;
                LoadLLMScript.ObjectMessages();
                interactedJudgeDesk = true;
                delay();
            }
            if (isShowingMenu && Input.GetButton("3"))
            {
                menuText.text = "You leave a bribe for the judge.";
                LoadLLMScript.judgeBribed = true;
                LoadLLMScript.ObjectMessages();
                interactedJudgeDesk = true;
                delay();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // if (other.CompareTag("Guard"))
        // {

        //     if (other.TryGetComponent(out Guard guard))
        //     {
        //        guard.chasing = true;
        //     }
           
        // }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Guard")) {
            isInTriggerSpace = false;
        }

        if (other.CompareTag("Book")) {
            isInteracting = false;
            menuCanvas.SetActive(false);
        }

        if (other.CompareTag("Gun")) {
            isInteracting = false;
            menuCanvas.SetActive(false);
        }

        if (other.CompareTag("Tape")) {
            isInteracting = false;
            menuCanvas.SetActive(false);
        }

        if (other.CompareTag("JudgeDesk")) {
            isInteracting = false;
            menuCanvas.SetActive(false);
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(5);

        menuCanvas.SetActive(false);
    }

}