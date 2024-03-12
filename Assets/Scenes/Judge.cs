using UnityEngine;
using LLMUnity;
using UnityEngine.UI;

public class Judge : MonoBehaviour{
  public LLM llm;
  
  void HandleReply(string reply){
    Debug.Log(reply);
  }
  
  void Start(){
    string message = "Today's case: John Lizardman is on trial for the murder of his sister, Stacy Lizardman. The evidence: Exhibit A: The murder weapon, a pistol. It has no fingerprints. Witness 1: Jack Dragon, the rival of John Lizardman, claims that he saw John leaving the scene of the crime on the night of the murder. Witness 2: Handsy Bobson, local cafe owner, claims that John was at his cafe at the time of the murder. Determine whether there is sufficient evidence to find John Lizardman guilty of the murder.";
    _ = llm.Chat(message, HandleReply);
  }
}