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


    //血量随机
    public int roundNumber = 1; // 当前第几回合
    public float baseHP = 1;       // 起始血量
    public float rate = 0.01f;
    public float hpOffset = 2;     // 波动值范围（例：±2）


    public void StartGame() {
        GenerateNewRow();
        NextTurn();
        NextTurn();
    }


    public void NextTurn() {
        MoveBrickDown();
        GenerateNewRow();
    }

    //下移逻辑
    private void MoveBrickDown() {
        foreach (GameObject brick in brickInScreen.ToArray()) {
            if (brick != null) {
                //下移
                brick.transform.position += Vector3.down * dropDistance;

                //GameOver判断
                if (brick.transform.position.y <= gameOverY) {
                    GameManager.Instance.GameOver();
                }
            }
        }
    }
    //方块生成逻辑
    private void GenerateNewRow() {
        roundNumber++;
        baseHP = baseHP + baseHP * rate;
        hpOffset = hpOffset + hpOffset * rate;

        int minHP = Mathf.RoundToInt(Mathf.Max(1, roundNumber + baseHP - hpOffset));
        int maxHP = Mathf.RoundToInt(roundNumber + baseHP + hpOffset);


        for (int i = 0; i < columns; i++) {
            if (Random.value < 0.5f) continue; // 随机决定是否生成砖块

            Vector3 spawnPos = new Vector3(ColumnToCoord(i), startY, 0);

            GameObject brick = Instantiate(brickPrefab[Random.Range(0, brickPrefab.Count)], spawnPos, Quaternion.identity);

            // 计算随机血量
            brick.GetComponent<Brick>().SetHP(Random.Range(minHP, maxHP + 1));

            brickInScreen.Add(brick);
        }




    }
    //坐标转换
    private int ColumnToCoord(int column) {
        return (int)(column - (float)columns / 2f + 0.5f);
    }



}
