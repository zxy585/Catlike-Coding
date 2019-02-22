using System;
using UnityEditor;
using UnityEngine;

public class InspectorTest : MonoBehaviour
{
    #region 编辑Inspector视图
    /// <summary>
    /// 只能输入 0-1的值
    /// </summary>
    [Range(0f, 1f)]
    public float tRange = 1f;

    /// <summary>
    /// 输入时的提示
    /// </summary>
    [Tooltip("Tooltip_test")]
    public float tTooltip = 1f;

    /// <summary>
    /// 标头
    /// </summary>
    [Header("Header_test")]
    public float tHeader = 1f;

    /// <summary>
    /// 距离上一行50px
    /// </summary>
    [Space(50)]
    public float tSpace = 1f;

    /// <summary>
    /// 隐藏该属性(依然会被实例化)
    /// </summary>
    [HideInInspector]
    public float tHideInInspector = 1f;

    #endregion

    #region 编辑MonoBehaviour功能
    /// <summary>
    /// 在标题栏Component中添加（"Duan/AddComponentMenu_test"）层级。
    /// 点击将（Test）脚本绑定到当前选中的gameobject上。
    ///（Test）脚本名必须与文件名一致,(单独的Class文件)。
    /// </summary>
    //[AddComponentMenu("Duan/AddComponentMenu_test")]
    //public class Test : MonoBehaviour{}

    /// <summary>
    /// 在辑模式运行Update、FixedUpdate和OnGUI。
    /// </summary>
    //[ExecuteInEditMode]
    //public class Test : MonoBehaviour{}

    /// <summary>
    /// 强制要求该脚本的gameobject必须同时绑定了Rigidbody组件，如果没有则立即添加。
    /// </summary>
    //[RequireComponent(typeof(Rigidbody))]
    //public class Test : MonoBehaviour{}

    /// <summary>
    /// 给当前脚本添加右键(或小齿轮)选项
    /// 点击调用该方法。
    /// </summary>
    [ContextMenu("ContextMenu Test")]
    public void mContextMenu()
    {
        Debug.Log("ContextMenu Test Log");
    }

    /// <summary>
    /// 在标题栏中添加（"Duan/MenuItem"）层级。
    /// 点击调用该方法。
    /// 该方法必须是static的。
    /// </summary>
    [MenuItem("Duan/MenuItem")]
    public static void tMenuItem()
    {
        Debug.Log("MenuItem Test Log");
    }
    #endregion

    #region 编辑属性
    /// <summary>
    /// 标记一个变量或方法不会被序列化
    /// </summary>
    [NonSerialized]
    public float tNonSerialized = 1f;

    /// <summary>
    /// 该类可以被序列化 (序列化就是把内存中对象以一种可以保存的形式保存起来。 )
    /// </summary>
    [Serializable]
    public class Serializable { }

    /// <summary>
    /// 强制序列化属性（Unity只序列化Public属性。序列化Private添加[SerializeField]。）
    /// </summary>
    [SerializeField]
    private bool tSerializeField = true;
    #endregion

    //在程序最开始执行
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void DoSomething()
    {
        Debug.Log("It's the start of the game");
    }
}
