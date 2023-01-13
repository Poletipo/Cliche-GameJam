Shader "Custom/Wave_VFX"
{
    Properties
    {
        _ColorA("ColorA", Color) = (1,1,1,1)
        _ColorB("ColorB", Color) = (1,1,1,1)
        _StartA("Start ColorA", Range(0,1)) = 0
        _StartB("Start ColorB", Range(0,1)) = 1
    }
        SubShader
    {
        Tags {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Pass
        {
            Cull off
            ZWrite off
            Blend One One //additive
            //Blend DstColor Zero // Multiply



            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.2831853071795865

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            float4 _ColorA;
            float4 _ColorB;
            float _StartA;
            float _StartB;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldDir(v.normal);

                return o;
            }

            float InverseLerp(float a, float b, float v) {
                return (v - a) / (b - a);
            }

            float4 frag(v2f i) : SV_Target
            {
                float xOffset = cos(i.uv.x * TAU * 8) * 0.01;

                float t = cos((i.uv.y + xOffset - _Time.y * 0.1) * TAU * 5) * 0.5 + 0.5;

                t *= 1 - i.uv.y;

                float waves = t * (abs(i.normal.y) < 0.999);

                float4 gradient = lerp(_ColorA, _ColorB, i.uv.y);

                return gradient * waves;
            }
            ENDCG
        }
    }
}
