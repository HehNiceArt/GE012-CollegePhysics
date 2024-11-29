Shader "Experiment3/SpriteGradient"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _TopColor ("Top Accent Color", Color) = (1,1,1,1)
        _BottomColor ("Bottom Accent Color", Color) = (1,1,1,1)
        _GradientPower ("Gradient Power", Range(0,1)) = 0.5

        [Toggle] _UseParticles ("Enable Particles", Float) = 0
        _ParticleSize ("Particle Size", Range(20, 100)) = 50
        _ParticleSpeed ("Particle Speed", Range(0.1, 5)) = 1
        _ParticleColor ("Particle Color", Color) = (1,1,1,0.5)
        _ParticleThreshold ("Particle Threshold", Range(0.1, 0.4)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;
            float4 _TopColor;
            float4 _BottomColor;
            float _GradientPower;

            float _UseParticles;
            float _ParticleSize;
            float _ParticleSpeed;
            float _ParticleThreshold;
            float4 _ParticleColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                return o;
            }

            float2 random2(float2 st)
            {
                st = float2(dot(st, float2(127.1, 311.7)), dot(st, float2(269.5, 183.3)));
                return -1.0 + 2.0 * frac(sin(st) * 43758.5454123);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float gradient = i.uv.y;
                float4 gradientColor = lerp(_BottomColor, _TopColor, gradient);
                float4 finalCol = lerp(_MainColor, gradientColor, _GradientPower);

                
                if(_UseParticles > 0.5)
                {
                    float2 st = i.uv * _ParticleSize;
                    float time = _Time.y * _ParticleSpeed;
                    st.y += time;

                    float2 ist = floor(st);
                    float2 fst = frac(st);

                    float minDist = 1.0;

                    for (int y = 01; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            float2 neighbor = float2(x,y);
                            float2 sum = ist + neighbor;
                            float2 p = random2(sum);
                            p = 0.5 + 0.5 * sin(time + 6.2831 * p);
                            float dist = length(neighbor + p - fst);
                            minDist = min(minDist, dist);
                        }
                    }
                    float particles = smoothstep(_ParticleThreshold, _ParticleThreshold + 0.01, minDist);
                    float4 finalColor = lerp(finalCol + _ParticleColor, finalCol, particles);
                    return col *finalColor;
                }else{
                    return col * finalCol;
                }
            }
            ENDCG
        }
    }
}
