using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    
    private bool is_Hidden = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player is hidden: " + is_Hidden);
    }

    // Update is called once per frame
    void Update()
    {
        player_movement();

    }

    void player_movement(){
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
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Flashlight"))
        {
            is_Hidden = true;
            Debug.Log("Player is hidden: " + is_Hidden);
        }
    }
}
