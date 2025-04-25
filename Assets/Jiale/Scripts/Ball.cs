using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public BallColor color;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Brick") {
            //颜色不同
            if (color != collision.transform.GetComponent<Brick>().color) {
                collision.transform.GetComponent<Brick>().ReduceHP();
            }
            //颜色相同
            else {
                GameManager.Instance.GetCoin(color);
                Destroy(collision.gameObject);
            }
            
        }
    }
}