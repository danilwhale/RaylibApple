﻿# raylib apple
a [bad apple animation](https://youtu.be/FtutLA63Cp8), but animated in raylib using raylib logo

watch raylib apple animation: __INSERT_VIDEO_LINK_HERE

### video summary
- video downscale: 14x
- logo size: 34px
- resolution in logos: ~34 * ~26 logos
- resolution in px: ~1156 * ~884 px
- framerate: 15 fps

### getting assets for animation
i didnt include assets of the animation, you need to do so yourself

#### get bad apple audio.mp4+video.mp4
you can use `yt-dlp` or any other tool to get mp4 file

we will use ffmpeg in next instructions to manipulate .mp4 files

#### (if audio is in .mp4 format) extract audio from .mp4
```
ffmpeg -i audio.mp4 -q:a 0 -map a audio.mp3
```

#### extract .png sequence for animation
```
ffmpeg -i video.mp4 -vf fps=15 "Frames/out%d.png"
```

`audio.mp3` **MUST** be in `PATH/TO/REPO/RaylibApple` directory

`out*.png` animation's frames **MUST** be in `PATH/TO/REPO/RaylibApple/Frames`

if you want to change locations, change them in `RaylibApple.csproj`