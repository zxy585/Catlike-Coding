using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeviceCameraCall : MonoBehaviour {


    public string deviceName;
    //接收返回的图片数据
    WebCamTexture tex;

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 20, 100, 40), "start"))
    //    {
    //        // 调用摄像头
    //        StartCoroutine(OpenDeviceCamera());
    //    }
    //    if (GUI.Button(new Rect(10, 70, 100, 40), "Pause"))
    //    {
    //        //捕获照片
    //        tex.Pause();
    //        Debug.Log("调用Pause");
    //        StartCoroutine(SaveTexture("/mnt/sdcard/CameraAdjust" + Time.time + ".jpg"));
    //        //StartCoroutine(SaveTexture(Application.dataPath + "/Photoes/" + Time.time + ".jpg"));
    //    }
    //    if (GUI.Button(new Rect(10, 120, 100, 40), "stop"))
    //    {
    //        //停止捕获镜头
    //        tex.Stop();
    //        StopAllCoroutines();
    //    }

    //}

    public void OpenDeviceCameraFunc()
    {
        StartCoroutine(OpenDeviceCamera());
    }

    public void SaveTextureFunc()
    {
        StartCoroutine(SaveTexture("/mnt" + Time.time + ".jpg"));
    }

    public void CloseDeviceCameraFunc()
    {
        tex.Stop();
        StopAllCoroutines();
    }

    /// <summary>
    /// 捕获窗口位置
    /// </summary>
    public IEnumerator OpenDeviceCamera()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            tex = new WebCamTexture(deviceName, 300, 300, 12);
            tex.Play();
        }
    }

    /// <summary>
    /// 获取截图
    /// </summary>
    /// <returns>The texture.</returns>
    public IEnumerator SaveTexture(string path)
    {
        yield return new WaitForEndOfFrame();
        Texture2D t = new Texture2D(tex.width, tex.height);
        t.SetPixels(tex.GetPixels());
        t.Apply();
        byte[] byt = t.EncodeToPNG();
        File.WriteAllBytes(path, byt);
        Debug.Log(path);
        tex.Play();
    }




}
