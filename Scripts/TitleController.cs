using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
