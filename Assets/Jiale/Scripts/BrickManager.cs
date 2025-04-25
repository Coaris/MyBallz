using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {
    public List<GameObject> brickPrefab;
    [SerializeField] private int columns = 15;
    [SerializeField] private int dropDistance = 2;
    [SerializeField] private int startY = 1;
    [SerializeField] private float gameOverY = -4;

    private List<GameObject> brickInScreen = new List<GameObject>();


    //Ѫ�����
    public int roundNumber = 1; // ��ǰ�ڼ��غ�
    public float baseHP = 1;       // ��ʼѪ��
    public float rate = 0.01f;
    public float hpOffset = 2;     // ����ֵ��Χ��������2��


    public void StartGame() {
        GenerateNewRow();
        NextTurn();
        NextTurn();
    }


    public void NextTurn() {
        MoveBrickDown();
        GenerateNewRow();
    }

    //�����߼�
    private void MoveBrickDown() {
        foreach (GameObject brick in brickInScreen.ToArray()) {
            if (brick != null) {
                //����
                brick.transform.position += Vector3.down * dropDistance;

                //GameOver�ж�
                if (brick.transform.position.y <= gameOverY) {
                    GameManager.Instance.GameOver();
                }
            }
        }
    }
    //���������߼�
    private void GenerateNewRow() {
        roundNumber++;
        baseHP = baseHP + baseHP * rate;
        hpOffset = hpOffset + hpOffset * rate;

        int minHP = Mathf.RoundToInt(Mathf.Max(1, roundNumber + baseHP - hpOffset));
        int maxHP = Mathf.RoundToInt(roundNumber + baseHP + hpOffset);


        for (int i = 0; i < columns; i++) {
            if (Random.value < 0.5f) continue; // ��������Ƿ�����ש��

            Vector3 spawnPos = new Vector3(ColumnToCoord(i), startY, 0);

            GameObject brick = Instantiate(brickPrefab[Random.Range(0, brickPrefab.Count)], spawnPos, Quaternion.identity);

            // �������Ѫ��
            brick.GetComponent<Brick>().SetHP(Random.Range(minHP, maxHP + 1));

            brickInScreen.Add(brick);
        }




    }
    //����ת��
    private int ColumnToCoord(int column) {
        return (int)(column - (float)columns / 2f + 0.5f);
    }



}
