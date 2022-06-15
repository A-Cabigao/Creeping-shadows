Shader "Custom/BlackOverlayShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" }

        Stencil
        {
            Ref 1
            Comp notequal
            Pass keep

        }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;

        sampler2D _MainTex;

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            // Albedo comes from a texture tinted by color
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

// Code credits to youtuber Nik Lever
