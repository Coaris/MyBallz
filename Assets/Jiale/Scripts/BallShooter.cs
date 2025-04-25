using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour {
    //��׼������
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3[] aimLinePos = new Vector3[2];//��׼�����ߵ�������յ�����
    [SerializeField] private LayerMask hitLayers;//ש���ǽ��
    private float maxDistance = 100f;
    private Vector2 aimDirection;
    private RaycastHit2D hit;


    //״̬����
    public bool isAiming;

    //С��
    [SerializeField] private List<GameObject> ballPrefab = new List<GameObject>();
    private GameObject currentPrefab;
    [SerializeField] private float shootForce = 500f;

    //С�򱳰�
    [SerializeField] private BallBag ballbag;
    private float shooterTimer=0;
    private float shooterDuration = 0.1f;


    //ʣ��С�������
    public int ballInScreen = 0;


    private void Update() {
        Aim();
        DrawAimLine();
        Shooting();
    }

    //�����Ļ���Ƿ���С��û�����л�����׼ģʽ
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


    //��׼
    private void Aim() {
        if (isAiming) {
            aimLinePos[0] = transform.position;//��׼�����

            //�����׼����
            aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            //��������߼��
            hit = Physics2D.Raycast(transform.position, aimDirection, maxDistance, hitLayers);
            if (hit.collider != null) {
                aimLinePos[1] = hit.point;//��׼���յ�
            }

            //����
            if (Input.GetMouseButtonDown(0)) {
                if (ballbag.IsRemaining()) {
                    ShootBall();
                }
            }
        }
    }
    //����
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

    //������׼��
    private void DrawAimLine() {
        //���ؿ���
        if (isAiming) {
            lineRenderer.enabled = true;
        }
        else {
            lineRenderer.enabled = false;
        }
        //�������
        lineRenderer.SetPositions(aimLinePos);
    }
}
