Shader "Study/OutLine"
{
    Properties
    {
        _Color("Outline Color", Color) = (1,0,0,1)
        _Width("Outline Width", Range(0.0, 0.1)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            // ZWrite: 깊이버퍼 사용 유무
            ZWrite off
            // Cull : 무엇을 안그릴지 설정
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float _Width;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = normalize(TransformViewToProjection(normal.xy));
                o.vertex.xy += offset * _Width;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
