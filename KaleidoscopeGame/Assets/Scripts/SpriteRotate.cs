using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    public bool isRotate;
    [SerializeField][Range(-1,1)] private int rotateDirection;
    [SerializeField] [Range(-1, 1)] private int fusionDirection;
    public float rotateSpeed;
    public float fusionSpeed;
    //[SerializeField] private List<float> fusionAngles;
    [SerializeField] public float preFusionAngle;
    [SerializeField] public float nextFusionAngle;
    public bool canFusion = false;
    public bool isFusion = false;

    public float startAngle;
    public float errorAngle;
    [SerializeField] public float fusionLimit;
    [SerializeField] private float debug;
    [SerializeField] private float angleZ;
    // Start is called before the first frame update
    void Start()
    {
        startAngle = transform.rotation.eulerAngles.z;
        errorAngle = ErrorAngle();
    }

    // Update is called once per frame
    void Update()
    {
        angleZ = transform.rotation.eulerAngles.z;
        RotateMove();
        FusionAction();
    }

    public float ErrorAngle()
    {
        return transform.rotation.eulerAngles.z % nextFusionAngle;
    }

    private void RotateMove()
    {
        if (isRotate && isFusion == false)
        {
            transform.Rotate(0, 0, rotateDirection * rotateSpeed * Time.deltaTime);
            //debug = Mathf.Abs((transform.rotation.eulerAngles.z + errorAngle) % fusionAngle);
            /*
            var canFusionCount = 0;
            for(int i = 0; i < fusionAngles.Count; i++)
            {
                if(Mathf.Abs((transform.rotation.eulerAngles.z - fusionAngles[i])) < fusionLimit)
                {
                    canFusionCount++;
                }
            }
            if(canFusionCount > 0)
            {
                canFusion = true;
            }
            else
            {
                canFusion = false;
            }
            */
        }
    }

    private void FusionAction()
    {
        if(isRotate && isFusion)
        {
            transform.Rotate(0, 0, fusionDirection * fusionSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            Debug.Log("collide");
            canFusion = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            canFusion = false;
        }
    }
}
