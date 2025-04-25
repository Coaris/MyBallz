using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    [SerializeField] private BallShooter shooter;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ball") {
            Destroy(collision.gameObject);
            shooter.ballInScreen--;
            shooter.CheckBallInScreen(collision.transform.position.x);
        }
    }
}
