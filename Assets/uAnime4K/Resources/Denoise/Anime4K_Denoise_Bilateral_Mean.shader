Shader "Hidden/uAnime4K_Denoise_Bilateral_Mean"
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
            #pragma fragment Fragment_Denoise_Bilateral_Mean_0
            #include "../Common.hlsl"
            #include "Anime4K_Denoise_Bilateral_Mean.hlsl"
            ENDCG
        }
    }
}
