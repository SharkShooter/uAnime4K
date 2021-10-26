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

//!DESC Anime4K-v3.2-Thin-(Fast)-Luma
//!HOOK MAIN
//!BIND HOOKED
Texture2D HOOKED;
float4 HOOKED_TexelSize;
//!SAVE LINELUMA
//!COMPONENTS 1

float get_luma_Thin_Fast_Luma_0(float4 rgba) {
	return dot(float4(0.299, 0.587, 0.114, 0.0), rgba);
}

float4 hook_Thin_Fast_Luma_0(float2 uv) {
    return float4(get_luma_Thin_Fast_Luma_0(HOOKED.Sample(linear_clamp_sampler, uv)), 0.0, 0.0, 0.0);
}
float4 Fragment_Thin_Fast_Luma_0(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Thin_Fast_Luma_0(texCoord);
}


//!DESC Anime4K-v3.2-Thin-(Fast)-Sobel-X
//!HOOK MAIN
//!BIND LINELUMA
Texture2D LINELUMA;
float4 LINELUMA_TexelSize;
//!SAVE LINESOBEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 2

float4 hook_Thin_Fast_Sobel_X_1(float2 uv) {
	float l = LINELUMA.Sample(linear_clamp_sampler, uv + float2(-1.0, 0.0)).x;
	float c = LINELUMA.Sample(linear_clamp_sampler, uv).x;
	float r = LINELUMA.Sample(linear_clamp_sampler, uv + float2(1.0, 0.0)).x;
	
	float xgrad = (-l + r);
	float ygrad = (l + c + c + r);
	
	return float4(xgrad, ygrad, 0.0, 0.0);
}
float4 Fragment_Thin_Fast_Sobel_X_1(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Thin_Fast_Sobel_X_1(texCoord);
}



//!DESC Anime4K-v3.2-Thin-(Fast)-Sobel-Y
//!HOOK MAIN
//!BIND LINESOBEL
Texture2D LINESOBEL;
float4 LINESOBEL_TexelSize;
//!SAVE LINESOBEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 1

float4 hook_Thin_Fast_Sobel_Y_2(float2 uv) {
	float tx = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.0, -0.5)).x;
	float cx = LINESOBEL.Sample(linear_clamp_sampler, uv).x;
	float bx = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.0, 0.5)).x;
	
	float ty = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.0, -0.5)).y;
	float by = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.0, 0.5)).y;
	
	float xgrad = (tx + cx + cx + bx) / 8.0;
	
	float ygrad = (-ty + by) / 8.0;
	
	//Computes the luminance's gradient
	float norm = sqrt(xgrad * xgrad + ygrad * ygrad);
	return float4(pow(norm, 0.7).xxxx);
}
float4 Fragment_Thin_Fast_Sobel_Y_2(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Thin_Fast_Sobel_Y_2(texCoord);
}



//!DESC Anime4K-v3.2-Thin-(Fast)-Gaussian-X
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINESOBEL
//!SAVE LINESOBEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 1

#define SPATIAL_SIGMA (1.0 * float(HOOKED_TexelSize.zw.y) / 1080.0) //Spatial window size, must be a positive real number.

#define KERNELSIZE (max(int(ceil(SPATIAL_SIGMA * 2.0)), 1) * 2 + 1) //Kernel size, must be an positive odd integer.
#define KERNELHALFSIZE (int(KERNELSIZE/2)) //Half of the kernel size without remainder. Must be equal to trunc(KERNELSIZE/2).
#define KERNELLEN (KERNELSIZE * KERNELSIZE) //Total area of kernel. Must be equal to KERNELSIZE * KERNELSIZE.

float gaussian_Thin_Fast_Gaussian_X_3(float x, float s, float m) {
	float scaled = (x - m) / s;
	return exp(-0.5 * scaled * scaled);
}

float comp_gaussian_x_Thin_Fast_Gaussian_X_3(float2 uv) {

	float g = 0.0;
	float gn = 0.0;
	
	for (int i=0; i<KERNELSIZE; i++) {
		float di = float(i - KERNELHALFSIZE);
		float gf = gaussian_Thin_Fast_Gaussian_X_3(di, SPATIAL_SIGMA, 0.0);
		
		g = g + LINESOBEL.Sample(linear_clamp_sampler, uv + float2(di, 0.0)).x * gf;
		gn = gn + gf;
		
	}
	
	return g / gn;
}

float4 hook_Thin_Fast_Gaussian_X_3(float2 uv) {
    return float4(comp_gaussian_x_Thin_Fast_Gaussian_X_3(uv), 0.0, 0.0, 0.0);
}
float4 Fragment_Thin_Fast_Gaussian_X_3(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Thin_Fast_Gaussian_X_3(texCoord);
}



