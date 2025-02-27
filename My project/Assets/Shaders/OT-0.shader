Shader "Hidden/PanoramaWithMeridiansAndParallels"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineThickness ("Line Thickness", Float) = 0.005
        _LineColor ("Line Color", Color) = (1,1,1,1)
        _Meridians ("Number of Meridians", Float) = 10
        _Parallels ("Number of Parallels", Float) = 5
    }
    SubShader
    {
        Cull Front // Permite ver la esfera desde dentro
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
            float _Parallels;

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

                // **Cálculo de Meridianos**
                float longitude = atan2(i.worldNormal.z, i.worldNormal.x); // Ángulo de longitud
                float normalizedLongitude = (longitude / UNITY_PI) * 0.5 + 0.5;
                float meridianSpacing = 1.0 / _Meridians;
                float closestMeridian = round(normalizedLongitude / meridianSpacing) * meridianSpacing;
                float distanceToMeridian = abs(normalizedLongitude - closestMeridian);

                // **Cálculo de Paralelos**
                float parallelSpacing = 1.0 / (_Parallels + 1);
                float minDistanceParallel = 1.0; // Inicializamos con un valor alto

                for (int j = 1; j <= _Parallels; j++) {
                    float parallelPosition = j * parallelSpacing;
                    float distanceToParallel = abs(i.uv.y - parallelPosition);
                    minDistanceParallel = min(minDistanceParallel, distanceToParallel);
                }

                // Determinar si el pixel pertenece a una línea
                float isMeridian = step(distanceToMeridian, _LineThickness);
                float isParallel = step(minDistanceParallel, _LineThickness);

                // Combinar ambas líneas
                float isLine = max(isMeridian, isParallel);

                // Mezclar el color de la línea con la textura
                col.rgb = lerp(col.rgb, _LineColor.rgb, isLine * _LineColor.a);

                return col;
            }
            ENDCG
        }
    }
}
