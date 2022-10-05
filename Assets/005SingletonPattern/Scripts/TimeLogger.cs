using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TimeLogger : MonoBehaviour
{

    public static TimeLogger Instance;
    
    //StreamWriter: 实现一个TextWriter，用于以特定编码向流写入字符
    private StreamWriter mSW; 

    void Awake()
    {
        Instance = this;
        LoggerInit(Application.dataPath + "\\005SingletonPattern\\Log.txt");
        //dataPath:包含游戏数据文件夹的路径(只读)
    }

    void LoggerInit(string path)
    {
        if (mSW == null)
        {
            //使用默认编码和缓冲区大小为指定文件初始化StreamWriter类的新实例。
            mSW = new StreamWriter(path); 
        }
    }

    public void WhiteLog(string info)
    {
        mSW.Write(DateTime.Now + ": " + info + "\n");
    }

    private void OnEnable()
    {
        LoggerInit(Application.dataPath + "\\005SingletonPattern\\Log.txt");
    }

    private void OnDisable()
    {
        mSW.Close();
    }
}
