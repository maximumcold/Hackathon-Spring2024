using UnityEngine;
using LLMUnity;
using UnityEngine.UI;

public class Judge : MonoBehaviour{
  public LLM llm;
  
  void HandleReply(string reply){
    Debug.Log(reply);
  }
  
  void Start(){
    string message = "What is your verdict?";
    _ = llm.Chat(message, HandleReply);
  }
}