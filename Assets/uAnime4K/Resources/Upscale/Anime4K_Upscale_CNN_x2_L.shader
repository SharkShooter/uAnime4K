Shader "Hidden/uAnime4K_Upscale_CNN_x2_L"
{
    Properties
    {
        _MainTex("", 2D) = ""{}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Conv_4x3x3x3_0
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Conv_4x3x3x3_1
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Conv_4x3x3x16_2
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Conv_4x3x3x16_3
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Conv_4x3x3x16_4
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Conv_4x3x3x16_5
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Conv_4x3x3x16_6
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Conv_4x3x3x16_7
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Conv_4x3x3x16_8
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Upscale_CNN_x2_L_Depth_to_Space_9
            #include "../Common.hlsl"
            #include "Anime4K_Upscale_CNN_x2_L.hlsl"
            ENDCG
        }
    }
}
