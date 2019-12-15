using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    public Text scoreText;
    [SerializeField] private float scoreChangeSpeed;
    private float preScore;
    public int score;
    [SerializeField] private int goodPoint;
    [SerializeField] private int missPoint;

    public int fadeActionNum = 0;
    public Image fadeImage;
    private float alpha = 0;
    private float count = 0;
    private bool trigger1 = false;
    private bool trigger2 = false;
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

        fadeImage.color = new Color(50, 50, 50, alpha);
        FadeAction();
        if (trigger2 != trigger1)
        {
            SceneManager.LoadScene(nextSceneName);
            trigger2 = trigger1;
        }
    }

    private void FadeAction()
    {
        switch (fadeActionNum)
        {
            case 1:
                if (alpha < 1)
                {
                    alpha += Time.deltaTime;
                }
                else
                {
                    alpha = 1;
                    fadeActionNum = 2;
                }
                break;
            case 2:
                if (count < 2)
                {
                    count += Time.deltaTime;
                }
                else
                {
                    fadeActionNum = 3;
                    count = 0;
                    trigger1 = true;
                }
                break;
        }
    }
}
