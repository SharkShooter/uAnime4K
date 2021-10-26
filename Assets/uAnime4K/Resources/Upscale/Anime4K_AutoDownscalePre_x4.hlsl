// This shader is ported from https://github.com/bloc97/Anime4K
// Below is the original license

// This is free and unencumbered software released into the public domain.

// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.

// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

// For more information, please refer to <https://unlicense.org>

//!DESC Anime4K-v3.2-AutoDownscalePre-x4
//!HOOK MAIN
//!BIND HOOKED
Texture2D HOOKED;
float4 HOOKED_TexelSize;
//!BIND NATIVE
Texture2D NATIVE;
float4 NATIVE_TexelSize;
//!WHEN OUTPUT.w NATIVE.w / 4.0 < OUTPUT.h NATIVE.h / 4.0 < * OUTPUT.w NATIVE.w / 2.4 > OUTPUT.h NATIVE.h / 2.4 > * *
//!WIDTH OUTPUT.w 2 /
//!HEIGHT OUTPUT.h 2 /

float4 hook_AutoDownscalePre_x4_0(float2 uv) {
	return HOOKED.Sample(linear_clamp_sampler, uv);
}
float4 Fragment_AutoDownscalePre_x4_0(float4 position : SV_Position, float2 texCoord : TEXCOORD) : SV_Target
{
    return hook_AutoDownscalePre_x4_0(texCoord);
}

