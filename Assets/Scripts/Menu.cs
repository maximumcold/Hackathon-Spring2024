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
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
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
