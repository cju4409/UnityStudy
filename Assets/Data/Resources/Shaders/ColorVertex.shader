Shader "Study/ColorVertex"
{
	// 콘스탄트 버퍼
	Properties
	{
		_Ambient("Ambient", Range(0.0, 1.0)) = 0.3
		//_MainColor("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				//시맨틱
				float4 vertex : POSITION;
				float4 color : COLOR;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float3 normal : NORMAL;
			};

			float _Ambient;

			// 버텍스 쉐이더
			v2f vert(appdata v)
			{
				v2f o;
				//UnityObjectToClipPos: 로컬 좌표 입력시 트랜스폼이 적용된 좌표 반환
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			// 픽셀 쉐이더
			fixed4 frag(v2f i) : SV_Target
			{
				float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
				float d = dot(lightDir, i.normal);
				if (d < _Ambient) d = _Ambient;
				return i.color * float4(d,d,d,1);
			}
			ENDCG
		}
	}
}
