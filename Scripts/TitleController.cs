using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleController : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefsから取得したスコアをHighScoreTextオブジェクトに表示
        highScoreText.text = "High Score:" + PlayerPrefs.GetInt("HighScore") + "m";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //スタートボタンに割り当てる予定のメソッド
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
}
