using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandShort : MonoBehaviour
{
    const float UNIT = - 360f / 60f / 12f;

    void Start()
    {
    }

    public void minutes_set(int min, int hour)
    {
        var angle = transform.eulerAngles;
        angle.z = UNIT * (min + hour * 60);
        transform.eulerAngles = angle;
        //Debug.Log(angle.z);
    }

    //void Update()
    //{        
    //}

}
