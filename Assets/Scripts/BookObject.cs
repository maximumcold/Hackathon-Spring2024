using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;


public class BookObject : ObjectInteraction
{
    // Start is called before the first frame update
    string text = "This is a book";
    private bool trigger_active = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger_active && Input.GetKeyDown(KeyCode.E)){
            // open menu of options
            Interact();
        }
    }
   
    public void Interact()
    {
        // Open menu of options
        Debug.Log("Interacting with book");

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            trigger_active = true;
            }
        }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            trigger_active = false;
            }
        }

}
