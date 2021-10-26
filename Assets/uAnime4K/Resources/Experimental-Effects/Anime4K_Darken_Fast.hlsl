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

//!DESC Anime4K-v3.2-Darken-DoG-(HQ)-Luma
//!HOOK MAIN
//!BIND HOOKED
Texture2D HOOKED;
float4 HOOKED_TexelSize;
//!SAVE LINELUMA
//!COMPONENTS 1

float get_luma_Darken_DoG_HQ_Luma_0(float4 rgba) {
	return dot(float4(0.299, 0.587, 0.114, 0.0), rgba);
}

float4 hook_Darken_DoG_HQ_Luma_0(float2 uv) {
    return float4(get_luma_Darken_DoG_HQ_Luma_0(HOOKED.Sample(linear_clamp_sampler, uv)), 0.0, 0.0, 0.0);
}
float4 Fragment_Darken_DoG_HQ_Luma_0(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Darken_DoG_HQ_Luma_0(texCoord);
}


//!DESC Anime4K-v3.2-Darken-DoG-(Fast)-Difference-X
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINELUMA
Texture2D LINELUMA;
float4 LINELUMA_TexelSize;
//!SAVE LINEKERNEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 1

#define SPATIAL_SIGMA (1.0 * float(HOOKED_TexelSize.zw.y) / 1080.0) //Spatial window size, must be a positive real number.

#define KERNELSIZE (max(int(ceil(SPATIAL_SIGMA * 2.0)), 1) * 2 + 1) //Kernel size, must be an positive odd integer.
#define KERNELHALFSIZE (int(KERNELSIZE/2)) //Half of the kernel size without remainder. Must be equal to trunc(KERNELSIZE/2).
#define KERNELLEN (KERNELSIZE * KERNELSIZE) //Total area of kernel. Must be equal to KERNELSIZE * KERNELSIZE.

float gaussian_Darken_DoG_Fast_Difference_X_1(float x, float s, float m) {
	float scaled = (x - m) / s;
	return exp(-0.5 * scaled * scaled);
}

float comp_gaussian_x_Darken_DoG_Fast_Difference_X_1(float2 uv) {

	float g = 0.0;
	float gn = 0.0;
	
	for (int i=0; i<KERNELSIZE; i++) {
		float di = float(i - KERNELHALFSIZE);
		float gf = gaussian_Darken_DoG_Fast_Difference_X_1(di, SPATIAL_SIGMA, 0.0);
		
		g = g + LINELUMA.Sample(linear_clamp_sampler, uv + float2(di, 0.0)).x * gf;
		gn = gn + gf;
		
	}
	
	return g / gn;
}

float4 hook_Darken_DoG_Fast_Difference_X_1(float2 uv) {
    return float4(comp_gaussian_x_Darken_DoG_Fast_Difference_X_1(uv), 0.0, 0.0, 0.0);
}
float4 Fragment_Darken_DoG_Fast_Difference_X_1(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Darken_DoG_Fast_Difference_X_1(texCoord);
}


//!DESC Anime4K-v3.2-Darken-DoG-(Fast)-Difference-Y
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINELUMA
//!BIND LINEKERNEL
Texture2D LINEKERNEL;
float4 LINEKERNEL_TexelSize;
//!SAVE LINEKERNEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 1

#define SPATIAL_SIGMA_Darken_DoG_Fast_Difference_Y_2 (0.5 * float(HOOKED_TexelSize.zw.y) / 1080.0) //Spatial window size, must be a positive real number.

#define KERNELSIZE (max(int(ceil(SPATIAL_SIGMA_Darken_DoG_Fast_Difference_Y_2 * 2.0)), 1) * 2 + 1) //Kernel size, must be an positive odd integer.
#define KERNELHALFSIZE (int(KERNELSIZE/2)) //Half of the kernel size without remainder. Must be equal to trunc(KERNELSIZE/2).
#define KERNELLEN (KERNELSIZE * KERNELSIZE) //Total area of kernel. Must be equal to KERNELSIZE * KERNELSIZE.

float gaussian_Darken_DoG_Fast_Difference_Y_2(float x, float s, float m) {
	float scaled = (x - m) / s;
	return exp(-0.5 * scaled * scaled);
}

float comp_gaussian_y_Darken_DoG_Fast_Difference_Y_2(float2 uv) {

	float g = 0.0;
	float gn = 0.0;
	
	for (int i=0; i<KERNELSIZE; i++) {
		float di = float(i - KERNELHALFSIZE);
		float gf = gaussian_Darken_DoG_Fast_Difference_Y_2(di, SPATIAL_SIGMA_Darken_DoG_Fast_Difference_Y_2, 0.0);
		
		g = g + LINEKERNEL.Sample(linear_clamp_sampler, uv + float2(0.0, di)).x * gf;
		gn = gn + gf;
		
	}
	
	return g / gn;
}

float4 hook_Darken_DoG_Fast_Difference_Y_2(float2 uv) {
    return float4(min(LINELUMA.Sample(linear_clamp_sampler, uv).x - comp_gaussian_y_Darken_DoG_Fast_Difference_Y_2(uv), 0.0), 0.0, 0.0, 0.0);
}
float4 Fragment_Darken_DoG_Fast_Difference_Y_2(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Darken_DoG_Fast_Difference_Y_2(texCoord);
}


