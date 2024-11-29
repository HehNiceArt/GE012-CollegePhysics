Shader "Experiment3/SpriteOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
        _OutlineWidth ("Outline Width", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent" 
            "Queue"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

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
            float4 _MainTex_TexelSize;
            float4 _OutlineColor;
            float _OutlineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                
                // Sample the neighboring pixels
                float2 offsets[8] = {
                    float2(-1, -1), float2(-1, 0), float2(-1, 1),
                    float2(0, -1),                 float2(0, 1),
                    float2(1, -1),  float2(1, 0),  float2(1, 1)
                };

                float alpha = col.a;
                
                // Check neighboring pixels for edges
                for (int j = 0; j < 8; j++)
                {
                    float2 offset = offsets[j] * _MainTex_TexelSize.xy * _OutlineWidth;
                    float4 neighborCol = tex2D(_MainTex, i.uv + offset);
                    
                    // If we're in a transparent pixel and the neighbor is opaque,
                    // we're on an edge
                    if (col.a < 0.5 && neighborCol.a > 0.5)
                    {
                        alpha = 1;
                        col = _OutlineColor;
                        break;
                    }
                }

                col.a = alpha;
                return col;
            }
            ENDCG
        }
    }
} 