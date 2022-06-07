Shader "50_Shades_of_Pog/Poggify"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
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

            //uv = pixel coord
            //fixed 4 = -2 to 2 (0 = r, 1 = g, 2 = b, 3 = a)
            //_Time[] (0 = sec/20, 1 = sec, 2 = sec * 2, 3 = sec * 3)

            fixed4 frag (v2f pixel) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, pixel.uv);

                col.r = abs(sin(pixel.vertex.x/1920 + _Time[1]));
                col.g = abs(sin(pixel.vertex.y/1080 + _Time[1]));

                return col;

            }
            ENDCG
        }
    }
}
