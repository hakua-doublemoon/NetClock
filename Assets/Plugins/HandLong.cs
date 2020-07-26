using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLong : MonoBehaviour
{
    const float UNIT = - 360f / 60f;

    void Start()
    {
    }

    public void minutes_set(int min)
    {
        var angle = transform.eulerAngles;
        angle.z = UNIT * min;
        transform.eulerAngles = angle;
        //Debug.Log(angle.z);
    }

    //void Update()
    //{        
    //}

}
