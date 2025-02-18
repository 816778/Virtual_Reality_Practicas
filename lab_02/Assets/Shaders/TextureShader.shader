Shader "Custom/TextureShader"
{
    Properties {
        _MainTex ("Albedo (Texture)", 2D) = "white" {}
        _customColor ("Main Color", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Standard

        struct Input {
            float2 uv_MainTex; // Coordenadas UV para la textura
        };

        sampler2D _MainTex;      // Textura principal
        fixed4 _customColor;     // Color principal

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Mapea la textura en el Albedo usando tex2D
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = texColor.rgb * _customColor.rgb;
        }
        ENDCG
    }
    Fallback "Diffuse"
}
