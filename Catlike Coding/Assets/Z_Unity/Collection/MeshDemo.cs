using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class MeshDemo : MonoBehaviour
{
    string fileName = " file:///C:/Users/Lenovo/Desktop/meshDate";
    [DllImport("tofclient")]
    public static extern void setMeshCallBack(showMeshCallBack cb);
    [DllImport("tofclient")]
    public static extern void startSlam();
    [DllImport("tofclient")]
    public static extern void startMesh();
    [DllImport("tofclient")]
    public static extern void stopMesh();
    [DllImport("tofclient")]
    public static extern void getMeshOnce();

    public delegate void showMeshCallBack(int indexSize, int verticesSize, ref int indexData, IntPtr verDatas);

    public static showMeshCallBack meshShowCallBack;

    public Text t01;
    public Text t02;

    private Queue<Vector3[]> _vertices;
    private Queue<Vector2[]> _uvs;
    private Queue<int[]> _triangles;
    private float lastTime;
    private float curTime;
    private bool isMakeMesh = false;
    private bool isCallBack = true;

    GameObject root;
    MeshFilter mf;
    MeshRenderer mr;
    public Material material;
    Mesh mesh;
    // Use this for initialization
    void Start()
    {
        //GetPentagon();
        meshShowCallBack = meshShowCallBackFunc;
        setMeshCallBack(meshShowCallBack);

        _vertices = new Queue<Vector3[]>();
        _uvs = new Queue<Vector2[]>();
        _triangles = new Queue<int[]>();

        root = new GameObject("root");
        root.transform.localPosition = Vector3.zero;
        root.transform.localScale = Vector3.one;
        mf = root.AddComponent<MeshFilter>();
        mr = root.AddComponent<MeshRenderer>();
        //material = Resources.Load<Material>("Trigrid_blue");
        mesh = new Mesh();
        root.SetActive(true);
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        CreateMesh();
    }
    void meshShowCallBackFunc(int indexSize, int verticesSize, ref int indexData, IntPtr verDatas)
    {
        if (isMakeMesh)
        {
            //if (!isCallBack) return;
            //  isCallBack = false;
            Debug.Log("调用meshShowCallBackFunc");
            t02.text = "调用meshShowCallBackFunc";
            //_vertices.Clear();
            //_uvs.Clear();
            //_triangles.Clear();

            float[] pointFloat = new float[indexData * 3 * 3];
            Marshal.Copy(verDatas, pointFloat, 0, pointFloat.Length);

            int[] triangles = new int[indexData * 3];
            Vector3[] vertices = new Vector3[indexData * 3];
            Vector2[] uvs = new Vector2[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertex = new Vector3(pointFloat[i * 3 + 0], pointFloat[i * 3 + 1], pointFloat[i * 3 + 2]);

                vertices[i] = vertex;
                triangles[i] = i;
                uvs[i] = new Vector2(vertex.x, vertex.y);
            }
            _vertices.Enqueue(vertices);
            _triangles.Enqueue(triangles);
            _uvs.Enqueue(uvs);
            Debug.Log("调用meshShowCallBackFunc成功");
            // CreateMesh();
            //Debug.Log("调用CreateMesh成功");
        }

    }
    void CreateMesh()
    {
        if (_vertices.Count < 1 || _uvs.Count < 1 || _triangles.Count < 1) return;
        if (isMakeMesh)
        {
            mesh.Clear();
            mesh.name = root.name;
  
            mesh.vertices = _vertices.Dequeue();
            mesh.uv = _uvs.Dequeue();
            mesh.triangles = _triangles.Dequeue();

            mesh.RecalculateBounds();//从顶点重新计算网格的边界体积。修改顶点后，应调用此函数以确保边界体积正确。分配三角形会自动重新计算边界体积。
            mesh.RecalculateNormals();

            root.GetComponent<MeshFilter>().mesh = mesh;

            root.GetComponent<MeshRenderer>().material = material;

           // isCallBack = true;
        }
    }

    public void StartMesh()
    {
        Debug.Log("调用StartMesh");
        t01.text = "调用StartMesh";
        startMesh();
        isMakeMesh = true;
        Debug.Log("调用StartMesh成功");
        t01.text = "调用StartMesh成功";
    }

    public void StopMesh()
    {
        Debug.Log("调用StopMesh");
        t01.text = "调用StopMesh";
        stopMesh();
        isMakeMesh = false;
        Debug.Log("调用StopMesh成功");
        t01.text = "调用StopMesh成功";
    }

    public void GetMeshOnce()
    {
        Debug.Log("调用GetMeshOnce");
        t01.text = "调用GetMeshOnce";
        getMeshOnce();
        Debug.Log("调用GetMeshOnce成功");
        t01.text = "调用GetMeshOnce成功";
    }


    public GameObject GetPentagon()
    {
        GameObject go = new GameObject("Pentagon");
        Debug.Log("GetPentagon");
        MeshFilter filter = go.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        filter.sharedMesh = mesh;
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 2, 0), new Vector3(2, 0, 0), new Vector3(2, -2, 0), new Vector3(1, -2, 0) };
        mesh.triangles = new int[9] { 0, 1, 2, 0, 2, 3, 0, 3, 4 };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        Material material = new Material(Shader.Find("Diffuse"));
        material.SetColor("_Color", Color.yellow);
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;
        return go;
    }
}
