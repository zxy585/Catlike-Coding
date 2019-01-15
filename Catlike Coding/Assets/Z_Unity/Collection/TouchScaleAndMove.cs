using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScaleAndMove : MonoBehaviour {

    // 记录手指触屏的位置  
    Vector2 m_screenpos = new Vector2();
    Vector3 oldPosition;

    // Use this for initialization  
    void Start()
    {
        //记录开始摄像机的Position
        oldPosition = Camera.main.transform.position;
    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.touchCount <= 0)
            return;

        // 1个手指触摸屏幕  
        if (Input.touchCount == 1)
        {

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                // 记录手指触屏的位置  
                m_screenpos = Input.touches[0].position;

            }
            // 手指移动  
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {

                // 移动摄像机  
                Camera.main.transform.Translate(new Vector3(-Input.touches[0].deltaPosition.x * Time.deltaTime * 0.1f, -Input.touches[0].deltaPosition.y * Time.deltaTime * 0.1f, 0));
            }
        }
        else if (Input.touchCount > 1)
        {
            // 记录两个手指的位置  
            Vector2 finger1 = new Vector2();
            Vector2 finger2 = new Vector2();

            // 记录两个手指的移动  
            Vector2 mov1 = new Vector2();
            Vector2 mov2 = new Vector2();

            for (int i = 0; i < 2; i++)
            {
                Touch touch = Input.touches[i];

                if (touch.phase == TouchPhase.Ended)
                    break;

                if (touch.phase == TouchPhase.Moved)
                {

                    float mov = 0;
                    if (i == 0)
                    {
                        finger1 = touch.position;
                        mov1 = touch.deltaPosition;

                    }
                    else
                    {
                        finger2 = touch.position;
                        mov2 = touch.deltaPosition;

                        if (finger1.x > finger2.x)
                        {
                            mov = mov1.x;
                        }
                        else
                        {
                            mov = mov2.x;
                        }

                        if (finger1.y > finger2.y)
                        {
                            mov += mov1.y;
                        }
                        else
                        {
                            mov += mov2.y;
                        }

                        Camera.main.transform.Translate(0, 0, mov * Time.deltaTime * 0.1f);
                    }

                }
            }

            //控制物体始终在屏幕中
            Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4f, 5.5f), Mathf.Clamp(transform.position.y, 0.5f, 10f), Mathf.Clamp(transform.position.z, -0.9f, 4.6f));

        }
    }
        //通过按钮让物体回到最初的位置
        public void BackRS()
        {
            Camera.main.transform.position = oldPosition;
        }

    }

