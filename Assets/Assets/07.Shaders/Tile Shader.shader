Shader "Custom/Tile Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _IsTileClicked ("IsTileClicked", float) = 0
        _OutlineThickness ("OutlineThickness", float) = 0
        _OutlineColor ("OutlineColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _IsTileClicked;
            float _OutlineThickness;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float t = 0;
                
                float outline = tex2D(_MainTex, i.uv + float2(_OutlineThickness, 0)).a +
                                tex2D(_MainTex, i.uv - float2(_OutlineThickness, 0)).a +
                                tex2D(_MainTex, i.uv + float2(0, _OutlineThickness)).a +
                                tex2D(_MainTex, i.uv - float2(0, _OutlineThickness)).a; 

                 if (outline > 0.0 && col.a == 0.0 && _IsTileClicked)
                {
                    return _OutlineColor; // 아웃라인 색상 적용
                }

                return col;
            }
            ENDCG
        }
    }
}
