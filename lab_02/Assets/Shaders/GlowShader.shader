Shader "Custom/GlowShader"
{
    Properties {
        _customColor ("Main Color", Color) = (1, 1, 1, 1)
        _EmissionColor ("Emission Color", Color) = (0, 0, 0, 1)
    }

    SubShader {
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
        #pragma surface surf Standard

        struct Input {
            float2 uv_MainTex;
        };

        fixed4 _customColor;
        fixed4 _EmissionColor;

        void surf (Input IN, inout SurfaceOutputStandard o) {
            o.Albedo = _customColor.rgb;       // Color base
            o.Emission = _EmissionColor.rgb;   // Emisión (Glow)
        }
        ENDCG
    }

    Fallback "Diffuse"
}
