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
    public bool isAiming;

    //小球
    [SerializeField] private List<GameObject> ballPrefab = new List<GameObject>();
    private GameObject currentPrefab;
    [SerializeField] private float shootForce = 500f;

    //小球背包
    [SerializeField] private BallBag ballbag;
    private float shooterTimer=0;
    private float shooterDuration = 0.1f;


    //剩余小球计数器
    public int ballInScreen = 0;


    private void Update() {
        Aim();
        DrawAimLine();
        Shooting();
    }

    //检查屏幕中是否还有小球，没有则切换到瞄准模式
    public void CheckBallInScreen(float pos) {
        if (ballInScreen > 0) return;
        ballbag.ReloadCheck();
        StartAim();
        ballbag.NextColor();
        pos = Mathf.Clamp(pos, -7, 7);
        Vector3 v = new Vector3(pos, transform.position.y, transform.position.z);
        transform.position = v;
    }
    public void StartAim() {
        isAiming = true;
    }
    public void StopAim() {
        isAiming = false;
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
                if (ballbag.IsRemaining()) {
                    ShootBall();
                }
            }
        }
    }
    //发射
    private void ShootBall() {
        currentPrefab = ballPrefab[ballbag.GetCurrentIndex()];
        StopAim();
    }
    private void Shooting() {
        if (isAiming) return;

        shooterTimer += Time.deltaTime;

        if (ballbag.IsRemaining()&& shooterTimer >= shooterDuration) {
            shooterTimer = 0f;
            GameObject ball = Instantiate(currentPrefab, transform.position, Quaternion.identity);
            ballInScreen++;
            ballbag.ReduceBall();
            ball.GetComponent<Rigidbody2D>().AddForce(aimDirection * shootForce);
        }
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
