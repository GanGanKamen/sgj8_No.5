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
    public bool isFusion = false;

    [SerializeField] public float fusionLimit;

    public bool changeCamera;
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
        }
    }

    private void FusionAction()
    {
        if(isRotate && isFusion)
        {
            transform.Rotate(0, 0, fusionDirection * fusionSpeed * Time.deltaTime);
        }
    }
}
