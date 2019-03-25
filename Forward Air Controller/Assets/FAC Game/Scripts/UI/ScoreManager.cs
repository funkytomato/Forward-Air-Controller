using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text textComponent;


    static int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int newScoreValue)
    {
        Score += newScoreValue;
        UpdateScore();
    }

    public float GetScore()
    {
        return Score;
    }

    void UpdateScore()
    {
        textComponent.text = Score.ToString();
    }


}
