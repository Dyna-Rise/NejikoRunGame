using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProのテキストを扱いたい

public class GameController : MonoBehaviour
{
    //ネジコの残ライフを知りたい
    public NejikoController nejiko;

    //TMPのTextオブジェクトの取扱い
    public TextMeshProUGUI scoreText;

    //LifePanelスクリプトのUpdateLife()を使いたい
    public LifePanel lifePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //常にスコア更新
        int score = CalcScore();
        //TMPのtext欄を書き換え
        scoreText.text = "Score : " + score + "m";

        //ライフパネルを更新
        //引数にネジコの現ライフを指定
        lifePanel.UpdateLife(nejiko.Life());
    }

    //ネジコのPosition：zを返すメソッド
    int CalcScore()
    {
        //ネジコの走行距離(Position:zどこまで進んだか)を取得
        return (int)nejiko.transform.position.z;
    }
}
