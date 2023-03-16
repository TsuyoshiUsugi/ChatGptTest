Shader "Custom/Outlined Diffuse with Edge Detection" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Outline("Outline width", Range(0.0, 0.03)) = 0.01
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
            LOD 100

            CGPROGRAM
            #pragma surface surf Standard vertex:vert
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input {
                float2 uv_MainTex;
                float3 worldPos;
                float3 worldNormal;
            };

            half4 _Color;
            half4 _OutlineColor;
            half _Outline;

            void vert(inout appdata_full v) {
                v.vertex = UnityObjectToClipPos(v.vertex);
            }

            void surf(Input IN, inout SurfaceOutputStandard o) {
                half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;

                // Calculate normal in view space
                half3 worldNormal = normalize(IN.worldNormal);
                half3 viewNormal = normalize(UnityWorldSpaceViewDir(IN.worldPos));
                half viewDot = 1.0 - dot(worldNormal, viewNormal);

                // Calculate outline based on the dot product
                half outline = smoothstep(_Outline - 0.002, _Outline + 0.002, viewDot);

                // Apply outline color to pixels that pass the outline test
                if (outline > 0) {
                    half4 outlineColor = _OutlineColor;
                    outlineColor.a = outline;
                    o.Emission = outlineColor.rgb;
                }
            }
            ENDCG
        }
            FallBack "Diffuse"
}
