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

//!DESC Anime4K-v3.2-Deblur-Original-Luma
//!HOOK MAIN
//!BIND HOOKED
Texture2D HOOKED;
float4 HOOKED_TexelSize;
//!SAVE LINELUMA
//!COMPONENTS 1

float get_luma_Deblur_Original_Luma_0(float4 rgba) {
	return dot(float4(0.299, 0.587, 0.114, 0.0), rgba);
}

float4 hook_Deblur_Original_Luma_0(float2 uv) {
    return float4(get_luma_Deblur_Original_Luma_0(HOOKED.Sample(linear_clamp_sampler, uv)), 0.0, 0.0, 0.0);
}
float4 Fragment_Deblur_Original_Luma_0(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_Original_Luma_0(texCoord);
}


//!DESC Anime4K-v3.2-Deblur-Original-Kernel-X
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINELUMA
Texture2D LINELUMA;
float4 LINELUMA_TexelSize;
//!SAVE LUMAD
//!WIDTH MAIN.w 2 *
//!HEIGHT MAIN.h 2 *
//!COMPONENTS 2

float4 hook_Deblur_Original_Kernel_X_1(float2 uv) {
	float2 d = HOOKED_TexelSize.xy;
	
	//[tl  t tr]
	//[ l  c  r]
	//[bl  b br]
	float l = LINELUMA.Sample(linear_clamp_sampler, uv + float2(-d.x, 0.0)).x;
	float c = LINELUMA.Sample(linear_clamp_sampler, uv).x;
	float r = LINELUMA.Sample(linear_clamp_sampler, uv + float2(d.x, 0.0)).x;
	
	
	//Horizontal Gradient
	//[-1  0  1]
	//[-2  0  2]
	//[-1  0  1]
	float xgrad = (-l + r);
	
	//Vertical Gradient
	//[-1 -2 -1]
	//[ 0  0  0]
	//[ 1  2  1]
	float ygrad = (l + c + c + r);
	
	//Computes the luminance's gradient
	return float4(xgrad, ygrad, 0.0, 0.0);
}
float4 Fragment_Deblur_Original_Kernel_X_1(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_Original_Kernel_X_1(texCoord);
}



//!DESC Anime4K-v3.2-Deblur-Original-Kernel-Y
//!HOOK MAIN
//!BIND HOOKED
//!BIND LUMAD
Texture2D LUMAD;
float4 LUMAD_TexelSize;
//!SAVE LUMAD
//!WIDTH MAIN.w 2 *
//!HEIGHT MAIN.h 2 *
//!COMPONENTS 2


/* --------------------- SETTINGS --------------------- */

//Strength of edge refinement, good values are between 0.2 and 4
#define REFINE_STRENGTH 1.0


/* --- MODIFY THESE SETTINGS BELOW AT YOUR OWN RISK --- */

//Bias of the refinement function, good values are between 0 and 1
#define REFINE_BIAS 0.0

//Polynomial fit obtained by minimizing MSE error on image
#define P5 ( 11.68129591)
#define P4 (-42.46906057)
#define P3 ( 60.28286266)
#define P2 (-41.84451327)
#define P1 ( 14.05517353)
#define P0 (-1.081521930)

/* ----------------- END OF SETTINGS ----------------- */

float power_function_Deblur_Original_Kernel_Y_2(float x) {
	float x2 = x * x;
	float x3 = x2 * x;
	float x4 = x2 * x2;
	float x5 = x2 * x3;
	
	return P5*x5 + P4*x4 + P3*x3 + P2*x2 + P1*x + P0;
}

float4 hook_Deblur_Original_Kernel_Y_2(float2 uv) {
	float2 d = HOOKED_TexelSize.xy;
	
	//[tl  t tr]
	//[ l cc  r]
	//[bl  b br]
	float tx = LUMAD.Sample(linear_clamp_sampler, uv + float2(0.0, -d.y)).x;
	float cx = LUMAD.Sample(linear_clamp_sampler, uv).x;
	float bx = LUMAD.Sample(linear_clamp_sampler, uv + float2(0.0, d.y)).x;
	
	
	float ty = LUMAD.Sample(linear_clamp_sampler, uv + float2(0.0, -d.y)).y;
	//float cy = LUMAD.Sample(linear_clamp_sampler, uv).y;
	float by = LUMAD.Sample(linear_clamp_sampler, uv + float2(0.0, d.y)).y;
	
	
	//Horizontal Gradient
	//[-1  0  1]
	//[-2  0  2]
	//[-1  0  1]
	float xgrad = (tx + cx + cx + bx);
	
	//Vertical Gradient
	//[-1 -2 -1]
	//[ 0  0  0]
	//[ 1  2  1]
	float ygrad = (-ty + by);
	
	//Computes the luminance's gradient
	float sobel_norm = clamp(sqrt(xgrad * xgrad + ygrad * ygrad), 0.0, 1.0);
	
	float dval = clamp(power_function_Deblur_Original_Kernel_Y_2(clamp(sobel_norm, 0.0, 1.0)) * REFINE_STRENGTH + REFINE_BIAS, 0.0, 1.0);
	
	return float4(sobel_norm, dval, 0.0, 0.0);
}
float4 Fragment_Deblur_Original_Kernel_Y_2(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_Original_Kernel_Y_2(texCoord);
}


//!DESC Anime4K-v3.2-Deblur-Original-Kernel-X
//!HOOK MAIN
//!BIND HOOKED
//!BIND LUMAD
//!SAVE LUMAMM
//!WIDTH MAIN.w 2 *
//!HEIGHT MAIN.h 2 *
//!COMPONENTS 2


