Shader "Custom/Diffuse2D2"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _Parameter("Parameter", Range(0,1)) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        
        	Blend SrcAlpha OneMinusSrcAlpha

		ZWrite off
		Cull off

		Pass{

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			sampler2D _MaskTex;
			float4 _MaskTex_ST;
			float4 _MainTex_ST;

			fixed4 _Color;
			fixed _Parameter;


			struct appdata{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 maskcoord : TEXCOORD1;
				fixed4 color : COLOR;
			};

			struct v2f{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 maskcoord : TEXCOORD1;
				fixed4 color : COLOR;
			};

			v2f vert(appdata v){
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.maskcoord = TRANSFORM_TEX(v.maskcoord, _MaskTex);
				o.color = v.color;
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET{
				float2 uv = i.uv;
				float2 uvMask = i.maskcoord;
				float2 uvMask2 = i.maskcoord;
				uvMask += float2(0, 0.5*_Parameter);
				float2 texMask = tex2D(_MaskTex, uvMask);
				uvMask2 -= float2(0, 0.5*_Parameter);
				float2 texMask2 = tex2D(_MaskTex, uvMask2);
				uv.y=lerp(uv.y, uv.y+min(0.05, _Parameter), texMask.r);
				uv.y=lerp(uv.y, uv.y-min(0.05, _Parameter), texMask2.r);
				fixed4 col =  tex2D (_MainTex, uv) * i.color;
				col *= _Color;
				col *= i.color;
				return col;
			}
			ENDCG
		}
}
}