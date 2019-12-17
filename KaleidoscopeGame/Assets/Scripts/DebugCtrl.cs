using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCtrl : MonoBehaviour
{
    [SerializeField] private NewScopeSystem scopeSystem;
    [SerializeField] private float[] rotZ;
    [SerializeField] private float remainderAngleA;
    [SerializeField] private float remainderAngleB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < rotZ.Length; i++)
        {
            rotZ[i] = scopeSystem.allSprites[i].transform.localRotation.eulerAngles.z;
        }

        remainderAngleA = (scopeSystem.allSprites[scopeSystem.level-1].transform.localRotation.eulerAngles.z + 360f) %
            scopeSystem.allSprites[scopeSystem.level - 1].nextFusionAngle;
        remainderAngleB = (scopeSystem.allSprites[scopeSystem.level].transform.localRotation.eulerAngles.z + 360f) %
            scopeSystem.allSprites[scopeSystem.level].preFusionAngle;
    }
}
