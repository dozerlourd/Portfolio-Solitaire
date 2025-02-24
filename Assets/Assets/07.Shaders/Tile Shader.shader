Shader "Custom/Tile Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _IsTileClicked ("IsTileClicked", float) = 0

        _GlowColor ("Glow Color", Color) = (1, 0.5, 0, 1)
        _GlowIntensity ("GlowIntensity", Range(1, 10)) = 1
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

            float4 _GlowColor;
            float _GlowIntensity;

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
                float t = sin(_Time.w + 1) * 0.5 + 1;
                
                fixed4 glow = fixed4(0,0,0,0);
                if(_IsTileClicked)
                {
                    float glowFactor = col.r * _GlowIntensity * t;
                    glow = _GlowColor * glowFactor;
                }

                return col+ glow;
            }
            ENDCG
        }
    }
}