//!DESC Anime4K-v3.2-Thin-(Fast)-Gaussian-Y
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINESOBEL
//!SAVE LINESOBEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 1

#define SPATIAL_SIGMA (1.0 * float(HOOKED_TexelSize.zw.y) / 1080.0) //Spatial window size, must be a positive real number.

#define KERNELSIZE (max(int(ceil(SPATIAL_SIGMA * 2.0)), 1) * 2 + 1) //Kernel size, must be an positive odd integer.
#define KERNELHALFSIZE (int(KERNELSIZE/2)) //Half of the kernel size without remainder. Must be equal to trunc(KERNELSIZE/2).
#define KERNELLEN (KERNELSIZE * KERNELSIZE) //Total area of kernel. Must be equal to KERNELSIZE * KERNELSIZE.

float gaussian_Thin_Fast_Gaussian_Y_4(float x, float s, float m) {
	float scaled = (x - m) / s;
	return exp(-0.5 * scaled * scaled);
}

float comp_gaussian_y_Thin_Fast_Gaussian_Y_4(float2 uv) {

	float g = 0.0;
	float gn = 0.0;
	
	for (int i=0; i<KERNELSIZE; i++) {
		float di = float(i - KERNELHALFSIZE);
		float gf = gaussian_Thin_Fast_Gaussian_Y_4(di, SPATIAL_SIGMA, 0.0);
		
		g = g + LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.0, di)).x * gf;
		gn = gn + gf;
		
	}
	
	return g / gn;
}

float4 hook_Thin_Fast_Gaussian_Y_4(float2 uv) {
    return float4(comp_gaussian_y_Thin_Fast_Gaussian_Y_4(uv), 0.0, 0.0, 0.0);
}
float4 Fragment_Thin_Fast_Gaussian_Y_4(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Thin_Fast_Gaussian_Y_4(texCoord);
}


//!DESC Anime4K-v3.2-Thin-(Fast)-Kernel-X
//!HOOK MAIN
//!BIND LINESOBEL
//!SAVE LINESOBEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 2

float4 hook_Thin_Fast_Kernel_X_5(float2 uv) {
	float l = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(-0.5, 0.0)).x;
	float c = LINESOBEL.Sample(linear_clamp_sampler, uv).x;
	float r = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.5, 0.0)).x;
	
	float xgrad = (-l + r);
	float ygrad = (l + c + c + r);
	
	return float4(xgrad, ygrad, 0.0, 0.0);
}
float4 Fragment_Thin_Fast_Kernel_X_5(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Thin_Fast_Kernel_X_5(texCoord);
}



//!DESC Anime4K-v3.2-Thin-(Fast)-Kernel-Y
//!HOOK MAIN
//!BIND LINESOBEL
//!SAVE LINESOBEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 2

float4 hook_Thin_Fast_Kernel_Y_6(float2 uv) {
	float tx = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.0, -0.5)).x;
	float cx = LINESOBEL.Sample(linear_clamp_sampler, uv).x;
	float bx = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.0, 0.5)).x;
	
	float ty = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.0, -0.5)).y;
	float by = LINESOBEL.Sample(linear_clamp_sampler, uv + float2(0.0, 0.5)).y;
	
	float xgrad = (tx + cx + cx + bx) / 8.0;
	
	float ygrad = (-ty + by) / 8.0;
	
	//Computes the luminance's gradient
	return float4(xgrad, ygrad, 0.0, 0.0);
}
float4 Fragment_Thin_Fast_Kernel_Y_6(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Thin_Fast_Kernel_Y_6(texCoord);
}



//!DESC Anime4K-v3.2-Thin-(Fast)-Warp
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINESOBEL

#define STRENGTH 0.6 //Strength of warping for each iteration
#define ITERATIONS 1 //Number of iterations for the forwards solver, decreasing strength and increasing iterations improves quality at the cost of speed.

float4 hook_Thin_Fast_Warp_7(float2 uv) {
	float2 d = HOOKED_TexelSize.xy;
	
	float relstr = HOOKED_TexelSize.zw.y / 1080.0 * STRENGTH;
	
	float2 pos = uv;
	for (int i=0; i<ITERATIONS; i++) {
		float2 dn = LINESOBEL.Sample(linear_clamp_sampler, pos).xy;
		float2 dd = (dn / (length(dn) + 0.01)) * d * relstr; //Quasi-normalization for large vectors, avoids divide by zero
		pos -= dd;
	}
	
	return HOOKED.Sample(linear_clamp_sampler, pos);
	
}
float4 Fragment_Thin_Fast_Warp_7(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Thin_Fast_Warp_7(texCoord);
}

