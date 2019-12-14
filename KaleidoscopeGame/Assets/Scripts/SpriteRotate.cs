using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    public bool isRotate;
    [SerializeField][Range(-1,1)] private int rotateDirection;
    [SerializeField] [Range(-1, 1)] private int fusionDirection;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float fusionAngle;
    [SerializeField] private List<float> fusionAngles;
    public bool canFusion = false;
    public bool isFusion = false;
    [SerializeField] private float fusionLimit;
    [SerializeField] private float debug;
    [SerializeField] private float angleZ;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateMove();
        FusionAction();
    }

    private void RotateMove()
    {
        if (isRotate && isFusion == false)
        {
            transform.Rotate(0, 0, rotateDirection * rotateSpeed * Time.deltaTime);
            //debug = Mathf.Abs((transform.rotation.eulerAngles.z + errorAngle) % fusionAngle);
            angleZ = transform.rotation.eulerAngles.z;
            /*
            if (Mathf.Abs((transform.rotation.eulerAngles.z + errorAngle) % fusionAngle) - errorAngle2 < fusionLimit)
            {
                canFusion = true;
            }
            else
            {
                canFusion = false;
            }*/
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
        }
    }

    private void FusionAction()
    {
        if(isRotate && isFusion)
        {
            transform.Rotate(0, 0, fusionDirection * rotateSpeed * Time.deltaTime);
        }
    }
}
