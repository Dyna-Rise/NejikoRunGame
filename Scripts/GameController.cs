using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProのテキストを扱いたい
using UnityEngine.SceneManagement;

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

        if (nejiko.Life() <= 0)
        {
            //自分自身の次のUpdateが起こらないようにする
            enabled = false; //GameController.enabled

            //もしもPlayerPrefsに記録しておいたスコアより高いスコアだったらPlayerPrefs更新
            if(PlayerPrefs.GetInt("HighScore") < score)
            {
                //現スコアの方が高ければ、PlayerPrefs更新
                PlayerPrefs.SetInt("HighScore",score);
            }

            //2秒後にReturnToTigleメソッドを発動してシーン切り替え
            Invoke("ReturnToTitle",2.0f);
        }
    }

    //ネジコのPosition：zを返すメソッド
    int CalcScore()
    {
        //ネジコの走行距離(Position:zどこまで進んだか)を取得
        return (int)nejiko.transform.position.z;
    }


    void ReturnToTitle()
    {
        //タイトルシーンに切り替え
        SceneManager.LoadScene("Title");
    }
}
