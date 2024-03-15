using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    private LoadLLMScript LoadLLMScript;
    void Start()
    {
        LoadLLMScript = GameObject.Find("player").GetComponent<LoadLLMScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            

            SceneManager.LoadScene("Judgement");
        }
    }
}
