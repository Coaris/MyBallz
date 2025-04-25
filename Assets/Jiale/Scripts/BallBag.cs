using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class BallBag : MonoBehaviour {
    [SerializeField] BallShooter ballShooter;

    public List<BallInfo> allBall; // 只有颜色 + prefab
    public List<Text> allBallCount;

    private List<BallInfo> turnBall = new List<BallInfo>();
    public List<Text> turnBallCount;

    private int currentIndex = 0;

    [SerializeField] private GameObject currentBallImage;

    private void Start() {
        ReloadTurnBall();
    }


    private void Update() {
        ChangeColor();
    }

    //切换颜色
    private void ChangeColor() {
        if (!ballShooter.isAiming) return;
        if (Input.GetKeyDown(KeyCode.A)) {
            currentIndex = (currentIndex - 1 + turnBall.Count) % turnBall.Count;
            UpdateBallImage();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            currentIndex = (currentIndex + 1) % turnBall.Count;
            UpdateBallImage();
        }
    }


    //重新装弹
    private void ReloadTurnBall() {
        foreach (var b in allBall) {
            BallInfo newBall = new BallInfo(b.color, b.count);
            turnBall.Add(newBall);
            UpdateBallImage();
            UpdateAllBallCount();
            //刷新每一个turnballcount
            for (int i = 0; i < allBall.Count; i++) {
                turnBallCount[i].text = allBall[i].count.ToString();
            }
        }
    }

    //重新装弹检查
    public void ReloadCheck() {
        foreach (var i in turnBall) {
            if (i.count != 0) {
                return;
            }
        }
        //全为0时，重新装弹
        ReloadTurnBall();
    }



    //图形更新

    private void UpdateBallImage() {
        switch (turnBall[currentIndex].color) {
            case BallColor.Red:
                currentBallImage.GetComponent<SpriteRenderer>().color = new Color(1, 100f / 255f, 100f / 255f, 1);
                break;
            case BallColor.Yellow:
                currentBallImage.GetComponent<SpriteRenderer>().color = new Color(1, 225f / 255f, 100f / 255f, 1);
                break;
            case BallColor.Green:
                currentBallImage.GetComponent<SpriteRenderer>().color = new Color(100f / 255f, 1, 100f / 255f, 1);
                break;
            case BallColor.Purple:
                currentBallImage.GetComponent<SpriteRenderer>().color = new Color(200f/255f, 100f / 255f, 1, 1);
                break;
            case BallColor.White:
                currentBallImage.GetComponent<SpriteRenderer>().color = new Color(225f / 255f, 225f / 255f, 225f / 255f, 1);
                break;
            default:
                currentBallImage.GetComponent<SpriteRenderer>().color = new Color(1, 100f / 255f, 100f / 255f, 1);
                break;
        }
    }

    //数字更新
    private void UpdateTurnBallCount(int index) {
        turnBallCount[index].text= turnBall[index].count.ToString();
    }

    private void UpdateAllBallCount() {
        for (int i = 0; i < allBall.Count; i++) {
            allBallCount[i].text = allBall[i].count.ToString();
        }
    }




    //获取当前小球编号
    public int GetCurrentIndex() {
        return currentIndex;
    }
    //获取当前小球本回合还有余量？
    public bool IsRemaining() {
        if (turnBall[currentIndex].count > 0) {
            return true;
        }
        else return false;
    }
    //小球减一
    public void ReduceBall() {
        if (turnBall[currentIndex].count > 0) {
            turnBall[currentIndex].count--;
            UpdateTurnBallCount(currentIndex);
        }
    }


}

public enum BallColor {
    Red, Yellow, Green, Purple, White
}

[System.Serializable]
public class BallInfo {
    public BallColor color;
    public int count;

    public BallInfo(BallColor c, int ct) {
        color = c;
        count = ct;
    }
}