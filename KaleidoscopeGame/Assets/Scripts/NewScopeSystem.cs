using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScopeSystem : MonoBehaviour
{
    public List<SpriteRotate> allSprites;
    public List<GameObject> cameras;
    public GameObject finalCamera;
    public int level;
    private int preLv;
    public bool canFusion = false;
    private bool isFusionCheckA = false;
    private bool isFusionCheckB = false;
    private bool isCoolDown = false;
    [SerializeField] private float touchCoolTime;
    [SerializeField] private MainUI mainUI;
    [SerializeField] private GameObject flashObj;
    [SerializeField] private float errorLimit;
    public GameObject resultUI;

    public int maxLevel;

    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in this.transform)
        {
            if(child.GetComponent<SpriteRotate>() != null)
            {
                SpriteRotate sprite = child.GetComponent<SpriteRotate>();
                allSprites.Add(sprite);
            }
                
        }
        level = 1;
        preLv = level;
        maxLevel = allSprites.Count -1;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        FusionCheck();
        KeyCtrl();
        LevelUp();
    }

    private void FusionCheck()
    {
        if(level < maxLevel)
        {
            var remainderAngle1 = (allSprites[level -1].transform.localRotation.eulerAngles.z + 360f) %
            allSprites[level-1].nextFusionAngle;
            var remainderAngle2 = (allSprites[level].transform.localRotation.eulerAngles.z + 360f) %
                allSprites[level].preFusionAngle;
            if(Mathf.Abs(remainderAngle1 - remainderAngle2) < errorLimit)
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
        
    }

    private void KeyCtrl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isCoolDown) return;
            Vector3 tapPosition = Input.mousePosition;
            tapPosition.z = 10;
            level++;
            flashObj.SetActive(false);
            if (canFusion)
            {
                GameObject tapObj = Instantiate<GameObject>(ResourcesMng.ResourcesLoad("Tap_yes"), Vector3.zero, Quaternion.identity);
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
            if (level <= maxLevel)
            {
                allSprites[preLv - 1].isFusion = true;
                allSprites[preLv].isFusion = true;
                if (preLv < cameras.Count)
                {
                    cameras[preLv - 1].gameObject.SetActive(false);
                    cameras[preLv].gameObject.SetActive(true);

                }
                allSprites[level].isRotate = true;

            }
            else if(level > maxLevel)
            {
                allSprites[level -1].isFusion = true;
                GameClear();
            }
            flashObj.transform.localScale = new Vector3(level, level, level);
            preLv = level;
        }
    }

    private void GameClear()
    {
        var finalTime = Time.time - startTime;
        Debug.Log("timeProcess" + finalTime);
        mainUI.score += (int)(10000 / finalTime);
        for(int i = 0; i < allSprites.Count; i++)
        {
            allSprites[i].transform.parent = null;
        }
        finalCamera.SetActive(true);
        resultUI.SetActive(true);
        Destroy(gameObject);
    }
}
