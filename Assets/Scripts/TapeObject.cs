using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;


public class TapeObject : MonoBehaviour
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
