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
    [SerializeField] CoinManager coinManager;
    [SerializeField] Shop shop;


    private void Start() {
        Cursor.visible = true;
        Time.timeScale = 0;
    }


    public void GetCoin(BallColor color) {
        coinManager.GetCoin(color);
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
        shop.StartGame();
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


    public void TryPurchase(int index,BallColor _ball, BallColor c1, int newCost1, BallColor c2, int newCost2, BallColor c3, int newCost3) {
        coinManager.TryPurchase(index, _ball, c1, newCost1, c2, newCost2, c3, newCost3);
       
    }



    public void ReloadGood(int index) {
        shop.ReloadGood(index);
    }
}
