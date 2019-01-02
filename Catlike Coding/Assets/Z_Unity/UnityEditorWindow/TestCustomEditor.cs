using UnityEngine;
using UnityEditor;
using System.Collections;
 
//typeof中参数即为我们需要定义Inspector面板的组件
[CustomEditor(typeof(CustomEditorTest))]
public class TestCustomEditor : Editor {
 
    CustomEditorTest script;
 
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //将target转化为我们需要的脚本
        script = target as CustomEditorTest;
        //增加一个按钮
        if (GUILayout.Button("Test Button"))
        {
            //可以直接访问CustomEditorTest类的内容
            script.Test(script.num);
        }
    }
}
