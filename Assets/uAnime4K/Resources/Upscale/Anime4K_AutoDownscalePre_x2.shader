Shader "Hidden/uAnime4K_AutoDownscalePre_x2"
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
            #pragma fragment Fragment_AutoDownscalePre_x2_0
            #include "../Common.hlsl"
            #include "Anime4K_AutoDownscalePre_x2.hlsl"
            ENDCG
        }
    }
}
