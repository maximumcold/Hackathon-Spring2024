using UnityEngine;
using LLMUnity;
using UnityEngine.UI;
using TMPro;

public class Judge : MonoBehaviour{
  public LLM llm;
  public TextMeshProUGUI AIText;

  public void SetAIText(string text)
    {
        AIText.text = text;
    }
  
  public void AIReplyComplete(){
    Debug.Log("Reply Complete");
  }

  void Start(){
    string message = "Today's case: John Lizardman is on trial for the murder of his sister, Stacy Lizardman. The evidence: Exhibit A: The murder weapon, a pistol. It has no fingerprints. Witness 1: Jack Dragon, the rival of John Lizardman, claims that he saw John leaving the scene of the crime on the night of the murder. Witness 2: Handsy Bobson, local cafe owner, claims that John was at his cafe at the time of the murder. Determine whether there is sufficient evidence to find John Lizardman guilty of the murder. End your reply with the words 'Guilty' or 'Not guilty'.";
    _ = llm.Chat(message, SetAIText, AIReplyComplete);
  }
}