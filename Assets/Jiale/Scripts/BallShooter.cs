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
    [SerializeField] private bool isAiming;

    //С��
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float shootForce = 500f;

    ////С�򱳰�
    //private List<int> ballBag = new List<int>(5);//1��  2��  3��  4��  5��
    //private List<int> ballRemaining = new List<int>(5);

    //[Range(0,4)]private int currentBall = 0;

    ////С���л�
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
                ShootBall();
            }
        }
    }
    //����
    private void ShootBall() {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(aimDirection * shootForce);
        //RemoveColor();
        StopAim();
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
