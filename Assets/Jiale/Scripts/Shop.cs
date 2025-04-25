using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] GameObject goodPrefab;

    List<Good> goods = new List<Good>(4);

    Vector3[] positions;



    private void Start() {
        positions = new Vector3[]
        {
            new Vector3(5, 1.5f, -1),
            new Vector3(7, 1.5f, -1),
            new Vector3(5, -1.5f, -1),
            new Vector3(7, -1.5f, -1),
        };

      
    }

    public void StartGame() {
        for (int i = 0; i < 4; i++) {
            GenerateGood(i);
        }
    }

    private void GenerateGood(int index) {
        if (index < 0 || index >= 4) return;

        GameObject go = Instantiate(goodPrefab, positions[index], Quaternion.identity);
        Good good = go.GetComponent<Good>();

        // ���С����ɫ
        good.ballColorSold = GetRandomColor();

        // ���������ɫ & �۸�0~2��
        
        BallColor c1 = GetRandomColor();
        good.costColor1 = c1;
        good.costAmount1 = Random.Range(1, 3);

        good.costColor2 = GetRandomColor(c1);
        good.costAmount2 = Random.Range(0, 3);

        good.costColor3 = GetRandomColor();
        good.costAmount3 =0;

        // ��ʾ�۸���Ӧ�� Good �ڲ�����۸�Ϊ 0 ʱ����ͼ������֣�
        good.UpdateDisplay();

        // �����������̵�ص������ڹ���󲹻���
        good.index = index;

        // �滻�б���Ķ�Ӧ��
        if (goods.Count > index) {
            goods[index] = good;
        }
        else {
            goods.Add(good);
        }
    }


    private BallColor GetRandomColor() {
        return (BallColor)Random.Range(0, 4);
    }
    private BallColor GetRandomColor(BallColor c) {

        BallColor randomColor;
        do {
            randomColor = (BallColor)Random.Range(0, 4); // ���� BallColor ö���� 4 ��ֵ
        } while (randomColor == c);

        return randomColor;
    }

    public void ReloadGood(int index) {
        Debug.Log("000");
        Destroy(goods[index].GetComponent<GameObject>());
        GenerateGood(index);
    }

}
