Shader "PostProcessingShader"
{
    Properties
    {
        _MainTex ("Main camera input", 2D) = "white" {}
        [Toggle] _desaturate("Desaturate enabled", Float) = 0
        _desaturationStrength("Desaturation strength", Range(0,1)) = 0
        _desaturationBrightness("Desaturation brightness", Range(-0.05,0.15)) = 0
        _OverlayTexture ("Overlay", 2D) = "white" {}
        _overlayIntensity("Overlay intensity", Range(0,0.25)) = 0.075
        _NoiseMaskTexture("Noise Mask", 2D) = "white" {}
        _noiseMaskIntensity ("Noise Mask intensity", Range(0,1)) = 0
        _octaves("Octaves",Int) = 7
        _lacunarity("Lacunarity", Range(1.0 , 5.0)) = 2
        _gain("Gain", Range(0.0 , 1.0)) = 0.5
        _strength("Strength", Range(-2.0 , 2.0)) = 0.0
        _amplitude("Amplitude", Range(0.0 , 5.0)) = 1.5
        _frequency("Frequency", Range(0.0 , 6.0)) = 2.0
        _power("Power", Range(0.1 , 5.0)) = 1.0
        _offsetX("Noise offset X",Float) = 0.0
        _offsetY("Noise offset Y",Float) = 0.0
        _speedU("Noise speed X", Range(-5,5)) = 0
        _speedV("Noise speed Y", Range(-5,5)) = 0
        _scale("Noise scale", Float) = 1.0
        _range("Monochromatic Range", Range(0.0 , 1.0)) = 0.5
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

            float _octaves, _lacunarity, _gain, _strength, _amplitude, _frequency, _offsetX, _offsetY, _power, _scale, _range, _speedU, _speedV, _noiseMaskIntensity, _overlayIntensity, _desaturate, _desaturationStrength, _desaturationBrightness;
            sampler2D _MainTex, _OverlayTexture, _NoiseMaskTexture;
            // Declare Scale and Transform properties of the texture. x/y correspond to the scale x/y inputs in the editor, z/w correspond to the offset x/y inputs in the editor
            uniform float4  _OverlayTexture_ST, _NoiseMaskTexture_ST;

            float fbm(float2 p)
            {
                p = p * _scale + float2(_offsetX, _offsetY);
                for (int i = 0; i < _octaves; i++)
                {
                    // Simplex fractural frequency calculations
                    float2 f = frac(p * _frequency);
                    float2 t = f * f * f * (f * (f * 6.0 - 15.0) + 10.0);
                    float2 i = floor(p * _frequency);

                    // Get UV corner sample points from top left to bottom right
                    float2 a = i + float2(0.0, 0.0);
                    float2 b = i + float2(1.0, 0.0);
                    float2 c = i + float2(0.0, 1.0);
                    float2 d = i + float2(1.0, 1.0);

                    // Sample points from simplex noise seed
                    a = -1.0 + 2.0 * frac(sin(float2(dot(a, float2(127.1, 311.7)), dot(a, float2(269.5, 183.3)))) * 43758.5453123);
                    b = -1.0 + 2.0 * frac(sin(float2(dot(b, float2(127.1, 311.7)), dot(b, float2(269.5, 183.3)))) * 43758.5453123);
                    c = -1.0 + 2.0 * frac(sin(float2(dot(c, float2(127.1, 311.7)), dot(c, float2(269.5, 183.3)))) * 43758.5453123);
                    d = -1.0 + 2.0 * frac(sin(float2(dot(d, float2(127.1, 311.7)), dot(d, float2(269.5, 183.3)))) * 43758.5453123);

                    // Apply sampled simplex result a to UV corner from top left to bottom right
                    float A = dot(a, f - float2(0.0, 0.0));
                    float B = dot(b, f - float2(1.0, 0.0));
                    float C = dot(c, f - float2(0.0, 1.0));
                    float D = dot(d, f - float2(1.0, 1.0));

                    //Lerp between simplex results and fractural results
                    float noiseResult = (lerp(lerp(A, B, t.x), lerp(C, D, t.x), t.y)); 

                    //Post octave calculation handlers
                    _strength += _amplitude * noiseResult;
                    _frequency *= _lacunarity;
                    _amplitude *= _gain;
                }
                _strength = clamp(_strength, -1.0, 1.0);
                return pow(_strength * 0.5 + 0.5, _power); //Power increases or decreases contrast
            }

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

            fixed4 frag(SHADERDATA i) : SV_Target // Main shader
            {
                fixed4 col = tex2D(_MainTex, i.uv); // Get color per pixel from camera
                // Declare UVs that must be translated and scaled independently from the main camera UV
                float2 overlayUV = i.uv.xy;
                float2 noiseMaskUV = i.uv.xy;
                float2 noiseUV = i.uv.xy;
                // Translate UVs * gametime, allowing them to move. z/w values are fetched from the editor's offset input for textures
                overlayUV.x += _Time * -_OverlayTexture_ST.z;
                overlayUV.y += _Time * _OverlayTexture_ST.w;
                noiseMaskUV.x += _Time * -_NoiseMaskTexture_ST.z;
                noiseMaskUV.y += _Time * _NoiseMaskTexture_ST.w;
                noiseUV.x += _Time * -_speedU;
                noiseUV.y += _Time * _speedV;
                // Scale  UVs
                overlayUV.x *= _OverlayTexture_ST.x;
                overlayUV.y *= _OverlayTexture_ST.y;
                noiseMaskUV.x *= _NoiseMaskTexture_ST.x;
                noiseMaskUV.y *= _NoiseMaskTexture_ST.y;
                // Sample textures based on modified UVs
                fixed4 overlayColor = tex2D(_OverlayTexture, overlayUV);
                fixed4 noiseColor = tex2D(_NoiseMaskTexture, noiseMaskUV);

                // Desaturation gets handled before everything else to not interfere with the overlay or noise masks
                if (_desaturate > 0) {
                    fixed4 desaturation = fixed4(0.3 * col.r, 0.6 * col.g, 0.1 * col.b, 1.0f); // DesaturationTable
                    col.r = col.r + _desaturationStrength * (desaturation - col.r) + _desaturationBrightness; // Desaturate but add a brightness value to ensure the scene doesn't get too dark.
                    col.g = col.g + _desaturationStrength * (desaturation - col.g) + _desaturationBrightness;
                    col.b = col.b + _desaturationStrength * (desaturation - col.b) + _desaturationBrightness;
                } 

                // Per color, affect it if the overlay mask color is <1 in that channel. Allows for 3 individual grayscale masks to affect each color independently
                if (overlayColor.r<1) { col.r = col.r - overlayColor.r * _overlayIntensity; } // Substract overlay color from pixel color based on intensity
                if (overlayColor.g<1) { col.g = col.g - overlayColor.g * _overlayIntensity; }
                if (overlayColor.b<1) { col.b = col.b - overlayColor.b * _overlayIntensity; }

                // Calculate noise from noise function for this pixel
                float c = fbm(noiseUV);
                // Filter by noise strength
                if (c >= _range) {
                    if (noiseColor.r < _noiseMaskIntensity) { col.r = col.r * c * noiseColor.r; } // Change pixel per color channel to noise color but mask it by noise mask
                    if (noiseColor.g < _noiseMaskIntensity) { col.g = col.g * c * noiseColor.g; }
                    if (noiseColor.b < _noiseMaskIntensity) { col.b = col.b * c * noiseColor.b; }
                }  

                return col;
            }
            ENDCG
        }
    }
}
