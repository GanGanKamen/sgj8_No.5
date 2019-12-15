using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScopeSystem : MonoBehaviour
{
    public List<SpriteRotate> allSprites;
    public List<CinemachineVirtualCamera> cameras;
    public GameObject finalCamera;
    public int level;
    private int preLv;
    public bool canFusion = false;
    private bool isFusionCheckA = false;
    private bool isFusionCheckB = false;
    private bool isCoolDown = false;
    [SerializeField] private float touchCoolTime;
    bool spriteAcanFusion = false;
    bool spriteBcanFusion = false;
    [SerializeField] private MainUI mainUI;

    [SerializeField] private GameObject flashObj;

    public GameObject resultUI;

    private bool checkCanFusionTrigger;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        preLv = level;
        checkCanFusionTrigger = canFusion;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCtrl();
        FusionCheck();
        LevelUp();
        if(checkCanFusionTrigger != canFusion)
        {

            checkCanFusionTrigger = canFusion;
        }
    }
    
    private void FusionCheck()
    {
        if (level < allSprites.Count)
        {
            if (allSprites[level].canFusion && allSprites[level - 1].canFusion)
            {
                canFusion = true;
                flashObj.SetActive(true);
            }
            else
            {
                canFusion = false;
                flashObj.SetActive(false);
            }
        }
        else canFusion = true;
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
            if (level <= allSprites.Count)
            {
                level++;
            }
            flashObj.SetActive(false);
            if (canFusion)
            {
                GameObject tapObj = Instantiate<GameObject>(ResourcesMng.ResourcesLoad("Tap_yes"), Vector3.zero,Quaternion.identity);
                tapObj.transform.position = Camera.main.ScreenToWorldPoint(tapPosition);
                if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().PlayOneShot(ResourcesMng.AudioClipLoad("SE/good-job-se"));
                mainUI.Good();
                canFusion = false;
            }

            else
            {
                GameObject tapObj = Instantiate<GameObject>(ResourcesMng.ResourcesLoad("Tap_no"), Vector3.zero, Quaternion.identity);
                tapObj.transform.position = Camera.main.ScreenToWorldPoint(tapPosition);
                if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().PlayOneShot(ResourcesMng.AudioClipLoad("SE/miss"));
                mainUI.Bad();
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
           if(level < allSprites.Count)
            {
                allSprites[preLv - 1].isFusion = true;
                allSprites[preLv].isFusion = true;
                //allSprites[preLv - 1].canFusion = false;
                //allSprites[preLv].canFusion = false;
                if (preLv < cameras.Count)
                {
                    cameras[preLv - 1].gameObject.SetActive(false);
                    cameras[preLv].gameObject.SetActive(true);

                }
                allSprites[level].isRotate = true;

            }
            else if(level >= allSprites.Count)
            {
                allSprites[level -1].isFusion = true;
                Debug.Log("stage clear");
                GameClear();
            }
            flashObj.transform.localScale = new Vector3(level, level, level);
            preLv = level;
        }
    }

    private void GameClear()
    {
        allSprites[level -1].isFusion = true;
        finalCamera.SetActive(true);
        resultUI.SetActive(true);
        Destroy(gameObject);
    }
}
