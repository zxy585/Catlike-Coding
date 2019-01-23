using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScr : MonoBehaviour {
    public Texture2D bulletTexture;
    public Texture2D wallTexture;

    Texture2D NewWallTexture;

    float wall_height;
    float wall_widht;

    float bullet_height;
    float bullet_widght;

    RaycastHit hit;
    Queue<Vector2> uvQueues;
    // Use this for initialization
    void Start () {
        uvQueues = new Queue<Vector2>();
        wallTexture = GetComponent<MeshRenderer>().material.mainTexture as Texture2D;

        NewWallTexture = Instantiate(wallTexture);

        GetComponent<MeshRenderer>().material.mainTexture = NewWallTexture;

        wall_height = NewWallTexture.height;
        wall_widht = NewWallTexture.width;

        bullet_height = bulletTexture.height;
        bullet_widght = bulletTexture.width;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.name=="Plane")
                {
                    Vector2 uv = hit.textureCoord;
                    uvQueues.Enqueue(uv);
                    for (int i=0;i<bullet_widght;i++)
                    {
                        for (int j = 0; j < bullet_height; j++)
                        {
                            float w = uv.x * wall_widht - bullet_widght / 2 + i;
                            float h = uv.y * wall_height - bullet_height / 2 + j;

                            Color wallColor = NewWallTexture.GetPixel((int)w,(int)h);

                            Color bulletColor = bulletTexture.GetPixel(i,j);

                            NewWallTexture.SetPixel((int)w,(int)h,wallColor*bulletColor);
                        }
                    }

                    NewWallTexture.Apply();
                    Debug.Log("调用融合贴图方法");
                    Invoke("ReturnWall", 3f);
                }
            }
        }
	}


    void ReturnWall()
    {
        Debug.Log("调用恢复贴图方法");
        Vector2 uv = uvQueues.Dequeue();
        for (int i = 0; i < bullet_widght; i++)
        {
            for (int j = 0; j < bullet_height; j++)
            {
                float w = uv.x * wall_widht - bullet_widght / 2 + i;
                float h = uv.y * wall_height - bullet_height / 2 + j;

                Color wallColor = wallTexture.GetPixel((int)w, (int)h);

                NewWallTexture.SetPixel((int)w, (int)h, wallColor);
            }
        }
        NewWallTexture.Apply();
    }

}
