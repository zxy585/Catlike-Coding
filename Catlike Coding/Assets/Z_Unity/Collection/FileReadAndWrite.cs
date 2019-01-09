using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class FileReadAndWrite : MonoBehaviour
{
    Transform mainCamera;
    string message;
    string path = "";
    private void Awake()
    {
        mainCamera = Camera.main.transform;
        path = Application.dataPath + "/ZXY/data.txt";
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        message = "Camera: 位置：" + "+X:" + mainCamera.position.x + "+Y:" + mainCamera.position.y + "+Z:" + mainCamera.position.z + "+旋转：" + "+X:" + mainCamera.rotation.x + "+Y:" + mainCamera.rotation.y + "+Z:" + mainCamera.rotation.z;
        WriteCameraData(message);
        if (Input.GetKeyDown(KeyCode.A))
        {
            WriteCameraData(message);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ClearData();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ReadData();
        }
    }

    StreamReader reader;
    StreamWriter writer;
    string str;
    void WriteCameraData(string message)
    {
        FileInfo file = new FileInfo(path);
        if (!file.Exists)
        {
            writer = file.CreateText();
        }
        else
        {
            writer = file.AppendText();
        }
        writer.WriteLine(message);
        writer.Flush();
        writer.Dispose();
        writer.Close();
    }

    void ReadData()
    {
        reader = new StreamReader(path, Encoding.UTF8);
        str = reader.ReadToEnd();
        reader.Dispose();
        reader.Close();
        Debug.Log(str);
    }

    [ContextMenu("Clear Txt Data")]
    void ClearData()
    {
        writer = new StreamWriter(Application.dataPath + "/ZXY/data.txt");
        writer.Write("");
        writer.Close();
    }
}
