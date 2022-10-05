using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

    public delegate void EmitHandler(Transform target);
    
    public EmitHandler OnEmitEvent; //委托
    
    public static Radio Instance; //static单例模式

    void Awake()
    {
        Instance = this;
    }
}
