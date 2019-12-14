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
    private bool isFusionCheckA = false;
    private bool isFusionCheckB = false;
    bool spriteAcanFusion = false;
    bool spriteBcanFusion = false;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        preLv = level;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(spriteAcanFusion && spriteBcanFusion)
        {
            canFusion = true;
        }
        else
        {
            canFusion = false;
        }*/
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

        var errorAngle = Mathf.Abs(allSprites[level].errorAngle - allSprites[level - 1].errorAngle);
        var canFusionCount = 0;
        /*
        if((allSprites[level - 1].transform.rotation.eulerAngles.z + errorAngle) 
            %allSprites[level -1].nextFusionAngle < allSprites[level - 1].fusionLimit)
        {
            canFusionCount++;
            Debug.Log("a");
            //StartCoroutine(CanFusionCheckProcessA());
        }
        */
        if((allSprites[level].transform.rotation.eulerAngles.z - errorAngle)
            % allSprites[level].nextFusionAngle < allSprites[level].fusionLimit)
        {
            canFusionCount++;
            Debug.Log("b");
            //StartCoroutine(CanFusionCheckProcessB());
        }
        if(canFusionCount > 0)
        {
            canFusion = true;
        }
        else
        {
            canFusion = false;
        }
    }

    private IEnumerator CanFusionCheckProcessA()
    {
        if (isFusionCheckA) yield break;
        isFusionCheckA = true;
        Debug.Log("FusionCheckA");
        spriteAcanFusion = true;
        yield return new WaitForSeconds(0.1f);
        spriteAcanFusion = false;
        isFusionCheckA = false;
        yield break;

    }

    private IEnumerator CanFusionCheckProcessB()
    {
        if (isFusionCheckB) yield break;
        isFusionCheckB = true;
        Debug.Log("FusionCheckB");
        spriteBcanFusion = true;
        yield return new WaitForSeconds(0.1f);
        spriteBcanFusion = false;
        isFusionCheckB = false;
        yield break;

    }

    private void KeyCtrl()
    {
        if (Input.GetMouseButtonDown(0) && canFusion)
        {
            level++;
            canFusion = false;
        }
    }

    private void LevelUp()
    {
        if (preLv != level)
        {
            if (preLv != 1) allSprites[preLv-1].isRotate = false;
            allSprites[preLv].isRotate = false;
            allSprites[preLv].transform.parent = allSprites[0].transform;
            allSprites[preLv].errorAngle = allSprites[preLv].ErrorAngle();
            allSprites[level].errorAngle = allSprites[preLv].ErrorAngle();
            //allSprites[preLv - 1].rotateSpeed = allSprites[preLv].rotateSpeed;
            cameras[preLv - 1].gameObject.SetActive(false);
            cameras[preLv].gameObject.SetActive(true);
            allSprites[level * 2 - 2].isRotate = true;
            preLv = level;
        }
    }
}
