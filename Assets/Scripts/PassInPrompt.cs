using UnityEngine;
using LLMUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PassInPrompt : MonoBehaviour
{
    public LLM llm;
    public TextMeshProUGUI AIText;
    private string prompt;
    private Queue<string> replyQueue = new Queue<string>(); // Queue to store AI replies
    private const int maxCharacters = 1000; // Maximum characters to display

    // Method to add AI reply to the queue
    private void AddReplyToQueue(string reply)
    {
        replyQueue.Enqueue(reply);
        UpdateAIText(); // Update displayed text
    }

    // Method to update the AIText display
    private void UpdateAIText()
    {
        string displayedText = "";
        int totalLength = 0;

        // Concatenate replies until total length exceeds maxCharacters
        while (replyQueue.Count > 0 && totalLength + replyQueue.Peek().Length <= maxCharacters)
        {
            string nextReply = replyQueue.Dequeue();
            displayedText += nextReply + "\n"; // Add next reply to displayed text
            totalLength += nextReply.Length;
        }

        AIText.text = displayedText;
    }

    void Start()
    {
        string message = "Assets/LLMPrompt.txt";
        prompt = System.IO.File.ReadAllText(message);
        llm.SetPrompt(prompt);
        StartCoroutine(SendChatRoutine()); // Start coroutine to send chat messages
    }

    // Coroutine to continuously send chat messages
    IEnumerator SendChatRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Adjust delay between chat messages as needed
            _ = llm.Chat("Hi", HandleReply);
        }
    }

    // Handle AI reply
    void HandleReply(string reply)
    {
        AddReplyToQueue(reply); // Add reply to queue
    }
}