float4 hook_Deblur_Original_Kernel_X_3(float2 uv) {
	float2 d = HOOKED_TexelSize.xy;
	
	if (LUMAD.Sample(linear_clamp_sampler, uv).y < 0.1) {
		return float4(0.0.xxxx);
	}
	
	//[tl  t tr]
	//[ l  c  r]
	//[bl  b br]
	float l = LUMAD.Sample(linear_clamp_sampler, uv + float2(-d.x, 0.0)).x;
	float c = LUMAD.Sample(linear_clamp_sampler, uv).x;
	float r = LUMAD.Sample(linear_clamp_sampler, uv + float2(d.x, 0.0)).x;
	
	//Horizontal Gradient
	//[-1  0  1]
	//[-2  0  2]
	//[-1  0  1]
	float xgrad = (-l + r);
	
	//Vertical Gradient
	//[-1 -2 -1]
	//[ 0  0  0]
	//[ 1  2  1]
	float ygrad = (l + c + c + r);
	
	
	return float4(xgrad, ygrad, 0.0, 0.0);
}
float4 Fragment_Deblur_Original_Kernel_X_3(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_Original_Kernel_X_3(texCoord);
}



//!DESC Anime4K-v3.2-Deblur-Original-Kernel-Y
//!HOOK MAIN
//!BIND HOOKED
//!BIND LUMAD
//!BIND LUMAMM
Texture2D LUMAMM;
float4 LUMAMM_TexelSize;
//!SAVE LUMAMM
//!WIDTH MAIN.w 2 *
//!HEIGHT MAIN.h 2 *
//!COMPONENTS 2

float4 hook_Deblur_Original_Kernel_Y_4(float2 uv) {
	float2 d = HOOKED_TexelSize.xy;
	
	if (LUMAD.Sample(linear_clamp_sampler, uv).y < 0.1) {
		return float4(0.0.xxxx);
	}
	
	//[tl  t tr]
	//[ l cc  r]
	//[bl  b br]
	float tx = LUMAMM.Sample(linear_clamp_sampler, uv + float2(0.0, -d.y)).x;
	float cx = LUMAMM.Sample(linear_clamp_sampler, uv).x;
	float bx = LUMAMM.Sample(linear_clamp_sampler, uv + float2(0.0, d.y)).x;
	
	float ty = LUMAMM.Sample(linear_clamp_sampler, uv + float2(0.0, -d.y)).y;
	//float cy = LUMAMM.Sample(linear_clamp_sampler, uv).y;
	float by = LUMAMM.Sample(linear_clamp_sampler, uv + float2(0.0, d.y)).y;
	
	//Horizontal Gradient
	//[-1  0  1]
	//[-2  0  2]
	//[-1  0  1]
	float xgrad = (tx + cx + cx + bx);
	
	//Vertical Gradient
	//[-1 -2 -1]
	//[ 0  0  0]
	//[ 1  2  1]
	float ygrad = (-ty + by);
	
	float norm = sqrt(xgrad * xgrad + ygrad * ygrad);
	if (norm <= 0.001) {
		xgrad = 0.0;
		ygrad = 0.0;
		norm = 1.0;
	}
	
	return float4(xgrad/norm, ygrad/norm, 0.0, 0.0);
}
float4 Fragment_Deblur_Original_Kernel_Y_4(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_Original_Kernel_Y_4(texCoord);
}



//!DESC Anime4K-v3.2-Deblur-Original-Apply
//!HOOK MAIN
//!BIND HOOKED
//!BIND LUMAD
//!BIND LUMAMM
//!WIDTH MAIN.w 2 *
//!HEIGHT MAIN.h 2 *
//!SAVE RESAMPLED


float4 hook_Deblur_Original_Apply_5(float2 uv) {
	float2 d = HOOKED_TexelSize.xy;
	
	float dval = LUMAD.Sample(linear_clamp_sampler, uv).y;
	if (dval < 0.1) {
		return HOOKED.Sample(linear_clamp_sampler, uv);
	}
	
	float4 dc = LUMAMM.Sample(linear_clamp_sampler, uv);
	if (abs(dc.x + dc.y) <= 0.0001) {
		return HOOKED.Sample(linear_clamp_sampler, uv);
	}
	
	float xpos = -sign(dc.x);
	float ypos = -sign(dc.y);
	
	float4 xval = HOOKED.Sample(linear_clamp_sampler, uv + float2(d.x * xpos, 0.0));
	float4 yval = HOOKED.Sample(linear_clamp_sampler, uv + float2(0.0, d.y * ypos));
	
	float xyratio = abs(dc.x) / (abs(dc.x) + abs(dc.y));
	
	float4 avg = xyratio * xval + (1.0 - xyratio) * yval;
	
	return avg * dval + HOOKED.Sample(linear_clamp_sampler, uv) * (1.0 - dval);
	
}
float4 Fragment_Deblur_Original_Apply_5(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_Original_Apply_5(texCoord);
}


//!DESC Anime4K-v3.2-Deblur-Original-Resample
//!HOOK MAIN
//!BIND HOOKED
//!BIND RESAMPLED
Texture2D RESAMPLED;
float4 RESAMPLED_TexelSize;

float4 hook_Deblur_Original_Resample_6(float2 uv) {
	return RESAMPLED.Sample(linear_clamp_sampler, uv);
}
float4 Fragment_Deblur_Original_Resample_6(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Deblur_Original_Resample_6(texCoord);
}

