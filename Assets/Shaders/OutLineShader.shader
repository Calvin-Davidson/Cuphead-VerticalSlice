Shader "OutLineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Toggle] _useTexelSize("Use Texel Size", Float) = 0
        _lineDepth("Line Depth", Range(-10,10)) = 1
		_color("Line Color", Color) = (1,1,1,1)
    }
        SubShader
        {
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

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _MainTex_TexelSize;
                float _lineDepth, _useTexelSize;
                float4 _color;

                SHADERDATA vert(v2f v)
                {
                    SHADERDATA o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;//TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(SHADERDATA i) : SV_Target
                {
                    float4 size = _MainTex_ST;
                    float lineDepth = _lineDepth / 50;
                    if(_useTexelSize>0){
                        size = _MainTex_TexelSize;
                        lineDepth = _lineDepth;
                    }
                    
                    fixed4 col = tex2D(_MainTex, i.uv);
                    fixed2 right = float2(_MainTex_TexelSize.x * lineDepth * lineDepth, 0);
                    fixed2 up = float2(0, _MainTex_TexelSize.y * lineDepth * lineDepth);
                    fixed leftPixel = tex2D(_MainTex, (i.uv + float2(-right.x, 0))).a;
                    fixed upPixel = tex2D(_MainTex, (i.uv + up)).a;
                    fixed rightPixel = tex2D(_MainTex, (i.uv + right)).a;
                    fixed bottomPixel = tex2D(_MainTex, (i.uv + float2(0, -up.y))).a;

                    fixed outline = (1 - leftPixel * upPixel * rightPixel * bottomPixel) * col.a;
                    if (lineDepth < 0) {
                        outline = max(max(leftPixel, upPixel), max(rightPixel, bottomPixel)) - col.a;
                    }

                    fixed4 color = lerp(col, _color, outline);
                    return color;
                }
                ENDCG
            }
        }
}