//!DESC Anime4K-v3.2-Darken-DoG-(Fast)-Gaussian-X
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINEKERNEL
//!SAVE LINEKERNEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 1

#define SPATIAL_SIGMA_Darken_DoG_Fast_Gaussian_X_3 (0.5 * float(HOOKED_TexelSize.zw.y) / 1080.0) //Spatial window size, must be a positive real number.

#define KERNELSIZE (max(int(ceil(SPATIAL_SIGMA_Darken_DoG_Fast_Gaussian_X_3 * 2.0)), 1) * 2 + 1) //Kernel size, must be an positive odd integer.
#define KERNELHALFSIZE (int(KERNELSIZE/2)) //Half of the kernel size without remainder. Must be equal to trunc(KERNELSIZE/2).
#define KERNELLEN (KERNELSIZE * KERNELSIZE) //Total area of kernel. Must be equal to KERNELSIZE * KERNELSIZE.

float gaussian_Darken_DoG_Fast_Gaussian_X_3(float x, float s, float m) {
	float scaled = (x - m) / s;
	return exp(-0.5 * scaled * scaled);
}

float comp_gaussian_x_Darken_DoG_Fast_Gaussian_X_3(float2 uv) {

	float g = 0.0;
	float gn = 0.0;
	
	for (int i=0; i<KERNELSIZE; i++) {
		float di = float(i - KERNELHALFSIZE);
		float gf = gaussian_Darken_DoG_Fast_Gaussian_X_3(di, SPATIAL_SIGMA_Darken_DoG_Fast_Gaussian_X_3, 0.0);
		
		g = g + LINEKERNEL.Sample(linear_clamp_sampler, uv + float2(di, 0.0)).x * gf;
		gn = gn + gf;
		
	}
	
	return g / gn;
}

float4 hook_Darken_DoG_Fast_Gaussian_X_3(float2 uv) {
    return float4(comp_gaussian_x_Darken_DoG_Fast_Gaussian_X_3(uv), 0.0, 0.0, 0.0);
}
float4 Fragment_Darken_DoG_Fast_Gaussian_X_3(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Darken_DoG_Fast_Gaussian_X_3(texCoord);
}


//!DESC Anime4K-v3.2-Darken-DoG-(Fast)-Gaussian-Y
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINEKERNEL
//!SAVE LINEKERNEL
//!WIDTH HOOKED.w 2 /
//!HEIGHT HOOKED.h 2 /
//!COMPONENTS 1

#define SPATIAL_SIGMA_Darken_DoG_Fast_Gaussian_Y_4 (0.5 * float(HOOKED_TexelSize.zw.y) / 1080.0) //Spatial window size, must be a positive real number.

#define KERNELSIZE (max(int(ceil(SPATIAL_SIGMA_Darken_DoG_Fast_Gaussian_Y_4 * 2.0)), 1) * 2 + 1) //Kernel size, must be an positive odd integer.
#define KERNELHALFSIZE (int(KERNELSIZE/2)) //Half of the kernel size without remainder. Must be equal to trunc(KERNELSIZE/2).
#define KERNELLEN (KERNELSIZE * KERNELSIZE) //Total area of kernel. Must be equal to KERNELSIZE * KERNELSIZE.

float gaussian_Darken_DoG_Fast_Gaussian_Y_4(float x, float s, float m) {
	float scaled = (x - m) / s;
	return exp(-0.5 * scaled * scaled);
}

float comp_gaussian_y_Darken_DoG_Fast_Gaussian_Y_4(float2 uv) {

	float g = 0.0;
	float gn = 0.0;
	
	for (int i=0; i<KERNELSIZE; i++) {
		float di = float(i - KERNELHALFSIZE);
		float gf = gaussian_Darken_DoG_Fast_Gaussian_Y_4(di, SPATIAL_SIGMA_Darken_DoG_Fast_Gaussian_Y_4, 0.0);
		
		g = g + LINEKERNEL.Sample(linear_clamp_sampler, uv + float2(0.0, di)).x * gf;
		gn = gn + gf;
		
	}
	
	return g / gn;
}

float4 hook_Darken_DoG_Fast_Gaussian_Y_4(float2 uv) {
    return float4(comp_gaussian_y_Darken_DoG_Fast_Gaussian_Y_4(uv), 0.0, 0.0, 0.0);
}
float4 Fragment_Darken_DoG_Fast_Gaussian_Y_4(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Darken_DoG_Fast_Gaussian_Y_4(texCoord);
}



//!DESC Anime4K-v3.2-Darken-DoG-(Fast)-Upsample
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINEKERNEL

#define STRENGTH 1.5 //Line darken proportional strength, higher is darker.

float4 hook_Darken_DoG_Fast_Upsample_5(float2 uv) {
	//This trick is only possible if the inverse Y->RGB matrix has 1 for every row... (which is the case for BT.709)
	//Otherwise we would need to convert RGB to YUV, modify Y then convert back to RGB.
    return HOOKED.Sample(linear_clamp_sampler, uv) + (LINEKERNEL.Sample(linear_clamp_sampler, uv).x * STRENGTH);
}
float4 Fragment_Darken_DoG_Fast_Upsample_5(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Darken_DoG_Fast_Upsample_5(texCoord);
}


