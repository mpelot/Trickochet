using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;
    public Ball ball;
    public Player player;
    public GameObject startingPosition;
    private int s = 0;
    private int hs = 0;

    public void AddScore(int scoreAdded) {
        s += scoreAdded;
        string stringScore = s + "";
        while (stringScore.Length < 6)
            stringScore = "0" + stringScore;
        score.text = stringScore;
    }

    public void Begin() {
        player.canMove = true;
    }

    public void End() {
        ball.transform.position = startingPosition.transform.position;
        ball.Reset();
        if (s > hs) {
            string stringScore = s + "";
            while (stringScore.Length < 6)
                stringScore = "0" + stringScore;
            highscore.text = stringScore;
            hs = s;
        }
        s = 0;
        score.text = "000000";
    }
}
