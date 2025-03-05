Shader "Custom/RainbowGlowEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _UVValue ("UVValue", float) = 0
        _AlphaValue ("AlphaValue", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        ZWrite Off
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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _UVValue;
            float _AlphaValue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float3 HSVToRGB(float3 hsv)
            {
                float3 rgb;
                float h = hsv.x;
                float s = hsv.y;
                float v = hsv.z;

                if (s == 0) {
                    rgb = float3(v, v, v);
                } else {
                    float i = floor(h * 6);
                    float f = h * 6 - i;
                    float p = v * (1 - s);
                    float q = v * (1 - f * s);
                    float t = v * (1 - (1 - f) * s);

                    if (i == 0) rgb = float3(v, t, p);
                    else if (i == 1) rgb = float3(q, v, p);
                    else if (i == 2) rgb = float3(p, v, t);
                    else if (i == 3) rgb = float3(p, q, v);
                    else if (i == 4) rgb = float3(t, p, v);
                    else rgb = float3(v, p, q);
                }

                return rgb;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Create a circular mask
                float2 center = float2(0.5, 0.5);

                i.uv -= center;
                i.uv *= 1 / clamp(_UVValue, 0, 1);
                i.uv += center;

                float distance = length(i.uv - center);
                float circleMask = smoothstep(0.5, 0.4, distance);

                // Calc angle
                float angle = atan2(i.uv.y - center.y, i.uv.x - center.x);
                angle = (angle + UNITY_PI) / (2 * UNITY_PI); // 0 ~ 1 범위로 변환

                // Calc HSV color
                float3 hsvColor = float3(angle, 1.0, 1.0);
                float3 rgbColor = HSVToRGB(hsvColor);

                if(distance > circleMask + 0.5)
                {
                    discard;
                }

                float glow = smoothstep(0.6, 0.1, distance); 
                glow = clamp(glow, 0.2, 1.0); 

                // Calc final color
                fixed4 col = fixed4(rgbColor, 1.0) * circleMask * glow * 5;

                col.a = smoothstep(0.5, 0.3, distance) * (distance) * clamp(_AlphaValue, 0, 1);

                return col;
            }

            ENDCG
        }
    }
}