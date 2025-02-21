Shader "Hidden/Panorama"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FlipX ("Flip Horizontally", Float) = 0 // 0: Normal, 1: Flip Horizontally
        _FlipY ("Flip Vertically", Float) = 0  // 0: Normal, 1: Flip Vertically
    }
    SubShader
    {
        // No culling or depth
        Cull Front // ZWrite Off ZTest Always

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
            
            float _FlipX;
            float _FlipY;
		
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                if (_FlipX == 1) o.uv.x = 1.0 - o.uv.x; // Flip Horizontal
                if (_FlipY == 1) o.uv.y = 1.0 - o.uv.y; // Flip Vertical

                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                // col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
