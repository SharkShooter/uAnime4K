using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
class Test : MonoBehaviour
{
    [Space]
    [SerializeField] Texture2D _source = null;
    [SerializeField] RenderTexture _output = null;
    [SerializeField] bool enable = true;
    public enum Mode { A_HQ, B_HQ, A_Fast, B_Fast };
    [SerializeField] Mode mode = Mode.A_HQ;
    [Space]
    [SerializeField] RawImage _sourceUI = null;
    [SerializeField] RawImage _outputUI = null;

    void Update()
    {
        if (_source == null || _output == null) return;

        if(enable) {
            if(mode == Mode.A_HQ)
                uAnime4K.ImageFilter.Upscale_A_HQ(_source, _output);
            if(mode == Mode.B_HQ)
                uAnime4K.ImageFilter.Upscale_B_HQ(_source, _output);
            if(mode == Mode.A_Fast)
                uAnime4K.ImageFilter.Upscale_A_Fast(_source, _output);
            if(mode == Mode.B_Fast)
                uAnime4K.ImageFilter.Upscale_B_Fast(_source, _output);
        }
        else {
            Graphics.Blit(_source, _output);
        }

        if (_sourceUI == null || _outputUI == null) return;

        _sourceUI.texture = _source;
        _outputUI.texture = _output;
    }
}
