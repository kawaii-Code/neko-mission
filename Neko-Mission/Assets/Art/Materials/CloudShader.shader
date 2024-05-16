Shader "Custom/CloudShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NoiseTex ("Noise", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _MagicNumber("Magic", float) = 1
        _Rotation("RotationAxis", vector ) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert //alpha:fade
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _MagicNumber;
        
        
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        float2 unity_gradientNoise_dir(float2 p)
    {
        p = p % 289;
        float x = (34 * p.x + 1) * p.x % 289 + p.y;
        x = (34 * x + 1) * x % 289;
        x = frac(x / 41) * 2 - 1;
        return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
    }

    float unity_gradientNoise(float2 p)
    {
        float2 ip = floor(p);
        float2 fp = frac(p);
        float d00 = dot(unity_gradientNoise_dir(ip), fp);
        float d01 = dot(unity_gradientNoise_dir(ip + float2(0, 1)), fp - float2(0, 1));
        float d10 = dot(unity_gradientNoise_dir(ip + float2(1, 0)), fp - float2(1, 0));
        float d11 = dot(unity_gradientNoise_dir(ip + float2(1, 1)), fp - float2(1, 1));
        fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
        return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
    }

    void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
    {
        Out = unity_gradientNoise(UV * Scale) + 0.5;
    }
        
        void Unity_RotateAboutAxis_Degrees_float(float3 In, float3 Axis, float Rotation, out float3 Out)
        {
            Rotation = radians(Rotation);
            float s = sin(Rotation);
            float c = cos(Rotation);
            float one_minus_c = 1.0 - c;

            Axis = normalize(Axis);
            float3x3 rot_mat = 
            {   one_minus_c * Axis.x * Axis.x + c, one_minus_c * Axis.x * Axis.y - Axis.z * s, one_minus_c * Axis.z * Axis.x + Axis.y * s,
                one_minus_c * Axis.x * Axis.y + Axis.z * s, one_minus_c * Axis.y * Axis.y + c, one_minus_c * Axis.y * Axis.z - Axis.x * s,
                one_minus_c * Axis.z * Axis.x - Axis.y * s, one_minus_c * Axis.y * Axis.z + Axis.x * s, one_minus_c * Axis.z * Axis.z + c
            };
            Out = mul(rot_mat,  In);
        }
        
        float rand(float2 co){
            return frac(sin(dot(co, float2(12.9898f, 78.233f))) * 43758.5453);
        }
        
        void vert (inout appdata_full v) {
            //if (v.texcoord1.x != 0)
            {
                //Unity_RotateAboutAxis_Degrees_float(v.vertex.xyz, v.normal, _Time, v.vertex.xyz);
                //v.vertex.xz += tex2Dlod(_NoiseTex, v.vertex).xz * v.normal * _MagicNumber;
                float CRUTCH = 0;
                float crutch = 0;
                Unity_GradientNoise_float(v.vertex.xy,_MagicNumber, CRUTCH);
                Unity_GradientNoise_float(v.vertex.xy,_MagicNumber, crutch);
                v.vertex.x += CRUTCH * v.normal * 1.6;
                v.vertex.z += crutch * v.normal * 1.6;
                //v.vertex *= v.normal;
                //v.texcoord1.x = 0;
            }
            
            //v.vertex.z = rand(v.vertex.z);
            //v.vertex.y += sin(v.vertex.z + _Time.y);
        }
        
        /*v2f vert (appdata v)
            {
                
                v2f o;
                v.vertex.y += sin(v.vertex.z + _Time.y);
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
        */
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
