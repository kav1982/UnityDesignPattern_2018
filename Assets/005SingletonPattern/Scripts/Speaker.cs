using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{

    public string SpeakerName;
    public float GapTime = 1;
    private int index = 1;

    void Start()
    {
        InvokeRepeating("Speak", 0, GapTime);
        //InvokeRepeating: 在time（参数2）秒内调用方法methodName（参数1），然后重复每repeatRate（参数3）秒
    }

    void Speak()
    {
        string content = "I'm " + SpeakerName + ". (Index : " + index + ")";
        Debug.Log(content);
        TimeLogger.Instance.WhiteLog(content);
        index++;
    }
}
