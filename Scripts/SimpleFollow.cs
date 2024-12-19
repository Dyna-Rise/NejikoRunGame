using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff; //Playerとの距離差

    public GameObject target; //Playerオブジェクト情報を参照させる

    public float followSpeed; //追従スピード


    // Start is called before the first frame update
    void Start()
    {
        //ゲームスタート時点でのカメラとPlayerの距離を記憶しておく
        diff = target.transform.position - transform.position;
    }

    //Updateの後に処理される
    //※Playerの位置計算が確実に終わっているであろうタイミング
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            //スタート位置　カメラの現在地
            transform.position,
            //ゴール位置　動きのあったPlayerに対して維持したい距離（位置）※カメラが次にいるべき場所
            target.transform.position - diff,
            //スタートからゴールへの進捗率（割合）
            //決められた変数の値にTime.deltaTimeをかけ算してやることで進捗率にムラを出す
            Time.deltaTime * followSpeed
            );
    }
}
