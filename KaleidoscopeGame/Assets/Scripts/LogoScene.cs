using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour
{
    public string nextSceneName;
    public int fadeActionNum = 0;
    public Image logoImage;
    private float alpha = 0;
    private float count = 0;
    private bool trigger1 = false;
    private bool trigger2 = false;
    // Start is called before the first frame update
    void Start()
    {
        fadeActionNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        logoImage.color = new Color(255, 255, 255, alpha);
        FadeAction();
        if(trigger2 != trigger1)
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
                if(alpha < 1)
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
                if(count < 2)
                {
                    count += Time.deltaTime;
                }
                else
                {
                    fadeActionNum = 3;
                    count = 0;
                }
                break;
            case 3:
                if(alpha > 0)
                {
                    alpha -= Time.deltaTime;
                }
                else
                {
                    alpha = 0;
                    trigger1 = true;
                }
                break;
        }
    }
}
