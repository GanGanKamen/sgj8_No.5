using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private float scoreChangeSpeed;
    private float preScore;
    public int score;
    [SerializeField] private int goodPoint;
    [SerializeField] private int missPoint;
    // Start is called before the first frame update
    void Start()
    {
        preScore = score;
    }

    public void Good()
    {
        score += goodPoint;
    }

    public void Bad()
    {
        score += missPoint;
    }

    private void ScoreChange()
    {
        if(preScore < score)
        {
            preScore+=Time.deltaTime * scoreChangeSpeed;
        }
        else
        {
            preScore = score;
        }

        scoreText.text = "Score : " + ((int)preScore).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreChange();
    }
}
