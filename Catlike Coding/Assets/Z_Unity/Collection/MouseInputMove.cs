using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputMove : MonoBehaviour {

    private bool flagMove;
    private Vector3 mousePos;
    private RaycastHit hit;
    private Vector3 targetDir;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayControl();
        }
        if (flagMove)
        {
            if (Vector3.Distance(transform.position, mousePos) > 1)
            {
                transform.Translate(transform.worldToLocalMatrix * transform.forward * Time.deltaTime * 5);//transform.forward是世界坐标，通过transform.worldToLocalMatrix转换矩阵转到本地坐标然后在本地坐标运动，没有必要必须在本地坐标系运动 但是必须注意要统一起来。
            }
            else
            {
                flagMove = false;
            }
        }
    }
    void RayControl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//向屏幕发射一条射线
        if (Physics.Raycast(ray, out hit, 200)) //射线长度为200 和地面的碰撞盒做检测
          {
            GameObject targetPos = GameObject.CreatePrimitive(PrimitiveType.Sphere);//实例化一个Sphere
            targetPos.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            mousePos = hit.point;//获取碰撞点坐标
            mousePos.y = transform.position.y;
            targetPos.transform.position = mousePos;//Sphere放到鼠标点击的地方
            targetDir = mousePos - transform.position;//计算出朝向
            Vector3 tempDir = Vector3.Cross(transform.forward, targetDir.normalized);//用叉乘判断两个向量是否同方向
            float dotValue = Vector3.Dot(transform.forward, targetDir.normalized);//点乘计算两个向量的夹角，及角色和目标点的夹角
            float angle = Mathf.Acos(dotValue) * Mathf.Rad2Deg;
            if (tempDir.y < 0)//这块 说明两个向量方向相反，这个判断用来确定假如两个之间夹角30度 到底是顺时 还是逆时针旋转。
            {
                angle = angle * (-1);
            }
            print(tempDir.y);
            print("2:" + angle);
            transform.RotateAround(transform.position, Vector3.up, angle);
            flagMove = true;
        }
    }

}
