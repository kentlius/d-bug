#include "UnityCG.cginc"
Shader "Custom/BlurSpriteBackground" {
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0.0, 10.0)) = 1.0
        _EnableBlur ("Enable Blur", Range(0.0, 1.0)) = 0.0
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _BlurSize;
            float _EnableBlur;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the sprite texture
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Sample the background pixels
                float4 bgColor = tex2D(_CameraOpaqueTexture, i.uv);

                // Calculate the blur radius in pixels
                float radius = _BlurSize * texelSize(_CameraOpaqueTexture, i.uv).x;

                // Apply the blur effect to the background pixels
                float4 blurredColor = 0.0;
                if (_EnableBlur > 0.0 && radius > 0.0) {
                    for (int x = -4; x <= 4; x++) {
                        for (int y = -4; y <= 4; y++) {
                            float2 offset = float2(x, y) * radius;
                            blurredColor += tex2D(_CameraOpaqueTexture, i.uv + offset);
                        }
                    }
                    blurredColor /= 81.0;
                }
                else {
                    blurredColor = bgColor;
                }

                // Combine the blurred background pixels with the sprite texture
                return texColor * (1 - _EnableBlur) + blurredColor * _EnableBlur;
            }
            ENDCG
        }
    }
}