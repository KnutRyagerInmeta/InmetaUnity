Shader "Energy Shield/Shield" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,0.7725025,0,1)
        _Speed1 ("Speed1", Range(-1, 1)) = 0.1
        _Speed2 ("Speed2", Range(-1, 1)) = 0.12
        _Power_T1 ("Power_T1", Range(0, 1)) = 0.5760269
        _Texture2 ("Texture 2", 2D) = "white" {}
        _Color2 ("Color2", Color) = (1,0.2266078,0,1)
        _Speed1_T2 ("Speed1_T2", Range(-1, 1)) = -0.1
        _Speed2_T2 ("Speed2_T2", Range(-1, 1)) = -0.12
        _Power_T2 ("Power_T2", Range(0, 1)) = 0.6410257
        _HitTexture ("HitTexture", 2D) = "white" {}
        _HitColor ("HitColor", Color) = (1,0,0.0689106,1)
        _FresnelEffect ("FresnelEffect", Range(0, 4)) = 2.616094
        _FresnelColor ("FresnelColor", Color) = (0,0.5849056,0.08482971,1)
        [HideInInspector]_HitPos ("HitPos", Vector) = (1,0,0,0)
        [HideInInspector]_HitAlpha ("HitAlpha", Range(0, 1)) = 0
        [HideInInspector]_HitPos1 ("HitPos1", Vector) = (0,1,0,0)
        [HideInInspector]_HitAlpha1 ("HitAlpha1", Range(0, 1)) = 0
        [HideInInspector]_HitPos2 ("HitPos2", Vector) = (0,1,0,0)
        [HideInInspector]_HitAlpha2 ("HitAlpha2", Range(0, 1)) = 0
        [HideInInspector]_HitPos3 ("HitPos3", Vector) = (0,1,0,0)
        [HideInInspector]_HitAlpha3 ("HitAlpha3", Range(0, 1)) = 0
        [HideInInspector]_HitPos4 ("HitPos4", Vector) = (0,1,0,0)
        [HideInInspector]_HitAlpha4 ("HitAlpha4", Range(0, 1)) = 0
        _StepUp ("StepUp", Range(0.5, 1)) = 1
        _StepDown ("StepDown", Range(0, 0.5)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcColor
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           // #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Speed1;
            uniform float _Speed2;
            uniform float4 _Color;
            uniform sampler2D _Texture2; uniform float4 _Texture2_ST;
            uniform float _Speed1_T2;
            uniform float _Speed2_T2;
            uniform float4 _Color2;
            uniform float _Power_T2;
            uniform float _Power_T1;
            uniform float _FresnelEffect;
            uniform float4 _FresnelColor;
            uniform float4 _HitPos;
            uniform sampler2D _HitTexture; uniform float4 _HitTexture_ST;
            uniform float4 _HitColor;
            uniform float4 _HitPos1;
            uniform float _HitAlpha1;
            uniform float _HitAlpha;
            uniform float4 _HitPos2;
            uniform float _HitAlpha2;
            uniform float4 _HitPos3;
            uniform float _HitAlpha3;
            uniform float4 _HitPos4;
            uniform float _HitAlpha4;
            uniform float _StepUp;
            uniform float _StepDown;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 node_2004 = _Time;
                float2 node_1830 = ((float2(_Speed1,_Speed2)*node_2004.g)+i.uv0);
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_1830, _Texture));
                float4 node_549 = _Time;
                float2 node_6193 = ((float2(_Speed1_T2,_Speed2_T2)*node_549.g)+i.uv0);
                float4 _Texture2_var = tex2D(_Texture2,TRANSFORM_TEX(node_6193, _Texture2));
                float4 _HitTexture_var = tex2D(_HitTexture,TRANSFORM_TEX(i.uv0, _HitTexture));
                float3 emissive = (lerp(((_Texture_var.rgb*_Color.rgb*_Power_T1)+(_Texture2_var.rgb*_Color2.rgb*_Power_T2)+(_FresnelColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelEffect))),(_HitTexture_var.rgb*_HitColor.rgb),(((saturate(((1.0 - distance((_HitPos.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha)*isFrontFace)+((saturate(((1.0 - distance((_HitPos1.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha1)*isFrontFace)+((saturate(((1.0 - distance((_HitPos2.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha2)*isFrontFace)+((saturate(((1.0 - distance((_HitPos3.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha3)*isFrontFace)+((saturate(((1.0 - distance((_HitPos4.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha4)*isFrontFace)))*saturate((step(i.uv0.g,_StepUp)*step(_StepDown,i.uv0.g))));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Speed1;
            uniform float _Speed2;
            uniform float4 _Color;
            uniform sampler2D _Texture2; uniform float4 _Texture2_ST;
            uniform float _Speed1_T2;
            uniform float _Speed2_T2;
            uniform float4 _Color2;
            uniform float _Power_T2;
            uniform float _Power_T1;
            uniform float _FresnelEffect;
            uniform float4 _FresnelColor;
            uniform float4 _HitPos;
            uniform sampler2D _HitTexture; uniform float4 _HitTexture_ST;
            uniform float4 _HitColor;
            uniform float4 _HitPos1;
            uniform float _HitAlpha1;
            uniform float _HitAlpha;
            uniform float4 _HitPos2;
            uniform float _HitAlpha2;
            uniform float4 _HitPos3;
            uniform float _HitAlpha3;
            uniform float4 _HitPos4;
            uniform float _HitAlpha4;
            uniform float _StepUp;
            uniform float _StepDown;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_2004 = _Time;
                float2 node_1830 = ((float2(_Speed1,_Speed2)*node_2004.g)+i.uv0);
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_1830, _Texture));
                float4 node_549 = _Time;
                float2 node_6193 = ((float2(_Speed1_T2,_Speed2_T2)*node_549.g)+i.uv0);
                float4 _Texture2_var = tex2D(_Texture2,TRANSFORM_TEX(node_6193, _Texture2));
                float4 _HitTexture_var = tex2D(_HitTexture,TRANSFORM_TEX(i.uv0, _HitTexture));
                o.Emission = (lerp(((_Texture_var.rgb*_Color.rgb*_Power_T1)+(_Texture2_var.rgb*_Color2.rgb*_Power_T2)+(_FresnelColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelEffect))),(_HitTexture_var.rgb*_HitColor.rgb),(((saturate(((1.0 - distance((_HitPos.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha)*isFrontFace)+((saturate(((1.0 - distance((_HitPos1.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha1)*isFrontFace)+((saturate(((1.0 - distance((_HitPos2.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha2)*isFrontFace)+((saturate(((1.0 - distance((_HitPos3.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha3)*isFrontFace)+((saturate(((1.0 - distance((_HitPos4.rgb*(-1.5)).rgb,i.normalDir))*3.0+-1.0))*_HitAlpha4)*isFrontFace)))*saturate((step(i.uv0.g,_StepUp)*step(_StepDown,i.uv0.g))));
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}