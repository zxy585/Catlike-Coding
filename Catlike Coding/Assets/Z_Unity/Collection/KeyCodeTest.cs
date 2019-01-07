using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCodeTest : MonoBehaviour
{
    public Text t01;

    void OnGUI()
    {
        //if (Input.anyKeyDown) 
        //{
        //    e = Event.KeyboardEvent;
        //    if (e.isKey)
        //    {
        //        currentKey = e.keyCode;
        //        Debug.LogError("Current Key is : " + currentKey.ToString());
        //    }
        //}

        //循环遍历输出
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Debug.LogError("Current Key is : " + keyCode.ToString());
                    t01.text = "Current Key is : " + keyCode.ToString();
                }
            }
        }
    }
}
