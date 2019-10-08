using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class buttonTap : MonoBehaviour {

    public GameObject gamesystem; // ゲームシステムオブジェクトのこと
    public int number; // このボタンの値
    public GameObject childText; // このボタンの値の表示GUI
    public int numberRange = 4; // 初期値の範囲(2から2のnumberRange乗)
    // ボムであった場合0以外になる 1:縦横の数値増加 2:タイム増加 3:コイン増加
    // 2048=ボムにしよう
    public int bomb = 0;
    public Sprite[] bombSprite; // インスペクターでボムのスプライトを設定
    Image imageCom;

    // Use this for initialization
    void Start() {
        // gamesystemタグのゲームオブジェクトを探索
        gamesystem = GameObject.FindGameObjectWithTag("gamesystem");
        // なぜか親子関係はtransformオブジェクトが担ってるんだよなぁ
        childText = this.gameObject.transform.Find("Text").gameObject;
        //number = 2;
        numberStart();

        //Debug.Log(childText);

        //imageCom = this.gameObject.GetComponent<Image>();
        //imageCom = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update() {

    }

    /// ボタンをクリックした時の処理
    public void OnClick()
    {
        // ボタンプレハブにこれをアタッチしてonclickの+を押してボタンプレハブを選択、更に別の枠でこのスクリプトを選択する
        // そうする事によってこいつを有効化している
        // https://qiita.com/2dgames_jp/items/b3d7d204895d67742d0c
        // colliderでも良くね？
        //Debug.Log("Button click!");

        /*
         * // onGUI操作のデバッグ用
        number *= 2;
        childText.GetComponent<Text>().text = number.ToString();
        */

        // gamesystemタグのゲームオブジェクトにアタッチされているstartスクリプトのclickafterメソッドを呼び出している
        // このゲームオブジェクトを相手に投げている
        gamesystem.GetComponent<start>().clickAfter(this.gameObject);
    }

    public void numberUp()
    {
        gamesystem.GetComponent<start>().scorePlus(number);
        number *= 2;

        childText.GetComponent<Text>().text = number.ToString();

        bombCheck();

    }

    public void numberStart()
    {
        int numCount = Random.Range(1, numberRange);
        //int numCount = 11; // ボムチェック用に全部ボムに
        //Debug.Log(numCount);
        //Debug.Log(numberRange);
        number = 1;
        for (int i = 0; i < numCount; i++) {
            number *= 2;
        }
        bombCheck();

        childText.GetComponent<Text>().text = number.ToString();
    }

    public void bombCheck()
    {
        

        if (number == gamesystem.GetComponent<start>().numberMax)
        {
            /*
            bomb = Random.Range(1, 4); // 1から3までだと1から4を指定しないといけない(int型だと)
            //Debug.Log(bomb);
            Debug.Log(imageCom);
            imageCom.sprite = bombSprite[bomb - 1];
            */

            gamesystem.GetComponent<start>().scorePlus(number);
            numberStart();

        }
        else
        {

            bomb = 0;
        }

        
    }
}
