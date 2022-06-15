Shader "Custom/FovShader"
{
    Properties
    {
    }

    SubShader
    {
        Tags { "Queue" = "Geometry-1" }

        ColorMask 0
        ZWrite off

        Stencil
        {
            Ref 1
            Comp always
            Pass replace
		}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert

        struct Input
        {
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Alpha = fixed4(1,1,1,1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}

// Code credits to youtuber Nik Lever