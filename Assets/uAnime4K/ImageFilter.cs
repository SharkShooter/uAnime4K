using UnityEngine;

namespace uAnime4K
{
    public static class ImageFilter
    {
        static Material _material_Clamp_Highlights;
        static Material _material_Upscale_Denoise_CNN_x2_S;
        static Material _material_Upscale_Denoise_CNN_x2_M;
        static Material _material_Upscale_Denoise_CNN_x2_VL;
        static Material _material_Upscale_CNN_x2_S;
        static Material _material_Upscale_CNN_x2_M;
        static Material _material_Upscale_CNN_x2_VL;
        static Material _material_Restore_CNN_S;
        static Material _material_Restore_CNN_M;
        static Material _material_Restore_CNN_VL;
        static Material _material_Restore_CNN_Soft_S;
        static Material _material_Restore_CNN_Soft_M;
        static Material _material_Restore_CNN_Soft_VL;

        public static void Clamp_Highlights(Texture source, RenderTexture destination) {
            if(_material_Clamp_Highlights == null)
                _material_Clamp_Highlights = new Material(Shader.Find("Hidden/uAnime4K_Clamp_Highlights"));

            var tempRT_STATSMAX1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_STATSMAX2 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);

            _material_Clamp_Highlights.SetTexture("HOOKED", source);
            Graphics.Blit(source, tempRT_STATSMAX1, _material_Clamp_Highlights, 0);
            _material_Clamp_Highlights.SetTexture("STATSMAX", tempRT_STATSMAX1);
            Graphics.Blit(source, tempRT_STATSMAX2, _material_Clamp_Highlights, 1);
            _material_Clamp_Highlights.SetTexture("STATSMAX", tempRT_STATSMAX2);
            Graphics.Blit(source, destination, _material_Clamp_Highlights, 2);

            RenderTexture.ReleaseTemporary(tempRT_STATSMAX1);
            RenderTexture.ReleaseTemporary(tempRT_STATSMAX2);
        }

