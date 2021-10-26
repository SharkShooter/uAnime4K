Shader "Hidden/uAnime4K_Deblur_DoG"
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
            #pragma fragment Fragment_Deblur_DoG_HQ_Luma_0
            #include "../Common.hlsl"
            #include "Anime4K_Deblur_DoG.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Deblur_DoG_Kernel_X_1
            #include "../Common.hlsl"
            #include "Anime4K_Deblur_DoG.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Deblur_DoG_Kernel_Y_2
            #include "../Common.hlsl"
            #include "Anime4K_Deblur_DoG.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_Deblur_DoG_Apply_3
            #include "../Common.hlsl"
            #include "Anime4K_Deblur_DoG.hlsl"
            ENDCG
        }
    }
}
