Shader "Custom/SeeThroughWalls" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _SeeThroughAmount("See Through Amount", Range(0.0,1.0)) = 0.5
    }

    SubShader{
        Tags { "Queue" = "Transparent" }
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"


            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _SeeThroughAmount;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
                float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos.xyz);
                o.viewDir = viewDir;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target{
                #include "UnityCG.cginc"
                // レイを作成して、敵との交差を検出する
                float3 rayOrigin = _WorldSpaceCameraPos;
                float3 rayDirection = normalize(i.viewDir);
                float rayLength = length(rayDirection);
                RaycastHit hit;
                bool hitSomething = Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength);

                // テクスチャーの色を取得
                fixed4 tex = tex2D(_MainTex, i.uv);

                // 敵との交差がある場合、半透明にする
                if (hitSomething && hit.collider.tag == "Enemy") {
                    tex.a *= _SeeThroughAmount;
                }

                // テクスチャーの色を変更して出力
                return tex * _Color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
