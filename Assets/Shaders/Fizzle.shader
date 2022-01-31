Shader "50_Shades_of_Pog/Fizzle"
{

    Properties
    {

        _MainTex ("Texture", 2D) = "white" {}
        Distortion ("Distort Dampening", Float) = 0.2

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
            float Distortion;

            //uv = pixel coord on texture
            //vertex = vertex info (can be used for screen coord)
            //fixed 4 = -2 to 2 (0 = r, 1 = g, 2 = b, 3 = a)
            //_Time[] (0 = sec/20, 1 = sec, 2 = sec * 2, 3 = sec * 3)

            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, i.uv + sin(_Time[1]) / Distortion);

                return col;

            }
            ENDCG

        }

    }

}