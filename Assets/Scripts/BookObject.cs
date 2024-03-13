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

        // Display options 
        // 1. Read
        // 2. Close
        // 3. Take
        // 4. Leave
        // 5. Burn

        // Create a canvas and panel for the popup
        GameObject canvas = new GameObject("Canvas");
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        GameObject panel = new GameObject("Panel");
        panel.transform.SetParent(canvas.transform);
        panel.AddComponent<Image>();

        // Position the panel in the center of the screen
        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
        panelRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        panelRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        panelRectTransform.pivot = new Vector2(0.5f, 0.5f);
        panelRectTransform.anchoredPosition = Vector2.zero;
        panelRectTransform.sizeDelta = new Vector2(200f, 200f);

        // Add buttons for each option
        for (int i = 0; i < 5; i++)
        {
            GameObject button = new GameObject("Button" + (i + 1));
            button.transform.SetParent(panel.transform);
            button.AddComponent<Image>();
            button.AddComponent<Button>();

            // Position the button within the panel
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            buttonRectTransform.anchorMin = new Vector2(0.5f, 1f - (i + 1) * 0.2f);
            buttonRectTransform.anchorMax = new Vector2(0.5f, 1f - i * 0.2f);
            buttonRectTransform.pivot = new Vector2(0.5f, 0.5f);
            buttonRectTransform.anchoredPosition = Vector2.zero;
            buttonRectTransform.sizeDelta = new Vector2(180f, 30f);

            // Add text to the button
            GameObject buttonText = new GameObject("Text");
            buttonText.transform.SetParent(button.transform);
            buttonText.AddComponent<Text>();
            Text textComponent = buttonText.GetComponent<Text>();
            textComponent.text = "Option " + (i + 1);
            textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textComponent.alignment = TextAnchor.MiddleCenter;
        }
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
