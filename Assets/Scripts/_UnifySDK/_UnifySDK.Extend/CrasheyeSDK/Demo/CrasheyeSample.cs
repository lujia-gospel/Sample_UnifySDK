using System;
using System.Collections;
using System.Collections.Generic;
using UnifySDK;
using UnityEngine;
using UnityEngine.Diagnostics;

public class CrasheyeSample : MonoBehaviour
{
    private GUIStyle style = new GUIStyle();
    private string exception;
    private List<int> numbers = new List<int>();

    void TestNullReference()
    {
        style.fontSize = 22;
        // try
        // {
            GameObject go = null;
            go.name = "";
        // }
        // catch (Exception e)
        // {
        //     Debug.LogError($"{e}");
        //     exception += $"{e}\n";
        // }
    }

    void TestOutOfRange()
    {
        // try
        // {
            numbers[1] = 1;
        // }
        // catch (Exception e)
        // {
        //     Debug.LogError($"{e}");
        //     exception += $"{e}\n";
        // }
    }


    void OnGUI()
    {
        GUILayout.Space(150);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("TestNullReference", GUILayout.Width(200), GUILayout.Height(60)))
        {
            TestNullReference();
        }
        GUILayout.Space(50);
        if (GUILayout.Button("OutOfRange", GUILayout.Width(200), GUILayout.Height(60)))
        {
            TestOutOfRange();
        }
        if (GUILayout.Button("AccessViolation", GUILayout.Width(200), GUILayout.Height(60)))
        {
            Utils.ForceCrash(ForcedCrashCategory.AccessViolation);
        }
        if (GUILayout.Button("Abort", GUILayout.Width(200), GUILayout.Height(60)))
        {
            Utils.ForceCrash(ForcedCrashCategory.Abort);
        }
        
        if (GUILayout.Button("FatalError", GUILayout.Width(200), GUILayout.Height(60)))
        {
            Utils.ForceCrash(ForcedCrashCategory.FatalError);
        }
        if (GUILayout.Button("MonoAbort", GUILayout.Width(200), GUILayout.Height(60)))
        {
            Utils.ForceCrash(ForcedCrashCategory.MonoAbort);
        }
        
        if (GUILayout.Button("PureVirtualFunction", GUILayout.Width(200), GUILayout.Height(60)))
        {
            Utils.ForceCrash(ForcedCrashCategory.PureVirtualFunction);
        }
        GUILayout.EndHorizontal();
        
        if (!string.IsNullOrEmpty(exception))
        {
            GUI.Label(new Rect(0, 100, Screen.width, Screen.height), $"{exception}", style);
        }


    }
}
