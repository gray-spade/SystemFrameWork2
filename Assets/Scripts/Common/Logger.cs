using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class Logger 
{
    [Conditional("DEV_VER")]//조건부 컴파일 심볼
    public static void Log(string msg) {
        UnityEngine.Debug.LogFormat("[{0} {1}]", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }

    [Conditional("DEV_VER")]
    public static void LogWarning(string msg)//워밍 로그함수
    {
        UnityEngine.Debug.LogWarningFormat("[{0} {1}]", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }
    public static void LogError(string msg)//에러 로그함수
    {
        UnityEngine.Debug.LogErrorFormat("[{0} {1}]", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }

}
