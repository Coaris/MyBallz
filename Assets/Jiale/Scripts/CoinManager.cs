using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {
    public List<Text> coinText;

    public List<BallInfo> coinBag;

    //��Ǯ
    public void GetCoin(BallColor color) {
        foreach (var i in coinBag) {
            if (i.color == color) {
                i.count++;
                UpdateCoinText();
            }
        }
    }


    //��Ǯ
    public void LoosCoin(BallColor color,int amount) {
        foreach (var i in coinBag) {
            if (i.color == color) {
                i.count-=amount;
                UpdateCoinText();
            }
        }
    }

    private void UpdateCoinText() {
        for (int i = 0; i < coinBag.Count; i++) {
            coinText[i].text = coinBag[i].count.ToString();
        }
    }



    public void TryPurchase(int index, BallColor _ball, BallColor c1, int newCost1, BallColor c2, int newCost2, BallColor c3, int newCost3) {


        if (!CheckCoin(c1, newCost1) || !CheckCoin(c2, newCost2) || !CheckCoin(c3, newCost3)) {
            Debug.Log("��Ҳ��㣬�޷�����");
            return;  // ֱ�ӷ��أ����ܹ���
        }

        LoosCoin(c1, newCost1);
        LoosCoin(c2, newCost2);
        LoosCoin(c3, newCost3);

        GameManager.Instance.LevelUp(_ball);
        GameManager.Instance.ReloadGood(index);
    }

    private bool CheckCoin(BallColor c, int cost) {
        foreach (var i in coinBag) {
            if (i.color == c&&i.count>=cost) {
                return true;
            }
        }
        return false;
    }


}


[System.Serializable]
public class CurrencyCost {
    public BallColor color; // ʹ�õĻ�����ɫ
    public int amount;      // ��������
}
