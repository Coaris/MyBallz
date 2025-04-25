using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour {
    //瞄准辅助线
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3[] aimLinePos = new Vector3[2];//瞄准辅助线的起点与终点坐标
    [SerializeField] private LayerMask hitLayers;//砖块和墙壁
    private float maxDistance = 100f;
    private Vector2 aimDirection;
    private RaycastHit2D hit;


    //状态控制
    [SerializeField] private bool isAiming;

    //小球
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float shootForce = 500f;

    ////小球背包
    //private List<int> ballBag = new List<int>(5);//1红  2黄  3绿  4紫  5白
    //private List<int> ballRemaining = new List<int>(5);

    //[Range(0,4)]private int currentBall = 0;

    ////小球切换
    //[SerializeField] private GameObject currentBallImage;
    //private Color red = new Color(255 / 255, 100 / 255, 100 / 255);
    //private Color yellow = new Color(255 / 255, 225 / 255, 100 / 255);
    //private Color green = new Color(100 / 255, 255 / 255, 100 / 255);
    //private Color purple = new Color(255 / 255, 100 / 255, 255 / 255);
    //private Color white = new Color(225 / 255, 225 / 255, 225 / 255);


    private void Update() {
        Aim();
        DrawAimLine();
    }



    public void StartAim() {
        isAiming = true;
        //currentBallImage.SetActive(true);
    }
    public void StopAim() {
        isAiming = false;
        //currentBallImage.SetActive(false);
    }


    //瞄准
    private void Aim() {
        if (isAiming) {
            aimLinePos[0] = transform.position;//瞄准线起点

            //鼠标瞄准方向
            aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            //沿鼠标射线检测
            hit = Physics2D.Raycast(transform.position, aimDirection, maxDistance, hitLayers);
            if (hit.collider != null) {
                aimLinePos[1] = hit.point;//瞄准线终点
            }

            //发射
            if (Input.GetMouseButtonDown(0)) {
                ShootBall();
            }
        }
    }
    //发射
    private void ShootBall() {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(aimDirection * shootForce);
        //RemoveColor();
        StopAim();
    }

    //绘制瞄准线
    private void DrawAimLine() {
        //开关控制
        if (isAiming) {
            lineRenderer.enabled = true;
        }
        else {
            lineRenderer.enabled = false;
        }
        //方向控制
        lineRenderer.SetPositions(aimLinePos);
    }
}
