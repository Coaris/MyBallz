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

    //����Ѫ��
    public void SetHP(int value) {
        hp = value;
        UpdateHP();
    }


    //�����п�Ѫ
    public void ReduceHP() {
        hp--;
        UpdateHP();
        if (hp <= 0) {
            //������
            Destroy(this.gameObject);
            //GameManager.Instance.GetCoin(color);
        }
    }

    private void UpdateHP() {
        hpNumber.text = hp.ToString();
    }
}
