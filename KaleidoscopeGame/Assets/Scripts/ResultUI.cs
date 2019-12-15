using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private MainUI mainUI;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private float scoreChangeSpeed;
    [SerializeField] private GameObject nextText;
    public float score = 0;
    public bool isOver;

   
    // Start is called before the first frame update
    void Start()
    {
        mainUI.scoreText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(score < mainUI.score)
        {
            score += Time.deltaTime * scoreChangeSpeed;
        }
        else
        {
            score = mainUI.score;
            isOver = true;
            nextText.SetActive(true);
        }
        finalScoreText.text = "Score : " + ((int)score).ToString();

        if(Input.GetMouseButtonDown(0) && isOver && mainUI.fadeActionNum == 0)
        {
            mainUI.fadeActionNum = 1;
        }

        
    }

    
}
