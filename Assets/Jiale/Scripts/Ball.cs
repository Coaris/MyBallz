using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public BallColor color;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Brick") {
            //��ɫ��ͬ
            if (color != collision.transform.GetComponent<Brick>().color) {
                collision.transform.GetComponent<Brick>().ReduceHP();
            }
            //��ɫ��ͬ
            else {
                GameManager.Instance.GetCoin(color);
                Destroy(collision.gameObject);
            }
            
        }
    }
}