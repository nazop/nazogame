using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{

    private float buttonX;
    private float buttonY;
    private GameObject player;
    private GameObject prefabButton; // インスペクターから入れる
    private int yoko = 4;
    private int tate = 5;
    private GameObject[,] buttonArray; // 2次元配列
    private  Vector3 board;
    private Vector3 hidarihasi;
    private int necessaryTap = 2;
    private GameObject[] tapButton; // タップしたボタンを入れる
    private int clickNumber = 0; // 現在タップされている値
    public int numberMax = 2048; // 2の11乗までアップする
    private int score;
    private GameObject scoreText;
    private GameObject timeText;
    private int time = 0;
    private int maxTime = 60; // 1ゲーム何秒か
    private float leftTime = 0; // 前回タイムが減ってからの経過時間


    // Use this for initialization
    void Start()
    {
        score = 0;
        time = 0;
        maxTime = 30;
        numberMax = 2048;
        clickNumber = 0;

        // スコアとタイムのテキストオブジェクトを取ってくる
        scoreText = GameObject.FindGameObjectWithTag("Score");
        timeText = GameObject.FindGameObjectWithTag("Time");
        // 文字を初期化
        scoreText.GetComponent<Text>().text = " score:" + score.ToString();
        timeText.GetComponent<Text>().text = " time:" + time.ToString();

        // 画面にボタンを並べつつそのボタンをbuttonArrayに突っ込む
        // 現状buttonArray使ってねーけど

        // 並べるcanvasを取ってくる
        //player = GameObject.FindGameObjectWithTag("canvas"); //canvasタグが2個あったら詰む書き方
        player = GameObject.FindGameObjectWithTag("Player");
        // Screenのサイズに合わせる為にボードの大きさをスクリーンの大きさとして決める
        board = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        // ボードの大きさを縦横の数で割って一つの大きさを出す。ScreenToWorldPointしてるので大きさが変わると嫌だなーと絶対値
        buttonX = System.Math.Abs(board.x / yoko);
        buttonY = System.Math.Abs(board.y / tate);
        buttonArray = new GameObject[yoko, tate];
        hidarihasi = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)); // 左下になる
        hidarihasi.y *= -1f; // ので反転して左上に

        for (int i = 0; i < yoko; i++)
        {
            for (int j = 0; j < tate; j++)
            {
                buttonArray[i, j] = CreateButton(buttonX * i, buttonY * j);

            }
        }

        tapButton = new GameObject[necessaryTap];

        //Debug.Log(hidarihasi);
        //Debug.Log(board.x);

    }

    // Update is called once per frame
    void Update()
    {

        // タッチされているかチェック(スマホ)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {

                //TapTouch(touch.position.x, touch.position.y);
                TapTouch(touch.position);
            }

        }

        // マウスを離した時
        if (Input.GetMouseButtonUp(0))
        {
            //TapTouch(Input.mousePosition.x, Input.mousePosition.y);
            TapTouch(Input.mousePosition);

        }

        timeCheck();

    }

    GameObject CreateButton(float X, float Y)
    {
        // ボタンを作る
        // 確かXY働いてないがレイアウトでカバーしてるというギャグ

        GameObject ret = Instantiate(prefabButton);

        ret.transform.SetParent(player.transform); // canvasを親にする

        float posBX = hidarihasi.x + X;
        float posBY = hidarihasi.y + Y;
        ret.transform.position = new Vector3(posBX, posBY, 100);
        ret.transform.localScale = new Vector3(1, 1, 1);
        //ret.transform.Translate(X, Y, 0, Space.World);
        //Debug.Log(ret.transform.position);   

        return ret;
    }

    //void TapTouch(float x, float y)
    void TapTouch(Vector3 pos)
    {
        // そういえばこれcolliderアタッチしてないと動かないよな……。
        // そしてボタンだからonclickで良いというオチ(=これは使う必要性が無いので使っておりません)

        Vector2 wpos = Camera.main.ScreenToWorldPoint(pos);
        RaycastHit2D hit = Physics2D.Raycast(wpos, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }

    }

    public void clickAfter(GameObject clickButton)
    {
        // クリック処理、基本的にボタンプレハブのonclickから呼び出される
        //Debug.Log("クリック");

        /*
        if(clickButton.GetComponent<buttonTap>().bomb > 0)
        {
            // ボムであった場合0以外になる 1:縦横の数値増加 2:タイム増加 3:コイン増加
            int bom = clickButton.GetComponent<buttonTap>().bomb;
            if (bom == 1)
            {

            } else if (bom == 2)
            {

            } else if(bom == 3)
            {

            }

            clickButton.GetComponent<buttonTap>().numberStart();
            tapButton = new GameObject[necessaryTap];
            clickNumber = 0;
            return;
        }
        */

        for (int i = 0; i < tapButton.Length; i++)
        {
            if (tapButton[i] == clickButton)
            {
                // 同じ物をクリックした場合、クリック状況を初期化
                // ……鬱陶しいだけだな？ 止めとこ
                /*
                tapButton = new GameObject[necessaryTap];
                tapButton[0] = clickButton;
                */
                break;
            }

            if (tapButton[i] == null)
            {

                if (i == 0)
                {
                    // 1個目の時ナンバーをセットする
                    clickNumber = clickButton.GetComponent<buttonTap>().number;
                    tapButton[i] = clickButton;

                }
                else
                {
                    // 2個目以降の時

                    if (clickNumber != clickButton.GetComponent<buttonTap>().number)
                    {
                        // 別のナンバーである場合初期化

                        tapButton = new GameObject[necessaryTap];
                        tapButton[0] = clickButton;
                        clickNumber = clickButton.GetComponent<buttonTap>().number;

                    }
                    else
                    {

                        // 同じナンバーの場合でオブジェクトがどれとも被っていない時追加
                        tapButton[i] = clickButton;

                    }
                }

                break;
            }
        }

        if (tapButton[tapButton.Length - 1] != null)
        {
            for (int i = 0; i < tapButton.Length; i++)
            {


                if (i == tapButton.Length - 1)
                {
                    tapButton[i].GetComponent<buttonTap>().numberUp();
                }
                else
                {
                    tapButton[i].GetComponent<buttonTap>().numberStart();
                }
            }
            tapButton = new GameObject[necessaryTap];
            clickNumber = 0;
        }
    }

    public void scorePlus(int plus)
    {
        score += plus;
        scoreText.GetComponent<Text>().text = " score:" + score.ToString();

    }

    public void timeCheck()
    {
        leftTime += Time.deltaTime;
        //time--;
        time = (int)leftTime;
        timeText.GetComponent<Text>().text = " time:" + time.ToString();
        if (time >= maxTime)
        {
            gameOver();
        }
    }

    public void gameOver()
    {
        if (score > PlayerPrefs.GetInt("maxScore", 0))
        {
            PlayerPrefs.SetInt("maxScore", score);
        }
        PlayerPrefs.SetInt("score", score); // 今回の得点も保存

        //SceneManager.LoadScene("start");
        SceneManager.LoadScene("tokutenn");
    }
}
