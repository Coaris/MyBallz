using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Good : MonoBehaviour {

    public int index;

    [SerializeField] SpriteRenderer ball;
    public BallColor ballColorSold;              // 售卖的小球颜色
    //第一个
    [SerializeField] SpriteRenderer coin1;
    public BallColor costColor1;
    [SerializeField] TextMeshPro coinText1;
    public int costAmount1;
    //第二个
    [SerializeField] SpriteRenderer coin2;
    public BallColor costColor2;
    [SerializeField] TextMeshPro coinText2;
    public int costAmount2;
    //第三个
    [SerializeField] SpriteRenderer coin3;
    public BallColor costColor3;
    [SerializeField] TextMeshPro coinText3;
    public int costAmount3;




    private void Start() {
        UpdatePriceDisplay();
        UpdateBallColorSold();
    }


    public void UpdateDisplay() {
        UpdatePriceDisplay();
        UpdateBallColorSold();
    }

    private void UpdateBallColorSold() {
        UpdateColor(ball, ballColorSold);
    }

    private void UpdatePriceDisplay() {
        if (costAmount1 == 0) {
            coin1.gameObject.SetActive(false);
            coinText1.gameObject.SetActive(false);
        }
        else {
            coin1.gameObject.SetActive(true);
            UpdateColor(coin1, costColor1);
            coinText1.gameObject.SetActive(true);
            coinText1.text = costAmount1.ToString();
        }

        // 第二个货币
        if (costAmount2 == 0) {
            coin2.gameObject.SetActive(false);
            coinText2.gameObject.SetActive(false);
        }
        else {
            coin2.gameObject.SetActive(true);
            UpdateColor(coin2, costColor2);
            coinText2.gameObject.SetActive(true);
            coinText2.text = costAmount2.ToString();
        }

        // 第三个货币
        if (costAmount3 == 0) {
            coin3.gameObject.SetActive(false);
            coinText3.gameObject.SetActive(false);
        }
        else {
            coin3.gameObject.SetActive(true);
            UpdateColor(coin3, costColor3);
            coinText3.gameObject.SetActive(true);
            coinText3.text = costAmount3.ToString();
        }
    }

    //颜色转换
    private void UpdateColor(SpriteRenderer image,BallColor _color) {
        switch (_color) {
            case BallColor.Red:
                image.color = new Color(1, 100f / 255f, 100f / 255f, 1);
                break;
            case BallColor.Yellow:
                image.color = new Color(1, 225f / 255f, 100f / 255f, 1);
                break;
            case BallColor.Green:
                image.color = new Color(100f / 255f, 1, 100f / 255f, 1);
                break;
            case BallColor.Purple:
                image.color = new Color(200f / 255f, 100f / 255f, 1, 1);
                break;
            case BallColor.White:
                image.color = new Color(225f / 255f, 225f / 255f, 225f / 255f, 1);
                break;
            default:
                image.color = new Color(1, 100f / 255f, 100f / 255f, 1);
                break;
        }
    }


    // 当商品价格改变时更新显示
    public void SetPrice(BallColor _ball,BallColor c1,int newCost1, BallColor c2, int newCost2, BallColor c3, int newCost3) {
        ballColorSold = _ball;

        costColor1 = c1;
        costAmount1 = newCost1;
        costColor2 = c2;
        costAmount2 = newCost2;
        costColor3 = c3;
        costAmount3 = newCost3;

        UpdatePriceDisplay(); // 更新显示
        UpdateBallColorSold();
    }


    private void OnMouseOver() {
        // 鼠标在物体上时检测右键点击
        if (Input.GetMouseButtonDown(1)) // 鼠标右键
        {
            TryPurchase();
        }
    }

    private void TryPurchase() {
        GameManager.Instance.TryPurchase(index, ballColorSold, costColor1, costAmount1, costColor2, costAmount2, costColor3, costAmount3);
    }
}
