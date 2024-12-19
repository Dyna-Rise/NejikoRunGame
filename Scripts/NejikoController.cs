using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    //レーンの管理に関係
    //レーンの前提条件
    const int MinLane = -2; //最も左のレーン
    const int MaxLane = 2; //最も右のレーン
    const float LaneWidth = 1.0f; //レーンの幅

    int targetLane; //プレイ中に随時目指すべきレーン

    //コンポーネントの参照用
    CharacterController controller;
    Animator animator;

    //ローカル座標
    Vector3 moveDirection = Vector3.zero;

    //各種設定
    public float gravity;　//重力の強さ
    public float speedZ;  //スピード 前に進む力
    public float speedX; //横に移動する力
    public float speedJump; //ジャンプ力
    public float accelarationZ; //Nejikoがトップスピードにいくための加速度

    // Start is called before the first frame update
    void Start()
    {
        //必要なコンポーネントを取得
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //キー入力によるメソッド発動
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        //自動で前に進む　※トップスピードを目指して徐々に加速する
        //①加速値を決める
        float accelaratedZ = moveDirection.z + (accelarationZ * Time.deltaTime);
        //②moveDirection.zを最終的な加速値に書き換える
        //Clampは第2引数～第3引数の数値を越える際、最小値か最大値に変換してしまう
        //※speedZで早さを打ち止め
        moveDirection.z = Mathf.Clamp(accelaratedZ, 0, speedZ);

        //左右キーの入力状況に応じてレーン移動
        //※目的地に近づくほどradioXの値は減衰
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;


        //常に重力の力がY軸にかかっている
        moveDirection.y -= gravity * Time.deltaTime;


        //プレイヤー主観における動きのデータ(変数moveDirection)がグローバル座標でいうとどうなるのか？という数字に変換
        //※特にプレイヤーが回転してしまう分、前後の動きが主観とグローバルで異なるため
        Vector3 globalDirection = transform.TransformDirection(moveDirection);

        //グローバル座標におきかえたプレイヤーの動きのデータをもとに実際に動作させる
        //CharactorControllerコンポーネントのMoveメソッドの引数に動かしたい方向を指定
        controller.Move(globalDirection * Time.deltaTime);
        //controller.Move(moveDirection * Time.deltaTime);

        //もしプレイヤーが地面に触れていたらY軸にかかる力を0にする
        if (controller.isGrounded) moveDirection.y = 0;

        //アニメーターのBoolパラメータ"run"をオンか、オフにする
        //プレイヤーが前後に走っている時つまり(moveDirection.z != 0)がtrueの時→オン
        //プレイヤーが止まっている時つまり(moveDirection.z != 0)がfalseの時→オフ
        //※オフという事はIdleアニメになっている
        animator.SetBool("run", moveDirection.z != 0.0f);
    }

    //自作メソッド

    //左ボタンがおされたら左レーンをtargetにする
    public void MoveToLeft()
    {
        //地面にいる＆それまでに利用したtargetLaneの数字が最小値(-2)にまだいってなかったら
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    //右ボタンがおされたら右レーンをtargetにする
    public void MoveToRight()
    {
        //地面にいる＆それまでに利用したtargetLaneの数字が最大値(2)にまだいってなかったら
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }


    //Updateの中でジャンプボタンがおされたら発動
    public void Jump()
    {
        //CharactorControllerコンポーネントの能力で地面判定ができる(→isGrounded)
        if (controller.isGrounded)　//地面にいる時
        {
            //Y軸方向に変数speedJumpに設定した数字を加える
            moveDirection.y = speedJump;
            //アニメーターのTriggerパラメータ"jump"をオンにしてジャンプアニメに移行
            //※Triggerパラメータは一度発動したら自動ですぐオフになり元のアニメに戻る
            animator.SetTrigger("jump");
        }
    }

}
