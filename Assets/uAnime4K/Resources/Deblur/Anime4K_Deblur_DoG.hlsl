// This shader is ported from https://github.com/bloc97/Anime4K
// Below is the original license

// MIT License

// Copyright (c) 2019-2021 bloc97
// All rights reserved.

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

//!DESC Anime4K-v3.2-Deblur-DoG-(HQ)-Luma
//!HOOK MAIN
//!BIND HOOKED
Texture2D HOOKED;
float4 HOOKED_TexelSize;
//!SAVE LINELUMA
//!COMPONENTS 1

float get_luma_Deblur_DoG_HQ_Luma_0(float4 rgba) {
	return dot(float4(0.299, 0.587, 0.114, 0.0), rgba);
}

float4 hook_Deblur_DoG_HQ_Luma_0(float2 uv) {
    return float4(get_luma_Deblur_DoG_HQ_Luma_0(HOOKED.Sample(linear_clamp_sampler, uv)), 0.0, 0.0, 0.0);
}
float4 Fragment_Deblur_DoG_HQ_Luma_0(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_DoG_HQ_Luma_0(texCoord);
}


//!DESC Anime4K-v3.2-Deblur-DoG-Kernel-X
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINELUMA
Texture2D LINELUMA;
float4 LINELUMA_TexelSize;
//!SAVE MMKERNEL
//!COMPONENTS 3

// #define L_tex LINELUMA_tex

float max3v_Deblur_DoG_Kernel_X_1(float a, float b, float c) {
	return max(max(a, b), c);
}
float min3v_Deblur_DoG_Kernel_X_1(float a, float b, float c) {
	return min(min(a, b), c);
}

float2 minmax3_Deblur_DoG_Kernel_X_1(float2 pos, float2 d) {
	float a = LINELUMA.Sample(linear_clamp_sampler, pos - d).x;
	float b = LINELUMA.Sample(linear_clamp_sampler, pos).x;
	float c = LINELUMA.Sample(linear_clamp_sampler, pos + d).x;
	
	return float2(min3v_Deblur_DoG_Kernel_X_1(a, b, c), max3v_Deblur_DoG_Kernel_X_1(a, b, c));
}

float lumGaussian7_Deblur_DoG_Kernel_X_1(float2 pos, float2 d) {
	float g = (LINELUMA.Sample(linear_clamp_sampler, pos - (d + d)).x + LINELUMA.Sample(linear_clamp_sampler, pos + (d + d)).x) * 0.06136;
	g = g + (LINELUMA.Sample(linear_clamp_sampler, pos - d).x + LINELUMA.Sample(linear_clamp_sampler, pos + d).x) * 0.24477;
	g = g + (LINELUMA.Sample(linear_clamp_sampler, pos).x) * 0.38774;
	
	return g;
}


float4 hook_Deblur_DoG_Kernel_X_1(float2 uv) {
    return float4(lumGaussian7_Deblur_DoG_Kernel_X_1(uv, float2(HOOKED_TexelSize.xy.x, 0.0)), minmax3_Deblur_DoG_Kernel_X_1(uv, float2(HOOKED_TexelSize.xy.x, 0.0)), 0.0);
}
float4 Fragment_Deblur_DoG_Kernel_X_1(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_DoG_Kernel_X_1(texCoord);
}



//!DESC Anime4K-v3.2-Deblur-DoG-Kernel-Y
//!HOOK MAIN
//!BIND HOOKED
//!BIND MMKERNEL
Texture2D MMKERNEL;
float4 MMKERNEL_TexelSize;
//!SAVE MMKERNEL
//!COMPONENTS 3

// #define L_tex MMKERNEL_tex

float max3v_Deblur_DoG_Kernel_Y_2(float a, float b, float c) {
	return max(max(a, b), c);
}
float min3v_Deblur_DoG_Kernel_Y_2(float a, float b, float c) {
	return min(min(a, b), c);
}

