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
    private bool isCoolDown = false;
    [SerializeField] private float touchCoolTime;
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
        KeyCtrl();
        FusionCheck();
        LevelUp();
    }
    
    private void FusionCheck()
    {
        if (level < allSprites.Count)
        {
            if (allSprites[level].canFusion && allSprites[level - 1].canFusion)
            {
                canFusion = true;
            }
            else
            {
                canFusion = false;
            }
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
        if (Input.GetMouseButtonDown(0))
        {
            if (isCoolDown) return;
            Vector3 tapPosition = Input.mousePosition;
            tapPosition.z = 10;
            if (canFusion)
            {
                if (level < allSprites.Count)
                {
                    level++;                   
                }
                else
                {
                    allSprites[level].isFusion = true;
                    Debug.Log("stage clear");
                }
                canFusion = false;
                GameObject tapObj = Instantiate<GameObject>(ResourcesMng.ResourcesLoad("Tap_yes"), Vector3.zero,Quaternion.identity);
                tapObj.transform.position = Camera.main.ScreenToWorldPoint(tapPosition);
            }

            else
            {
                GameObject tapObj = Instantiate<GameObject>(ResourcesMng.ResourcesLoad("Tap_no"), Vector3.zero, Quaternion.identity);
                tapObj.transform.position = Camera.main.ScreenToWorldPoint(tapPosition);
                Debug.Log(tapObj.transform.position);
            }
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        isCoolDown = true;
        yield return new WaitForSeconds(touchCoolTime);
        isCoolDown = false;
        yield break;
    }

    private void LevelUp()
    {
        if (preLv != level)
        {
            allSprites[preLv-1].isFusion = true;
            allSprites[preLv].isFusion = true;
            //allSprites[preLv - 1].canFusion = false;
            //allSprites[preLv].canFusion = false;
            if(preLv < cameras.Count)
            {
                cameras[preLv - 1].gameObject.SetActive(false);
                cameras[preLv].gameObject.SetActive(true);
                allSprites[level].isRotate = true;
            }
            preLv = level;
        }
    }
}
