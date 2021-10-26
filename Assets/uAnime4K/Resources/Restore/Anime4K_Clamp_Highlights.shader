Shader "Hidden/uAnime4K_Clamp_Highlights"
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
            #pragma fragment Fragment_De_Ring_Compute_Statistics_0
            #include "../Common.hlsl"
            #include "Anime4K_Clamp_Highlights.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_De_Ring_Compute_Statistics_1
            #include "../Common.hlsl"
            #include "Anime4K_Clamp_Highlights.hlsl"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment_De_Ring_Clamp_2
            #include "../Common.hlsl"
            #include "Anime4K_Clamp_Highlights.hlsl"
            ENDCG
        }
    }
}
