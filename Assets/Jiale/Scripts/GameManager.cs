using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseButton;

    [SerializeField] BallShooter ballShooter;


    private void Start() {
        Time.timeScale = 0;
    }

    public void StartGame() {
        mainMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
        ballShooter.gameObject.SetActive(true);
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void Replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
