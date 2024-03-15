using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button startButton;
    public Button exitButton;

    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
        Button btn2 = exitButton.GetComponent<Button>();
        btn2.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
