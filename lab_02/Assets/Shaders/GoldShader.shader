Shader "Custom/GoldShader"
{
    Properties
    {
        _Color ("Base Color", Color) = (1, 0.843, 0, 1) // Color dorado
        _Metallic ("Metallic", Range(0,1)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.8
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Metallic;
        half _Glossiness;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color.rgb; // Color base
            o.Metallic = _Metallic; // 100% metálico
            o.Smoothness = _Glossiness; // Reflejo fuerte
            o.Emission = _Color.rgb * 0.2; // Sutil brillo dorado
        }
        ENDCG
    }
    FallBack "Diffuse"
}
