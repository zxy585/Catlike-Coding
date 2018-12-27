using System.Collections.Generic;
using UnityEngine;

//继承自PostEffectBase
public class TextureAdjustEffect : PostEffectBase
{
    public bool isPlay;
    //需要使用的图像
    public List<Texture2D> blendTexture = new List<Texture2D>();
    public int framenum = 1;
    private int Texturenum = 0;
    private int num = 0;
    private int frame = 0;
    //通过Range控制可以输入的参数的范围
    [Range(0.0f, 3.0f)]
    public float brightness = 1.0f;//亮度
    [Range(0.0f, 1.0f)]
    public float opacity = 1.0f;//混合程度
    //覆写OnRenderImage函数
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        //仅仅当有材质的时候才进行后处理，如果_Material为空，不进行后处理
        if (_Material)
        {
            //通过Material.SetXXX（"name",value）可以设置shader中的参数值
            _Material.SetFloat("_Brightness", brightness);
            _Material.SetTexture("_BlendTex", blendTexture[num]);
            _Material.SetFloat("_Opacity", opacity);
            //使用Material处理Texture，dest不一定是屏幕，后处理效果可以叠加的！
            Graphics.Blit(src, dest, _Material);
        }
        else
        {
            //直接绘制
            Graphics.Blit(src, dest);
        }
    }

    private void Start()
    {
        opacity = 0;
        num = 0;
        Texturenum = blendTexture.Count - 1;
    }

    private void FixedUpdate()
    {
        if (!isPlay)
        {
            opacity = 0;
            num = 0;
            brightness = 1;
            return;
        }
        if (brightness > 0.6)
        {
            brightness -= 0.05f;
            return;
        }
        opacity = 1;
        if (num >= Texturenum) num = 20;

        frame += 1;
        if (frame >= framenum)
        {
            frame = 0;
            num += 1;
        }
    }


}
