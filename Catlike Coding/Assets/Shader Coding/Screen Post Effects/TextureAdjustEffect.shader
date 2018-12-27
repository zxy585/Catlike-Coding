Shader "MyShader/PostEffect/TextureBlendEffect"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Brightness("Brightness", Range(0,3)) = 1
		_BlendTex("Blend Texture", 2D) = "white"{}
		_Opacity("Blend Opacity", Range(0,1)) = 1
	}

		SubShader
		{

			Pass
		  {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _BlendTex;
			fixed _Opacity;
			half _Brightness;

			fixed4 frag(v2f_img i) : COLOR
			{
				//Get the colors from the RenderTexture and the uv's
				//from the v2f_img struct
				fixed4 renderTex = tex2D(_MainTex, i.uv);
				fixed4 blendTex = tex2D(_BlendTex, i.uv);
				fixed3 finalColor = renderTex * _Brightness;
				renderTex = fixed4(finalColor, renderTex.a);

				//Perform a multiply Blend mode
                //Multiply混合模式
				fixed4 blendedMultiply = renderTex * blendTex;
                //Add Blend模式
				fixed4 blendedAdd = renderTex + blendTex;
                //Overlay混合模式
				fixed4 blendedScreen = (1-(1-renderTex)*(1-blendTex));

				//Adjust amount of Blend Mode with a lerp
				renderTex = lerp(renderTex, blendedScreen, _Opacity);

				return renderTex;
			}

			ENDCG
		}

	}
	FallBack off
}
