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

//!DESC Anime4K-v3.2-Denoise-Bilateral-Mean
//!HOOK MAIN
//!BIND HOOKED
Texture2D HOOKED;
float4 HOOKED_TexelSize;

#define INTENSITY_SIGMA 0.1 //Intensity window size, higher is stronger denoise, must be a positive real number
#define SPATIAL_SIGMA 1.0 //Spatial window size, higher is stronger denoise, must be a positive real number.

#define INTENSITY_POWER_CURVE 1.0 //Intensity window power curve. Setting it to 0 will make the intensity window treat all intensities equally, while increasing it will make the window narrower in darker intensities and wider in brighter intensities.

#define KERNELSIZE (max(int(ceil(SPATIAL_SIGMA * 2.0)), 1) * 2 + 1) //Kernel size, must be an positive odd integer.
#define KERNELHALFSIZE (int(KERNELSIZE/2)) //Half of the kernel size without remainder. Must be equal to trunc(KERNELSIZE/2).
#define KERNELLEN (KERNELSIZE * KERNELSIZE) //Total area of kernel. Must be equal to KERNELSIZE * KERNELSIZE.

#define GETOFFSET(i) float2((i % KERNELSIZE) - KERNELHALFSIZE, (i / KERNELSIZE) - KERNELHALFSIZE)

float4 gaussian_vec_Denoise_Bilateral_Mean_0(float4 x, float4 s, float4 m) {
	float4 scaled = (x - m) / s;
	return exp(-0.5 * scaled * scaled);
}

float gaussian_Denoise_Bilateral_Mean_0(float x, float s, float m) {
	float scaled = (x - m) / s;
	return exp(-0.5 * scaled * scaled);
}

float4 hook_Denoise_Bilateral_Mean_0(float2 uv) {
	float4 sum = float4(0.0.xxxx);
	float4 n = float4(0.0.xxxx);
	
	float4 vc = HOOKED.Sample(linear_clamp_sampler, uv);
	
	float4 is = pow(vc + 0.0001, float4(INTENSITY_POWER_CURVE.xxxx)) * INTENSITY_SIGMA;
	float ss = SPATIAL_SIGMA;
	
	for (int i=0; i<KERNELLEN; i++) {
		float2 ipos = GETOFFSET(i);
		float4 v = HOOKED.Sample(linear_clamp_sampler, uv + ipos);
		float4 d = gaussian_vec_Denoise_Bilateral_Mean_0(v, is, vc) * gaussian_Denoise_Bilateral_Mean_0(length(ipos), ss, 0.0);
		sum += d * v;
		n += d;
	}
	
	return sum / n;
}
float4 Fragment_Denoise_Bilateral_Mean_0(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_Denoise_Bilateral_Mean_0(texCoord);
}

