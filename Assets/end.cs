using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class end : MonoBehaviour {

    public int score;
    public GameObject scoreText;
    public int maxscore;

    // Use this for initialization
    private void Start () {

        // スコアのテキストオブジェクトを取ってくる
        scoreText = GameObject.FindGameObjectWithTag("Score");

        maxscore = PlayerPrefs.GetInt("maxScore", 0); // セーブされた値、orセーブが無い時は0
        score = PlayerPrefs.GetInt("score", 0); // セーブされた値、orセーブが無い時は0  

        // 文字を初期化
        scoreText.GetComponent<Text>().text = " MaxScore:" + maxscore.ToString() + "\n 現在のスコア:" + score.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClick()
    {
        SceneManager.LoadScene("start");

    }
}
