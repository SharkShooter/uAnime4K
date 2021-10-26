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

//!DESC Anime4K-v3.2-Denoise-Bilateral-Mode-Luma
//!HOOK MAIN
//!BIND HOOKED
Texture2D HOOKED;
float4 HOOKED_TexelSize;
//!SAVE LINELUMA
//!COMPONENTS 1

float get_luma_Denoise_Bilateral_Mode_Luma_0(float4 rgba) {
	return dot(float4(0.299, 0.587, 0.114, 0.0), rgba);
}

float4 hook_Denoise_Bilateral_Mode_Luma_0(float2 uv) {
    return float4(get_luma_Denoise_Bilateral_Mode_Luma_0(HOOKED.Sample(linear_clamp_sampler, uv)), 0.0, 0.0, 0.0);
}
float4 Fragment_Denoise_Bilateral_Mode_Luma_0(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Denoise_Bilateral_Mode_Luma_0(texCoord);
}


//!DESC Anime4K-v3.1-Denoise-Bilateral-Mode-Apply
//!HOOK MAIN
//!BIND HOOKED
//!BIND LINELUMA
Texture2D LINELUMA;
float4 LINELUMA_TexelSize;

#define INTENSITY_SIGMA 0.1 //Intensity window size, higher is stronger denoise, must be a positive real number
#define SPATIAL_SIGMA 1.0 //Spatial window size, higher is stronger denoise, must be a positive real number.
#define HISTOGRAM_REGULARIZATION 0.2 //Histogram regularization window size, higher values approximate a bilateral "closest-to-mean" filter.

#define INTENSITY_POWER_CURVE 1.0 //Intensity window power curve. Setting it to 0 will make the intensity window treat all intensities equally, while increasing it will make the window narrower in darker intensities and wider in brighter intensities.

#define KERNELSIZE int(max(int(SPATIAL_SIGMA), 1) * 2 + 1) //Kernel size, must be an positive odd integer.
#define KERNELHALFSIZE (int(KERNELSIZE/2)) //Half of the kernel size without remainder. Must be equal to trunc(KERNELSIZE/2).
#define KERNELLEN (KERNELSIZE * KERNELSIZE) //Total area of kernel. Must be equal to KERNELSIZE * KERNELSIZE.

#define GETOFFSET(i) float2((i % KERNELSIZE) - KERNELHALFSIZE, (i / KERNELSIZE) - KERNELHALFSIZE)

float gaussian_Denoise_Bilateral_Mode_Apply_1(float x, float s, float m) {
	float scaled = (x - m) / s;
	return exp(-0.5 * scaled * scaled);
}

float4 getMode_Denoise_Bilateral_Mode_Apply_1(float4 v[KERNELLEN], float w[KERNELLEN]) {
	float4 maxv = float4(0.0.xxxx);
	float maxw = 0.0;
	
	for (int i=0; i<KERNELLEN; i++) {
		if (w[i] >= maxw) {
			maxw = w[i];
			maxv = v[i];
		}
	}
	
	return maxv;
}

float4 hook_Denoise_Bilateral_Mode_Apply_1(float2 uv) {
	float4 histogram_v[KERNELLEN];
	float histogram_l[KERNELLEN];
	float histogram_w[KERNELLEN];
	float histogram_wn[KERNELLEN];
	
	float vc = LINELUMA.Sample(linear_clamp_sampler, uv).x;
	
	float is = pow(vc + 0.0001, INTENSITY_POWER_CURVE) * INTENSITY_SIGMA;
	float ss = SPATIAL_SIGMA;
	
	for (int i=0; i<KERNELLEN; i++) {
		float2 ipos = GETOFFSET(i);
		histogram_v[i] = HOOKED.Sample(linear_clamp_sampler, uv + ipos);
		histogram_l[i] = LINELUMA.Sample(linear_clamp_sampler, uv + ipos).x;
		histogram_w[i] = gaussian_Denoise_Bilateral_Mode_Apply_1(histogram_l[i], is, vc) * gaussian_Denoise_Bilateral_Mode_Apply_1(length(ipos), ss, 0.0);
		histogram_wn[i] = 0.0;
	}
	
	for (int i=0; i<KERNELLEN; i++) {
		histogram_wn[i] += gaussian_Denoise_Bilateral_Mode_Apply_1(0.0, HISTOGRAM_REGULARIZATION, 0.0) * histogram_w[i];
		for (int j=(i+1); j<KERNELLEN; j++) {
			float d = gaussian_Denoise_Bilateral_Mode_Apply_1(histogram_l[j], HISTOGRAM_REGULARIZATION, histogram_l[i]);
			histogram_wn[j] += d * histogram_w[i];
			histogram_wn[i] += d * histogram_w[j];
		}
	}
	
	return getMode_Denoise_Bilateral_Mode_Apply_1(histogram_v, histogram_wn);
}
float4 Fragment_Denoise_Bilateral_Mode_Apply_1(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Denoise_Bilateral_Mode_Apply_1(texCoord);
}

