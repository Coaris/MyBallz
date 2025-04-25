using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Brick : MonoBehaviour {
    public int hp=1;
    public BallColor color;
    [SerializeField] private TextMeshPro hpNumber;


    private void Start() {
        UpdateHP();
    }

    //设置血量
    public void SetHP(int value) {
        hp = value;
        UpdateHP();
    }


    //被击中扣血
    public void ReduceHP() {
        hp--;
        UpdateHP();
        if (hp <= 0) {
            //被击碎
            Destroy(this.gameObject);
            //GameManager.Instance.GetCoin(color);
        }
    }

    private void UpdateHP() {
        hpNumber.text = hp.ToString();
    }
}
