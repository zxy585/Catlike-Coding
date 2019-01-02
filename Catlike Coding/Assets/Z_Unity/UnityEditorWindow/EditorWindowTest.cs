using UnityEngine;
using UnityEditor;
using System.Collections;

//typeof(编辑器类名),继承EditorWindow
[CustomEditor(typeof(EditorWindowTest))]
public class EditorWindowTest : EditorWindow
{
    //通过MenuItem按钮来创建这样的一个对话框
    [MenuItem("MyTools/EditorWindowTest")]
    public static void ConfigDialog()
    {
        //GetWindow创建
        EditorWindow.GetWindow(typeof(EditorWindowTest));
    }

    public UnityEngine.Object go = null;

    //对话框中的各种内容通过OnGUI函数来设置
    void OnGUI()
    {
        //Label
        GUILayout.Label("Label Test", EditorStyles.boldLabel);
        //通过EditorGUILayout.ObjectField可以接受Object类型的参数进行相关操作
        go = EditorGUILayout.ObjectField(go, typeof(UnityEngine.Object), true);
        //Button
        if (GUILayout.Button("Button Test"))
        {
            Debug.Log(go.name);
        }
    }

}
