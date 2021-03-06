﻿
Shader "Hidden/MaskShder"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MaskWidth("MaskWidth", Range(0,1)) = 1.0
		_Color("Color", Color) = ( 1.0, 1.0, 1.0, 1.0 )
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _MaskWidth;
			fixed4 _Color;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				
				// 去掉透明度接近0的位置，和需要被遮罩的位置 
				clip(col.a - 0.01);
				clip(_MaskWidth - i.uv.x);
				
				return col*_Color;
			}
			ENDCG
		}
	}
}
