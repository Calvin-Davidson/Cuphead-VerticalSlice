Shader "CanvasShader"
{
    Properties
    {
        _MainTex("Main camera input", 2D) = "white" {}
        [Toggle] _desaturate("Desaturate enabled", Float) = 0
        _desaturationStrength("Desaturation strength", Range(0,1)) = 0
        _desaturationBrightness("Desaturation brightness", Range(-0.05,0.15)) = 0
        _OverlayTexture("Overlay texture", 2D) = "white" {}
        _overlayIntensity("Overlay intensity", Range(0,1)) = 0.075
        _MaskTexture("Mask texture", 2D) = "white" {}
        _maskIntensity("Mask intensity", Range(0,1)) = 0
        _colorGrade("Color Grading", Color) = (1,1,1,1)
    }
        SubShader
        {
            // No culling or depth as it is a post processing shader
            Cull Off ZWrite Off ZTest Always

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct v2f
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct SHADERDATA
                {
                    float4 vertex : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                float _maskIntensity, _overlayIntensity, _desaturate, _desaturationStrength, _desaturationBrightness;
                sampler2D _MainTex, _OverlayTexture, _MaskTexture;
                // Declare Scale and Transform properties of the texture. x/y correspond to the scale x/y inputs in the editor, z/w correspond to the offset x/y inputs in the editor
                uniform float4  _OverlayTexture_ST, _MaskTexture_ST, _colorGrade;

                SHADERDATA vertex_shader(float4 vertex:POSITION, float2 uv : TEXCOORD0) //Convert all vertexes on screen to UV coordinates
                {
                    SHADERDATA vs;
                    vs.vertex = UnityObjectToClipPos(vertex);
                    vs.uv = uv;
                    return vs;
                }

                SHADERDATA vert(v2f v) //Calculate shader data from 3d to 2d
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 lerp4(fixed4 a, fixed4 b, fixed4 alpha) {
                    float colR = a.r + alpha * (b.r - a.r);
                    float colG = a.g + alpha * (b.g - a.g);
                    float colB = a.b + alpha * (b.b - a.b);
                    float colW = a.a + alpha * (b.a - a.a);
                    fixed4 returnValue = fixed4(colR, colG, colB, colW);
                    return returnValue;
                }

                float lerp(float a, float b, float alpha) {
                    return a + alpha * (b - a);
                }

                fixed4 frag(SHADERDATA i) : SV_Target // Main shader
                {
                    fixed4 col = tex2D(_MainTex, i.uv); // Get color per pixel from camera
                    // Declare UVs that must be translated and scaled independently from the main camera UV
                    float2 overlayUV = i.uv.xy;
                    float2 maskUV = i.uv.xy;
                    // Scale  UVs
                    overlayUV.x *= _OverlayTexture_ST.x;
                    overlayUV.y *= _OverlayTexture_ST.y;
                    maskUV.x *= _MaskTexture_ST.x;
                    maskUV.y *= _MaskTexture_ST.y;
                    // Sample textures based on modified UVs
                    fixed4 overlayColor = tex2D(_OverlayTexture, overlayUV);
                    fixed4 overlayColorGraded = col;
                    fixed4 maskColor = tex2D(_MaskTexture, maskUV);
                    fixed4 maskColorGraded = col;

                    col.r = col.r * _colorGrade.r;
                    col.g = col.g * _colorGrade.g;
                    col.b = col.b * _colorGrade.b;

                    // Desaturation gets handled before everything else to not interfere with the overlay or noise masks
                    if (_desaturate > 0) {
                        fixed4 desaturation = fixed4(0.3 * col.r, 0.6 * col.g, 0.1 * col.b, 1.0f); // DesaturationTable
                        col.r = col.r + _desaturationStrength * (desaturation - col.r) + _desaturationBrightness; // Desaturate but add a brightness value to ensure the scene doesn't get too dark.
                        col.g = col.g + _desaturationStrength * (desaturation - col.g) + _desaturationBrightness;
                        col.b = col.b + _desaturationStrength * (desaturation - col.b) + _desaturationBrightness;
                    }
                    if (overlayColor.a > 0) {
                        overlayColorGraded = lerp4(col,overlayColor,overlayColor.a);
                    }
                    maskColorGraded = col * maskColor;
                    fixed4 col1 = lerp4(col, maskColorGraded, _maskIntensity);
                    fixed4 col2 = lerp4(col, overlayColorGraded, _overlayIntensity);
                    float lerpRatio = lerp(0, overlayColor.a, _overlayIntensity);
                    col = lerp4(col1, col2, lerpRatio);
                    return col;
                }
                ENDCG
            }
        }
}
