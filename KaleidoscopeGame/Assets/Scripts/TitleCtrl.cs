using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleCtrl : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed;
    private bool startGame = false;
    [SerializeField]private int fadeNum = 0;
    private float alpha = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fadeImage.color = new Color(255, 255, 255, alpha);
        FadeAction();
        KeyCtrl();
    }

    private void KeyCtrl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouseDown");
            if (startGame) return;
            StartCoroutine(StartGame());
        }
    }

    private void FadeAction()
    {
        switch (fadeNum)
        {
            case 1:
                if(alpha < 1)
                {
                    alpha += Time.deltaTime * fadeSpeed;
                }
                else
                {
                    alpha = 1;
                    fadeNum = 0;
                }
                break;
        }
    }
    
    private IEnumerator StartGame()
    {
        if (startGame) yield break;
        startGame = true;
        fadeNum = 1;
        while(fadeNum != 0)
        {
            yield return null;
        }
        SceneManager.LoadSceneAsync(nextSceneName);
  
    }
}
