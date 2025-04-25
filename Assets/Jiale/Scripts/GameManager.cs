using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseButton;

    [SerializeField] BallShooter ballShooter;
    [SerializeField] BrickManager brickManager;


    private void Start() {
        Cursor.visible = true;
        Time.timeScale = 0;
    }



    public void LevelUp(BallColor color) {
        ballShooter.GetComponent<BallBag>().LevelUp(color);
    }

    public void NextTurn() {
        brickManager.NextTurn();
    }

    public void StartGame() {
        mainMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
        ballShooter.gameObject.SetActive(true);
        brickManager.StartGame();
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void Replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver() {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        ballShooter.gameObject.SetActive(false);
    }

}
