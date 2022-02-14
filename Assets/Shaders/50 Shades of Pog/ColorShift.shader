Shader "50_Shades_of_Pog/ColorShift"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
        [NoScaleOffset]NoiseTex ("Noise Texture", 2D) = "white" {}
        NoiseMult ("Noise Multiplier", Float) = 1
        NoiseSpeedX ("Noise Speed X", Float) = 1
        NoiseSpeedY ("Noise Speed Y", Float) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D NoiseTex;
            float NoiseSpeedX;
            float NoiseSpeedY;
            float NoiseMult;

            //uv = pixel coord
            //fixed 4 = -2 to 2 (0 = r, 1 = g, 2 = b, 3 = a)
            //_Time[] (0 = sec/20, 1 = sec, 2 = sec * 2, 3 = sec * 3)

            fixed4 frag (v2f pixel) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, pixel.uv);
                fixed4 noise = tex2D(NoiseTex, float2(abs(sin(pixel.uv.x * (_Time[1] * NoiseSpeedX))), pixel.uv.y * abs(sin((_Time[1 * NoiseSpeedY])))));

                col.r = tex2D(_MainTex, float2(pixel.uv.x * (noise.r * NoiseMult), pixel.uv.y * (noise.r + NoiseMult))).r;
                
                col.b = tex2D(_MainTex, float2(pixel.uv.x * (noise.r / NoiseMult), pixel.uv.y * (noise.r * NoiseMult))).b;

                return col;

            }
            ENDCG
        }
    }
}