        public static void Upscale_Denoise_CNN_x2_S(Texture source, RenderTexture destination)
        {
            if(_material_Upscale_Denoise_CNN_x2_S == null)
                _material_Upscale_Denoise_CNN_x2_S = new Material(Shader.Find("Hidden/uAnime4K_Upscale_Denoise_CNN_x2_S"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Upscale_Denoise_CNN_x2_S, 0);
            _material_Upscale_Denoise_CNN_x2_S.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Upscale_Denoise_CNN_x2_S, 1);
            _material_Upscale_Denoise_CNN_x2_S.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Upscale_Denoise_CNN_x2_S, 2);
            _material_Upscale_Denoise_CNN_x2_S.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_last_tf, _material_Upscale_Denoise_CNN_x2_S, 3);
            _material_Upscale_Denoise_CNN_x2_S.SetTexture("conv2d_last_tf", tempRT_conv2d_last_tf);
            Graphics.Blit(source, destination, _material_Upscale_Denoise_CNN_x2_S, 4);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf);

        }

        public static void Upscale_Denoise_CNN_x2_M(Texture source, RenderTexture destination) {
            if(_material_Upscale_Denoise_CNN_x2_M == null)
                _material_Upscale_Denoise_CNN_x2_M = new Material(Shader.Find("Hidden/uAnime4K_Upscale_Denoise_CNN_x2_M"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Upscale_Denoise_CNN_x2_M, 0);
            _material_Upscale_Denoise_CNN_x2_M.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Upscale_Denoise_CNN_x2_M, 1);
            _material_Upscale_Denoise_CNN_x2_M.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Upscale_Denoise_CNN_x2_M, 2);
            _material_Upscale_Denoise_CNN_x2_M.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_3_tf, _material_Upscale_Denoise_CNN_x2_M, 3);
            _material_Upscale_Denoise_CNN_x2_M.SetTexture("conv2d_3_tf", tempRT_conv2d_3_tf);
            Graphics.Blit(source, tempRT_conv2d_4_tf, _material_Upscale_Denoise_CNN_x2_M, 4);
            _material_Upscale_Denoise_CNN_x2_M.SetTexture("conv2d_4_tf", tempRT_conv2d_4_tf);
            Graphics.Blit(source, tempRT_conv2d_5_tf, _material_Upscale_Denoise_CNN_x2_M, 5);
            _material_Upscale_Denoise_CNN_x2_M.SetTexture("conv2d_5_tf", tempRT_conv2d_5_tf);
            Graphics.Blit(source, tempRT_conv2d_6_tf, _material_Upscale_Denoise_CNN_x2_M, 6);
            _material_Upscale_Denoise_CNN_x2_M.SetTexture("conv2d_6_tf", tempRT_conv2d_6_tf);
            Graphics.Blit(source, tempRT_conv2d_last_tf, _material_Upscale_Denoise_CNN_x2_M, 7);
            _material_Upscale_Denoise_CNN_x2_M.SetTexture("conv2d_last_tf", tempRT_conv2d_last_tf);
            Graphics.Blit(source, destination, _material_Upscale_Denoise_CNN_x2_M, 8);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf);
        }

        public static void Upscale_Denoise_CNN_x2_VL(Texture source, RenderTexture destination) {
            if(_material_Upscale_Denoise_CNN_x2_VL == null)
                _material_Upscale_Denoise_CNN_x2_VL = new Material(Shader.Find("Hidden/uAnime4K_Upscale_Denoise_CNN_x2_VL"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Upscale_Denoise_CNN_x2_VL, 0);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_tf1, _material_Upscale_Denoise_CNN_x2_VL, 1);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_tf1", tempRT_conv2d_tf1);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Upscale_Denoise_CNN_x2_VL, 2);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf1, _material_Upscale_Denoise_CNN_x2_VL, 3);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_1_tf1", tempRT_conv2d_1_tf1);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Upscale_Denoise_CNN_x2_VL, 4);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf1, _material_Upscale_Denoise_CNN_x2_VL, 5);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_2_tf1", tempRT_conv2d_2_tf1);
            Graphics.Blit(source, tempRT_conv2d_3_tf, _material_Upscale_Denoise_CNN_x2_VL, 6);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_3_tf", tempRT_conv2d_3_tf);
            Graphics.Blit(source, tempRT_conv2d_3_tf1, _material_Upscale_Denoise_CNN_x2_VL, 7);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_3_tf1", tempRT_conv2d_3_tf1);
            Graphics.Blit(source, tempRT_conv2d_4_tf, _material_Upscale_Denoise_CNN_x2_VL, 8);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_4_tf", tempRT_conv2d_4_tf);
            Graphics.Blit(source, tempRT_conv2d_4_tf1, _material_Upscale_Denoise_CNN_x2_VL, 9);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_4_tf1", tempRT_conv2d_4_tf1);
            Graphics.Blit(source, tempRT_conv2d_5_tf, _material_Upscale_Denoise_CNN_x2_VL, 10);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_5_tf", tempRT_conv2d_5_tf);
            Graphics.Blit(source, tempRT_conv2d_5_tf1, _material_Upscale_Denoise_CNN_x2_VL, 11);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_5_tf1", tempRT_conv2d_5_tf1);
            Graphics.Blit(source, tempRT_conv2d_6_tf, _material_Upscale_Denoise_CNN_x2_VL, 12);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_6_tf", tempRT_conv2d_6_tf);
            Graphics.Blit(source, tempRT_conv2d_6_tf1, _material_Upscale_Denoise_CNN_x2_VL, 13);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_6_tf1", tempRT_conv2d_6_tf1);
            Graphics.Blit(source, tempRT_conv2d_last_tf, _material_Upscale_Denoise_CNN_x2_VL, 14);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_last_tf", tempRT_conv2d_last_tf);
            Graphics.Blit(source, tempRT_conv2d_last_tf1, _material_Upscale_Denoise_CNN_x2_VL, 15);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_last_tf1", tempRT_conv2d_last_tf1);
            Graphics.Blit(source, tempRT_conv2d_last_tf2, _material_Upscale_Denoise_CNN_x2_VL, 16);
            _material_Upscale_Denoise_CNN_x2_VL.SetTexture("conv2d_last_tf2", tempRT_conv2d_last_tf2);
            Graphics.Blit(source, destination, _material_Upscale_Denoise_CNN_x2_VL, 17);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf2);
        }

        public static void Upscale_CNN_x2_S(Texture source, RenderTexture destination) {
            if(_material_Upscale_CNN_x2_S == null)
                _material_Upscale_CNN_x2_S = new Material(Shader.Find("Hidden/uAnime4K_Upscale_CNN_x2_S"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Upscale_CNN_x2_S, 0);
            _material_Upscale_CNN_x2_S.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Upscale_CNN_x2_S, 1);
            _material_Upscale_CNN_x2_S.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Upscale_CNN_x2_S, 2);
            _material_Upscale_CNN_x2_S.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_last_tf, _material_Upscale_CNN_x2_S, 3);
            _material_Upscale_CNN_x2_S.SetTexture("conv2d_last_tf", tempRT_conv2d_last_tf);
            Graphics.Blit(source, destination, _material_Upscale_CNN_x2_S, 4);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf);
        }

        public static void Upscale_CNN_x2_M(Texture source, RenderTexture destination) {
            if(_material_Upscale_CNN_x2_M == null)
                _material_Upscale_CNN_x2_M = new Material(Shader.Find("Hidden/uAnime4K_Upscale_CNN_x2_M"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Upscale_CNN_x2_M, 0);
            _material_Upscale_CNN_x2_M.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Upscale_CNN_x2_M, 1);
            _material_Upscale_CNN_x2_M.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Upscale_CNN_x2_M, 2);
            _material_Upscale_CNN_x2_M.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_3_tf, _material_Upscale_CNN_x2_M, 3);
            _material_Upscale_CNN_x2_M.SetTexture("conv2d_3_tf", tempRT_conv2d_3_tf);
            Graphics.Blit(source, tempRT_conv2d_4_tf, _material_Upscale_CNN_x2_M, 4);
            _material_Upscale_CNN_x2_M.SetTexture("conv2d_4_tf", tempRT_conv2d_4_tf);
            Graphics.Blit(source, tempRT_conv2d_5_tf, _material_Upscale_CNN_x2_M, 5);
            _material_Upscale_CNN_x2_M.SetTexture("conv2d_5_tf", tempRT_conv2d_5_tf);
            Graphics.Blit(source, tempRT_conv2d_6_tf, _material_Upscale_CNN_x2_M, 6);
            _material_Upscale_CNN_x2_M.SetTexture("conv2d_6_tf", tempRT_conv2d_6_tf);
            Graphics.Blit(source, tempRT_conv2d_last_tf, _material_Upscale_CNN_x2_M, 7);
            _material_Upscale_CNN_x2_M.SetTexture("conv2d_last_tf", tempRT_conv2d_last_tf);
            Graphics.Blit(source, destination, _material_Upscale_CNN_x2_M, 8);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf);
        }

        public static void Upscale_CNN_x2_VL(Texture source, RenderTexture destination) {
            if(_material_Upscale_CNN_x2_VL == null)
                _material_Upscale_CNN_x2_VL = new Material(Shader.Find("Hidden/uAnime4K_Upscale_CNN_x2_VL"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_last_tf2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Upscale_CNN_x2_VL, 0);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_tf1, _material_Upscale_CNN_x2_VL, 1);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_tf1", tempRT_conv2d_tf1);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Upscale_CNN_x2_VL, 2);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf1, _material_Upscale_CNN_x2_VL, 3);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_1_tf1", tempRT_conv2d_1_tf1);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Upscale_CNN_x2_VL, 4);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf1, _material_Upscale_CNN_x2_VL, 5);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_2_tf1", tempRT_conv2d_2_tf1);
            Graphics.Blit(source, tempRT_conv2d_3_tf, _material_Upscale_CNN_x2_VL, 6);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_3_tf", tempRT_conv2d_3_tf);
            Graphics.Blit(source, tempRT_conv2d_3_tf1, _material_Upscale_CNN_x2_VL, 7);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_3_tf1", tempRT_conv2d_3_tf1);
            Graphics.Blit(source, tempRT_conv2d_4_tf, _material_Upscale_CNN_x2_VL, 8);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_4_tf", tempRT_conv2d_4_tf);
            Graphics.Blit(source, tempRT_conv2d_4_tf1, _material_Upscale_CNN_x2_VL, 9);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_4_tf1", tempRT_conv2d_4_tf1);
            Graphics.Blit(source, tempRT_conv2d_5_tf, _material_Upscale_CNN_x2_VL, 10);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_5_tf", tempRT_conv2d_5_tf);
            Graphics.Blit(source, tempRT_conv2d_5_tf1, _material_Upscale_CNN_x2_VL, 11);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_5_tf1", tempRT_conv2d_5_tf1);
            Graphics.Blit(source, tempRT_conv2d_6_tf, _material_Upscale_CNN_x2_VL, 12);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_6_tf", tempRT_conv2d_6_tf);
            Graphics.Blit(source, tempRT_conv2d_6_tf1, _material_Upscale_CNN_x2_VL, 13);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_6_tf1", tempRT_conv2d_6_tf1);
            Graphics.Blit(source, tempRT_conv2d_last_tf, _material_Upscale_CNN_x2_VL, 14);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_last_tf", tempRT_conv2d_last_tf);
            Graphics.Blit(source, tempRT_conv2d_last_tf1, _material_Upscale_CNN_x2_VL, 15);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_last_tf1", tempRT_conv2d_last_tf1);
            Graphics.Blit(source, tempRT_conv2d_last_tf2, _material_Upscale_CNN_x2_VL, 16);
            _material_Upscale_CNN_x2_VL.SetTexture("conv2d_last_tf2", tempRT_conv2d_last_tf2);
            Graphics.Blit(source, destination, _material_Upscale_CNN_x2_VL, 17);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_last_tf2);
        }

        public static void Restore_CNN_S(Texture source, RenderTexture destination) {
            if(_material_Restore_CNN_S == null)
                _material_Restore_CNN_S = new Material(Shader.Find("Hidden/uAnime4K_Restore_CNN_S"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Restore_CNN_S, 0);
            _material_Restore_CNN_S.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Restore_CNN_S, 1);
            _material_Restore_CNN_S.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Restore_CNN_S, 2);
            _material_Restore_CNN_S.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, destination, _material_Restore_CNN_S, 3);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);

        }

        public static void Restore_CNN_M(Texture source, RenderTexture destination) {
            if(_material_Restore_CNN_M == null)
                _material_Restore_CNN_M = new Material(Shader.Find("Hidden/uAnime4K_Restore_CNN_M"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_7_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Restore_CNN_M, 0);
            _material_Restore_CNN_M.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Restore_CNN_M, 1);
            _material_Restore_CNN_M.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Restore_CNN_M, 2);
            _material_Restore_CNN_M.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_3_tf, _material_Restore_CNN_M, 3);
            _material_Restore_CNN_M.SetTexture("conv2d_3_tf", tempRT_conv2d_3_tf);
            Graphics.Blit(source, tempRT_conv2d_4_tf, _material_Restore_CNN_M, 4);
            _material_Restore_CNN_M.SetTexture("conv2d_4_tf", tempRT_conv2d_4_tf);
            Graphics.Blit(source, tempRT_conv2d_5_tf, _material_Restore_CNN_M, 5);
            _material_Restore_CNN_M.SetTexture("conv2d_5_tf", tempRT_conv2d_5_tf);
            Graphics.Blit(source, tempRT_conv2d_6_tf, _material_Restore_CNN_M, 6);
            _material_Restore_CNN_M.SetTexture("conv2d_6_tf", tempRT_conv2d_6_tf);
            Graphics.Blit(source, tempRT_conv2d_7_tf, _material_Restore_CNN_M, 7);
            Graphics.Blit(tempRT_conv2d_7_tf, destination);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_7_tf);
        }

        public static void Restore_CNN_VL(Texture source, RenderTexture destination) {
            if(_material_Restore_CNN_VL == null)
                _material_Restore_CNN_VL = new Material(Shader.Find("Hidden/uAnime4K_Restore_CNN_VL"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_7_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_7_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Restore_CNN_VL, 0);
            _material_Restore_CNN_VL.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_tf1, _material_Restore_CNN_VL, 1);
            _material_Restore_CNN_VL.SetTexture("conv2d_tf1", tempRT_conv2d_tf1);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Restore_CNN_VL, 2);
            _material_Restore_CNN_VL.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf1, _material_Restore_CNN_VL, 3);
            _material_Restore_CNN_VL.SetTexture("conv2d_1_tf1", tempRT_conv2d_1_tf1);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Restore_CNN_VL, 4);
            _material_Restore_CNN_VL.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf1, _material_Restore_CNN_VL, 5);
            _material_Restore_CNN_VL.SetTexture("conv2d_2_tf1", tempRT_conv2d_2_tf1);
            Graphics.Blit(source, tempRT_conv2d_3_tf, _material_Restore_CNN_VL, 6);
            _material_Restore_CNN_VL.SetTexture("conv2d_3_tf", tempRT_conv2d_3_tf);
            Graphics.Blit(source, tempRT_conv2d_3_tf1, _material_Restore_CNN_VL, 7);
            _material_Restore_CNN_VL.SetTexture("conv2d_3_tf1", tempRT_conv2d_3_tf1);
            Graphics.Blit(source, tempRT_conv2d_4_tf, _material_Restore_CNN_VL, 8);
            _material_Restore_CNN_VL.SetTexture("conv2d_4_tf", tempRT_conv2d_4_tf);
            Graphics.Blit(source, tempRT_conv2d_4_tf1, _material_Restore_CNN_VL, 9);
            _material_Restore_CNN_VL.SetTexture("conv2d_4_tf1", tempRT_conv2d_4_tf1);
            Graphics.Blit(source, tempRT_conv2d_5_tf, _material_Restore_CNN_VL, 10);
            _material_Restore_CNN_VL.SetTexture("conv2d_5_tf", tempRT_conv2d_5_tf);
            Graphics.Blit(source, tempRT_conv2d_5_tf1, _material_Restore_CNN_VL, 11);
            _material_Restore_CNN_VL.SetTexture("conv2d_5_tf1", tempRT_conv2d_5_tf1);
            Graphics.Blit(source, tempRT_conv2d_6_tf, _material_Restore_CNN_VL, 12);
            _material_Restore_CNN_VL.SetTexture("conv2d_6_tf", tempRT_conv2d_6_tf);
            Graphics.Blit(source, tempRT_conv2d_6_tf1, _material_Restore_CNN_VL, 13);
            _material_Restore_CNN_VL.SetTexture("conv2d_6_tf1", tempRT_conv2d_6_tf1);
            Graphics.Blit(source, tempRT_conv2d_7_tf, _material_Restore_CNN_VL, 14);
            _material_Restore_CNN_VL.SetTexture("conv2d_7_tf", tempRT_conv2d_7_tf);
            Graphics.Blit(source, tempRT_conv2d_7_tf1, _material_Restore_CNN_VL, 15);
            _material_Restore_CNN_VL.SetTexture("conv2d_7_tf1", tempRT_conv2d_7_tf1);
            Graphics.Blit(source, destination, _material_Restore_CNN_VL, 16);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_7_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_7_tf1);
        }

        public static void Restore_CNN_Soft_S(Texture source, RenderTexture destination) {
            if(_material_Restore_CNN_Soft_S == null)
                _material_Restore_CNN_Soft_S = new Material(Shader.Find("Hidden/uAnime4K_Restore_CNN_Soft_S"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Restore_CNN_Soft_S, 0);
            _material_Restore_CNN_Soft_S.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Restore_CNN_Soft_S, 1);
            _material_Restore_CNN_Soft_S.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Restore_CNN_Soft_S, 2);
            _material_Restore_CNN_Soft_S.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, destination, _material_Restore_CNN_Soft_S, 3);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
        }

        public static void Restore_CNN_Soft_M(Texture source, RenderTexture destination) {
            if(_material_Restore_CNN_Soft_M == null)
                _material_Restore_CNN_Soft_M = new Material(Shader.Find("Hidden/uAnime4K_Restore_CNN_Soft_M"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Restore_CNN_Soft_M, 0);
            _material_Restore_CNN_Soft_M.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Restore_CNN_Soft_M, 1);
            _material_Restore_CNN_Soft_M.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Restore_CNN_Soft_M, 2);
            _material_Restore_CNN_Soft_M.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_3_tf, _material_Restore_CNN_Soft_M, 3);
            _material_Restore_CNN_Soft_M.SetTexture("conv2d_3_tf", tempRT_conv2d_3_tf);
            Graphics.Blit(source, tempRT_conv2d_4_tf, _material_Restore_CNN_Soft_M, 4);
            _material_Restore_CNN_Soft_M.SetTexture("conv2d_4_tf", tempRT_conv2d_4_tf);
            Graphics.Blit(source, tempRT_conv2d_5_tf, _material_Restore_CNN_Soft_M, 5);
            _material_Restore_CNN_Soft_M.SetTexture("conv2d_5_tf", tempRT_conv2d_5_tf);
            Graphics.Blit(source, tempRT_conv2d_6_tf, _material_Restore_CNN_Soft_M, 6);
            _material_Restore_CNN_Soft_M.SetTexture("conv2d_6_tf", tempRT_conv2d_6_tf);
            Graphics.Blit(source, destination, _material_Restore_CNN_Soft_M, 7);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf);
        }

        public static void Restore_CNN_Soft_VL(Texture source, RenderTexture destination) {
            if(_material_Restore_CNN_Soft_VL == null)
                _material_Restore_CNN_Soft_VL = new Material(Shader.Find("Hidden/uAnime4K_Restore_CNN_Soft_VL"));

            var tempRT_conv2d_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_1_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_2_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_3_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_4_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_5_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_6_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_7_tf = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT_conv2d_7_tf1 = RenderTexture.GetTemporary(destination.width, destination.height, -1, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(source, tempRT_conv2d_tf, _material_Restore_CNN_Soft_VL, 0);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_tf", tempRT_conv2d_tf);
            Graphics.Blit(source, tempRT_conv2d_tf1, _material_Restore_CNN_Soft_VL, 1);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_tf1", tempRT_conv2d_tf1);
            Graphics.Blit(source, tempRT_conv2d_1_tf, _material_Restore_CNN_Soft_VL, 2);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_1_tf", tempRT_conv2d_1_tf);
            Graphics.Blit(source, tempRT_conv2d_1_tf1, _material_Restore_CNN_Soft_VL, 3);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_1_tf1", tempRT_conv2d_1_tf1);
            Graphics.Blit(source, tempRT_conv2d_2_tf, _material_Restore_CNN_Soft_VL, 4);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_2_tf", tempRT_conv2d_2_tf);
            Graphics.Blit(source, tempRT_conv2d_2_tf1, _material_Restore_CNN_Soft_VL, 5);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_2_tf1", tempRT_conv2d_2_tf1);
            Graphics.Blit(source, tempRT_conv2d_3_tf, _material_Restore_CNN_Soft_VL, 6);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_3_tf", tempRT_conv2d_3_tf);
            Graphics.Blit(source, tempRT_conv2d_3_tf1, _material_Restore_CNN_Soft_VL, 7);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_3_tf1", tempRT_conv2d_3_tf1);
            Graphics.Blit(source, tempRT_conv2d_4_tf, _material_Restore_CNN_Soft_VL, 8);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_4_tf", tempRT_conv2d_4_tf);
            Graphics.Blit(source, tempRT_conv2d_4_tf1, _material_Restore_CNN_Soft_VL, 9);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_4_tf1", tempRT_conv2d_4_tf1);
            Graphics.Blit(source, tempRT_conv2d_5_tf, _material_Restore_CNN_Soft_VL, 10);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_5_tf", tempRT_conv2d_5_tf);
            Graphics.Blit(source, tempRT_conv2d_5_tf1, _material_Restore_CNN_Soft_VL, 11);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_5_tf1", tempRT_conv2d_5_tf1);
            Graphics.Blit(source, tempRT_conv2d_6_tf, _material_Restore_CNN_Soft_VL, 12);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_6_tf", tempRT_conv2d_6_tf);
            Graphics.Blit(source, tempRT_conv2d_6_tf1, _material_Restore_CNN_Soft_VL, 13);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_6_tf1", tempRT_conv2d_6_tf1);
            Graphics.Blit(source, tempRT_conv2d_7_tf, _material_Restore_CNN_Soft_VL, 14);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_7_tf", tempRT_conv2d_7_tf);
            Graphics.Blit(source, tempRT_conv2d_7_tf1, _material_Restore_CNN_Soft_VL, 15);
            _material_Restore_CNN_Soft_VL.SetTexture("conv2d_7_tf1", tempRT_conv2d_7_tf1);
            Graphics.Blit(source, destination, _material_Restore_CNN_Soft_VL, 16);

            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_1_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_2_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_3_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_4_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_5_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_6_tf1);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_7_tf);
            RenderTexture.ReleaseTemporary(tempRT_conv2d_7_tf1);
        }

        public static void Upscale_A_HQ(Texture source, RenderTexture destination) {
            var tempRT1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT3 = RenderTexture.GetTemporary(source.width * 2, source.height * 2, -1, RenderTextureFormat.ARGBHalf);
            var tempRT4 = RenderTexture.GetTemporary(destination.width / 2, destination.height / 2, -1, RenderTextureFormat.ARGBHalf);

            Clamp_Highlights(source, tempRT1);
            Restore_CNN_VL(tempRT1, tempRT2);
            Upscale_CNN_x2_VL(tempRT2, tempRT3);
            Graphics.Blit(tempRT3, tempRT4);
            Upscale_CNN_x2_M(tempRT4, destination);

            RenderTexture.ReleaseTemporary(tempRT1);
            RenderTexture.ReleaseTemporary(tempRT2);
            RenderTexture.ReleaseTemporary(tempRT3);
            RenderTexture.ReleaseTemporary(tempRT4);
        }

        public static void Upscale_B_HQ(Texture source, RenderTexture destination) {
            var tempRT1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT3 = RenderTexture.GetTemporary(source.width * 2, source.height * 2, -1, RenderTextureFormat.ARGBHalf);
            var tempRT4 = RenderTexture.GetTemporary(destination.width / 2, destination.height / 2, -1, RenderTextureFormat.ARGBHalf);

            Clamp_Highlights(source, tempRT1);
            Restore_CNN_Soft_VL(tempRT1, tempRT2);
            Upscale_CNN_x2_VL(tempRT2, tempRT3);
            Graphics.Blit(tempRT3, tempRT4);
            Upscale_CNN_x2_M(tempRT4, destination);

            RenderTexture.ReleaseTemporary(tempRT1);
            RenderTexture.ReleaseTemporary(tempRT2);
            RenderTexture.ReleaseTemporary(tempRT3);
            RenderTexture.ReleaseTemporary(tempRT4);
        }

        public static void Upscale_A_Fast(Texture source, RenderTexture destination) {
            var tempRT1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT3 = RenderTexture.GetTemporary(source.width * 2, source.height * 2, -1, RenderTextureFormat.ARGBHalf);
            var tempRT4 = RenderTexture.GetTemporary(destination.width / 2, destination.height / 2, -1, RenderTextureFormat.ARGBHalf);

            Clamp_Highlights(source, tempRT1);
            Restore_CNN_M(tempRT1, tempRT2);
            Upscale_CNN_x2_M(tempRT2, tempRT3);
            Graphics.Blit(tempRT3, tempRT4);
            Upscale_CNN_x2_S(tempRT4, destination);

            RenderTexture.ReleaseTemporary(tempRT1);
            RenderTexture.ReleaseTemporary(tempRT2);
            RenderTexture.ReleaseTemporary(tempRT3);
            RenderTexture.ReleaseTemporary(tempRT4);
        }

        public static void Upscale_B_Fast(Texture source, RenderTexture destination) {
            var tempRT1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT3 = RenderTexture.GetTemporary(source.width * 2, source.height * 2, -1, RenderTextureFormat.ARGBHalf);
            var tempRT4 = RenderTexture.GetTemporary(destination.width / 2, destination.height / 2, -1, RenderTextureFormat.ARGBHalf);

            Clamp_Highlights(source, tempRT1);
            Restore_CNN_Soft_M(tempRT1, tempRT2);
            Upscale_CNN_x2_M(tempRT2, tempRT3);
            Graphics.Blit(tempRT3, tempRT4);
            Upscale_CNN_x2_S(tempRT4, destination);

            RenderTexture.ReleaseTemporary(tempRT1);
            RenderTexture.ReleaseTemporary(tempRT2);
            RenderTexture.ReleaseTemporary(tempRT3);
            RenderTexture.ReleaseTemporary(tempRT4);
        }

        public static void Upscale_x2_A_HQ(Texture source, RenderTexture destination) {
            var tempRT1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT3 = RenderTexture.GetTemporary(source.width * 2, source.height * 2, -1, RenderTextureFormat.ARGBHalf);

            Clamp_Highlights(source, tempRT1);
            Restore_CNN_VL(tempRT1, tempRT2);
            Upscale_CNN_x2_VL(tempRT2, tempRT3);
            Graphics.Blit(tempRT3, destination);

            RenderTexture.ReleaseTemporary(tempRT1);
            RenderTexture.ReleaseTemporary(tempRT2);
            RenderTexture.ReleaseTemporary(tempRT3);
        }

        public static void Upscale_x2_B_HQ(Texture source, RenderTexture destination) {
            var tempRT1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT3 = RenderTexture.GetTemporary(source.width * 2, source.height * 2, -1, RenderTextureFormat.ARGBHalf);

            Clamp_Highlights(source, tempRT1);
            Restore_CNN_Soft_VL(tempRT1, tempRT2);
            Upscale_CNN_x2_VL(tempRT2, tempRT3);
            Graphics.Blit(tempRT3, destination);

            RenderTexture.ReleaseTemporary(tempRT1);
            RenderTexture.ReleaseTemporary(tempRT2);
            RenderTexture.ReleaseTemporary(tempRT3);
        }

        public static void Upscale_x2_A_Fast(Texture source, RenderTexture destination) {
            var tempRT1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT3 = RenderTexture.GetTemporary(source.width * 2, source.height * 2, -1, RenderTextureFormat.ARGBHalf);

            Clamp_Highlights(source, tempRT1);
            Restore_CNN_M(tempRT1, tempRT2);
            Upscale_CNN_x2_M(tempRT2, tempRT3);
            Graphics.Blit(tempRT3, destination);

            RenderTexture.ReleaseTemporary(tempRT1);
            RenderTexture.ReleaseTemporary(tempRT2);
            RenderTexture.ReleaseTemporary(tempRT3);
        }

        public static void Upscale_x2_B_Fast(Texture source, RenderTexture destination) {
            var tempRT1 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT2 = RenderTexture.GetTemporary(source.width, source.height, -1, RenderTextureFormat.ARGBHalf);
            var tempRT3 = RenderTexture.GetTemporary(source.width * 2, source.height * 2, -1, RenderTextureFormat.ARGBHalf);

            Clamp_Highlights(source, tempRT1);
            Restore_CNN_Soft_M(tempRT1, tempRT2);
            Upscale_CNN_x2_M(tempRT2, tempRT3);
            Graphics.Blit(tempRT3, destination);

            RenderTexture.ReleaseTemporary(tempRT1);
            RenderTexture.ReleaseTemporary(tempRT2);
            RenderTexture.ReleaseTemporary(tempRT3);
        }


    }
}