float2 minmax3_Deblur_DoG_Kernel_Y_2(float2 pos, float2 d) {
	float a0 = MMKERNEL.Sample(linear_clamp_sampler, pos - d).y;
	float b0 = MMKERNEL.Sample(linear_clamp_sampler, pos).y;
	float c0 = MMKERNEL.Sample(linear_clamp_sampler, pos + d).y;
	
	float a1 = MMKERNEL.Sample(linear_clamp_sampler, pos - d).z;
	float b1 = MMKERNEL.Sample(linear_clamp_sampler, pos).z;
	float c1 = MMKERNEL.Sample(linear_clamp_sampler, pos + d).z;
	
	return float2(min3v_Deblur_DoG_Kernel_Y_2(a0, b0, c0), max3v_Deblur_DoG_Kernel_Y_2(a1, b1, c1));
}

float lumGaussian7_Deblur_DoG_Kernel_Y_2(float2 pos, float2 d) {
	float g = (MMKERNEL.Sample(linear_clamp_sampler, pos - (d + d)).x + MMKERNEL.Sample(linear_clamp_sampler, pos + (d + d)).x) * 0.06136;
	g = g + (MMKERNEL.Sample(linear_clamp_sampler, pos - d).x + MMKERNEL.Sample(linear_clamp_sampler, pos + d).x) * 0.24477;
	g = g + (MMKERNEL.Sample(linear_clamp_sampler, pos).x) * 0.38774;
	
	return g;
}


float4 hook_Deblur_DoG_Kernel_Y_2(float2 uv) {
    return float4(lumGaussian7_Deblur_DoG_Kernel_Y_2(uv, float2(0.0, HOOKED_TexelSize.xy.y)), minmax3_Deblur_DoG_Kernel_Y_2(uv, float2(0.0, HOOKED_TexelSize.xy.y)), 0.0);
}
float4 Fragment_Deblur_DoG_Kernel_Y_2(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_DoG_Kernel_Y_2(texCoord);
}


//!DESC Anime4K-v3.2-Deblur-DoG-Apply
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINELUMA
//!BIND MMKERNEL

#define STRENGTH 0.6 //De-blur proportional strength, higher is sharper. However, it is better to tweak BLUR_CURVE instead to avoid ringing.
#define BLUR_CURVE 0.6 //De-blur power curve, lower is sharper. Good values are between 0.3 - 1. Values greater than 1 softens the image;
#define BLUR_THRESHOLD 0.1 //Value where curve kicks in, used to not de-blur already sharp edges. Only de-blur values that fall below this threshold.
#define NOISE_THRESHOLD 0.001 //Value where curve stops, used to not sharpen noise. Only de-blur values that fall above this threshold.

// #define L_tex LINELUMA_tex

float4 hook_Deblur_DoG_Apply_3(float2 uv) {
	float c = (LINELUMA.Sample(linear_clamp_sampler, uv).x - MMKERNEL.Sample(linear_clamp_sampler, uv).x) * STRENGTH;
	
	float t_range = BLUR_THRESHOLD - NOISE_THRESHOLD;
	
	float c_t = abs(c);
	if (c_t > NOISE_THRESHOLD) {
		c_t = (c_t - NOISE_THRESHOLD) / t_range;
		c_t = pow(c_t, BLUR_CURVE);
		c_t = c_t * t_range + NOISE_THRESHOLD;
		c_t = c_t * sign(c);
	} else {
		c_t = c;
	}
	
	float cc = clamp(c_t + LINELUMA.Sample(linear_clamp_sampler, uv).x, MMKERNEL.Sample(linear_clamp_sampler, uv).y, MMKERNEL.Sample(linear_clamp_sampler, uv).z) - LINELUMA.Sample(linear_clamp_sampler, uv).x;
	
	//This trick is only possible if the inverse Y->RGB matrix has 1 for every row... (which is the case for BT.709)
	//Otherwise we would need to convert RGB to YUV, modify Y then convert back to RGB.
	return HOOKED.Sample(linear_clamp_sampler, uv) + cc;
}
float4 Fragment_Deblur_DoG_Apply_3(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_DoG_Apply_3(texCoord);
}




