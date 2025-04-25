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

        // 随机小球颜色
        good.ballColorSold = GetRandomColor();

        // 随机货币颜色 & 价格（0~2）
        
        BallColor c1 = GetRandomColor();
        good.costColor1 = c1;
        good.costAmount1 = Random.Range(1, 3);

        good.costColor2 = GetRandomColor(c1);
        good.costAmount2 = Random.Range(0, 3);

        good.costColor3 = GetRandomColor();
        good.costAmount3 =0;

        // 显示价格（你应在 Good 内部处理价格为 0 时隐藏图标和文字）
        good.UpdateDisplay();

        // 设置索引和商店回调（用于购买后补货）
        good.index = index;

        // 替换列表里的对应项
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
            randomColor = (BallColor)Random.Range(0, 4); // 假设 BallColor 枚举有 4 个值
        } while (randomColor == c);

        return randomColor;
    }

    public void ReloadGood(int index) {
        Debug.Log("000");
        Destroy(goods[index].GetComponent<GameObject>());
        GenerateGood(index);
    }

}
