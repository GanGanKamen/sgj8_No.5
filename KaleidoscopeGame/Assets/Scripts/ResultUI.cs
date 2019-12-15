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
    private bool trigger;

    [SerializeField] private GameObject successEffect;
    // Start is called before the first frame update
    void Start()
    {
        mainUI.scoreText.gameObject.SetActive(false);
        trigger = false;
        successEffect.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger != isOver)
        {
            trigger = true;
            GetComponent<AudioSource>().PlayOneShot(ResourcesMng.AudioClipLoad("Voice/TapT1576374544351"));
        }

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
