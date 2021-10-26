uAnime4K
============

[Anime4K] is a set of real-time upscaling/denoising algorithms for anime and uAnime4K is the Unity implementation. I found out [UnityAnime4K] uses the older version of Anime4K so I ported the updated CNN-based shaders to Unity.

[Anime4K]: https://github.com/bloc97/Anime4K
[UnityAnime4K]: https://github.com/keijiro/UnityAnime4K

Examples
--------

<img src="https://github.com/SharkShooter/uAnime4K/blob/master/Assets/f113/f113_Bilinear.png?raw=true" width=50% height=50%>

(Bilinear)

<img src="https://github.com/SharkShooter/uAnime4K/blob/master/Assets/f113/f113_A_HQ.png?raw=true" width=50% height=50%><img src="https://github.com/SharkShooter/uAnime4K/blob/master/Assets/f113/f113_B_HQ.png?raw=true" width=50% height=50%>

(Left: Anime4K Mode A(HQ), Right: Anime4K Mode B(HQ))

<img src="https://github.com/SharkShooter/uAnime4K/blob/master/Assets/f113/f113_A_Fast.png?raw=true" width=50% height=50%><img src="https://github.com/SharkShooter/uAnime4K/blob/master/Assets/f113/f113_B_Fast.png?raw=true" width=50% height=50%>

(Left: Anime4K Mode A(Fast), Right: Anime4K Mode B(Fast))

How To Use
----------

You can choose two major modes(A, B) and two variants(HQ, Fast) for each mode. Mode B usually looks softer than A. Refer to [Anime4K Installation Instructions for GLSL/MPV] for the detailed mode explanation. 

APIs are like below:

`uAnime4K.ImageFilter.Upscale_A_HQ(source, destination)`

[Anime4K Installation Instructions for GLSL/MPV]: https://github.com/bloc97/Anime4K/blob/master/GLSL_Instructions.md

Acknowledgements
----------------

Thanks to the great project [Anime4K]. Please follow the [original license] when you use.

[original license]: https://github.com/bloc97/Anime4K/blob/master/LICENSE

And also thanks to [UnityAnime4K], this sample scene setup is based on the project.

This repository contains the following image materials:

- 【物語×放置ゲームコレクション】ゴスロリ吸血鬼 from Niconi Commons

  https://commons.nicovideo.jp/material/nc160359

- ジュエルセイバーFREE

  http://www.jewel-s.jp/
