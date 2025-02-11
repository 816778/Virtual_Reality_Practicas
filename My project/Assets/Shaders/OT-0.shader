Shader "Hidden/Panorama_Grid"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Parallels ("Number of Parallels", Float) = 10
        _Meridians ("Number of Meridians", Float) = 10
        _LineThickness ("Line Thickness", Float) = 0.0005
        _LineColor ("Line Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Cull Front // Ensures correct rendering for panoramic images

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
            float _Parallels;   // Number of horizontal lines
            float _Meridians;   // Number of vertical lines
            float _LineThickness;  // Thickness of the grid lines
            fixed4 _LineColor;  // Color of the grid lines

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Convert UV to spherical coordinates
                float latitude = i.uv.y * 180.0 - 90.0;  // -90 to 90 degrees
                float longitude = i.uv.x * 360.0 - 180.0; // -180 to 180 degrees
                
                // Convert lat/lon into a grid pattern
                float parallels = abs(sin(_Parallels * (latitude + 90.0) * 3.14159 / 180.0));
                float meridians = abs(sin(_Meridians * (longitude + 180.0) * 3.14159 / 180.0));

                // Determine grid intensity
                float grid = max(parallels, meridians);
                float mask = step(1.0 - _LineThickness, grid); // Controls line thickness

                // Blend the grid with the image
                col.rgb = lerp(col.rgb, _LineColor.rgb, mask * _LineColor.a);

                return col;
            }
            ENDCG
        }
    }
}

