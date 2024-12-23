using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    
    const int StageChipSize = 30;　//１ステージのサイズ

    int currentChipIndex;　//最先端のステージ番号が何番か

    public Transform character;//Playerの情報
    public GameObject[] stageChips; //ランダムに生成したいステージ達の素
    public int startChipIndex; //最初の開始番号
    public int preInstantiate; //何個ステージを維持するのか(初期登録 + preInstantiate個分維持する)
    public List<GameObject> generatedStageList = new List<GameObject>();  //その時の維持されているステージをリスト化（先入先出法）

    // Start is called before the first frame update
    void Start()
    {
        //startChipIndex(1)から1引いた数(0)が最先端のステージ番号
        //※直後に更新される
        currentChipIndex = startChipIndex - 1;

        //まずはゲーム開始と同時にpreInstantiate(5個)分だけステージを生成する
        UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerのZ位置をステージのサイズ30で割るとPlayerの現在Indexがわかる
        int charaPositionIndex = (int)(character.position.z /StageChipSize);

        //Playerの現在IndexにpreInstantiate(5個)足した数が、最先端のステージ番号を上回ってしまったら
        if(charaPositionIndex + preInstantiate > currentChipIndex)
        {
            //ステージを補充するメソッド
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    //引数に指定された番号までステージを補充
    void UpdateStage(int toChipIndex)
    {
        //もしも引数に与えられた番号が最先端のステージ番号以下であれば何かの間違いなのでreturn
        if (toChipIndex <= currentChipIndex) return;


        //カウンタ変数iは最先端のステージ番号
        //引数に指定されたステージ番号まで生成を繰り返す
        for(int i = currentChipIndex; i < toChipIndex; i++)
        {
            //実際の生成はGenerateStageメソッドを使う。生成されたオブジェクトを変数stageObjectに代入
            GameObject stageObject = GenerateStage(i);

            //生成されたステージ情報をリストの末尾に追加
            generatedStageList.Add(stageObject);
        }

        //preInstantiate(5個) + 2個 = 7個
        //リストに記載されたステージ情報が7個を上回ってしまったら
        //DestroyOldestStageメソッドで古いステージをHierarchyとリストから削除しておく
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        //最先端のステージ番号を引数で指定された番号に更新
        currentChipIndex = toChipIndex;
    }

    //ステージを生成するメソッド
    GameObject GenerateStage(int chipIndex)
    {
        //配列stageChipsの数を見ながら数字をランダムに決める
        int nextStageChip = Random.Range(0, stageChips.Length);

        //Instantiateメソッドでステージ生成
        //①stageChips配列からランダム数字の番号のステージをチョイス
        //②引数にあたえられたステージ番号*30サイズのZ位置にあたらしく生成
        //③特に回転はしない
        GameObject stageObject = Instantiate(
            stageChips[nextStageChip],
            new Vector3(0,0,chipIndex * StageChipSize),
            Quaternion.identity
            );

        //Instantiateメソッドでつくったオブジェクト情報を戻り値として返す
        return stageObject;
    }


    //Hierarchyとリストから古いステージを削除する
    //先入先出法(First-in, first-out method)で古いもの(先頭：0番)を消す
    void DestroyOldestStage()
    {
        //リストの先頭(0番目)のオブジェクト情報を変数oldStageに取得
        GameObject oldStage = generatedStageList[0];
        //リストの先頭の情報を抹消
        generatedStageList.RemoveAt(0);
        //変数oldStageに取得したステージオブジェクトをHerarchyから抹消
        Destroy(oldStage);
    }
}
