using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startgamenn : MonoBehaviour {

    public int score;
    public GameObject scoreText;

    // Use this for initialization
    void Start () {

        // スコアのテキストオブジェクトを取ってくる
        scoreText = GameObject.FindGameObjectWithTag("Score");

        score = PlayerPrefs.GetInt("maxScore", 0); // セーブされた値、orセーブが無い時は0
        // 文字を初期化
        scoreText.GetComponent<Text>().text = " MaxScore:" + score.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClick()
    {
        SceneManager.LoadScene("index");

    }
}
