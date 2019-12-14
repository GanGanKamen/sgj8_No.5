using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScopeSystem : MonoBehaviour
{
    public List<SpriteRotate> allSprites;
    public List<CinemachineVirtualCamera> cameras;
    public int level;
    private int preLv;
    public bool canFusion = false;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        preLv = level;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCtrl();
        FusionCheck();
        LevelUp();
    }
    /*
    private void FusionCheck()
    {
        var canFusionCheckCount = 0;
        for (int i = 0; i < allSprites.Count; i++)
        {
            if (allSprites[i].canFusion && allSprites[i].isRotate && allSprites[i].isFusion == false)
            {
                canFusionCheckCount++;
            }
        }
        Debug.Log(canFusionCheckCount);
        if (canFusionCheckCount >= 2)
        {
            canFusion = true;
        }
        else
        {
            canFusion = false;
        }
    }
    */
    private void FusionCheck()
    {
        if(allSprites[level * 2 - 2].canFusion || allSprites[level * 2 - 1].canFusion)
        {
            canFusion = true;
        }
        else
        {
            canFusion = false;
        }
    }

    private void KeyCtrl()
    {
        if (Input.GetMouseButtonDown(0) && canFusion)
        {
            level++;
        }
    }

    private void LevelUp()
    {
        if (preLv != level)
        {
            allSprites[preLv * 2 - 2].isFusion = true;
            allSprites[preLv * 2 - 1].isFusion = true;
            cameras[level - 2].gameObject.SetActive(false);
            cameras[level - 1].gameObject.SetActive(true);
            allSprites[level * 2 - 2].isRotate = true;
            allSprites[level * 2 - 1].isRotate = true;
            preLv = level;
        }
    }
}
