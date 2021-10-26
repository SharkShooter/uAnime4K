Shader "Hidden/uAnime4K_AutoDownscalePre_x4"
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
            #pragma fragment Fragment_AutoDownscalePre_x4_0
            #include "../Common.hlsl"
            #include "Anime4K_AutoDownscalePre_x4.hlsl"
            ENDCG
        }
    }
}
