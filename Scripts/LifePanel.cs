using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour
{
    //LifeIconオブジェクトを順番に格納
    public GameObject[] icons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //残りライフに応じて各LifeIconの描画が変わる
    public void UpdateLife(int life)
    {
        //LifeIconの数だけ繰り返す
        for (int i = 0; i < icons.Length; i++)
        {
            //引数に与えた情報より小さい番号のIconは表示する
            if (i < life) icons[i].SetActive(true);
            else icons[i].SetActive(false);
        }
    }

}
