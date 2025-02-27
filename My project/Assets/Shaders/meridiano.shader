Shader "Hidden/PanoramaWithFixedMeridians"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineThickness ("Line Thickness", Float) = 0.0005
        _LineColor ("Line Color", Color) = (1,1,1,1)
        _Meridians ("Number of Meridians", Float) = 10
    }
    SubShader
    {
        Cull Front // Permite ver la esfera desde adentro
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float _LineThickness;
            fixed4 _LineColor;
            float _Meridians;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Convertimos la normal del vértice en coordenadas esféricas
                float longitude = atan2(i.worldNormal.z, i.worldNormal.x); // Ángulo alrededor del eje Y

                // Normalizamos el ángulo para que esté entre 0 y 1 en el espacio UV
                float normalizedLongitude = (longitude / UNITY_PI) * 0.5 + 0.5;

                // Espaciado entre meridianos
                float meridianSpacing = 1.0 / _Meridians;

                // Encuentra la distancia al meridiano más cercano
                float closestMeridian = round(normalizedLongitude / meridianSpacing) * meridianSpacing;
                float distanceToMeridian = abs(normalizedLongitude - closestMeridian);

                // Dibuja la línea si está dentro del grosor definido
                if (distanceToMeridian < _LineThickness)
                {
                    col = _LineColor;
                }
                
                return col;
            }
            ENDCG
        }
    }
}
