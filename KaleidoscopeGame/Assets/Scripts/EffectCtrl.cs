using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var time = GetComponent<ParticleSystem>().duration;
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
