Shader "Hidden/GrayScaleShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_IsGrayScale ("IsGray", int) = 0
		_Light ("LightRate", Range(0,1)) = 0
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		AlphaTest Greater 0.5
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
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
			int _IsGrayScale;
			float _Light;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				
				if(_IsGrayScale == 1)
				{
					float val = col.r + col.g + col.b;
					val /= 3.0;
					col.rgb = val;
				}
				//col.rgb *= _Light;
				
				return col;
			}
			ENDCG
		}
	}
}

