global using Raylib_cs;
global using static Raylib_cs.Raylib;
using RaylibApple;

const int downscale = 12;

InitWindow(480 / downscale * CanvasRenderer.LogoSize, 360 / downscale * CanvasRenderer.LogoSize, "raylib apple");
InitAudioDevice();
SetTargetFPS(30);

var renderer = new CanvasRenderer();

var frames = Directory.GetFiles("Frames");
Array.Sort(frames, (s, s1) =>
{
    var indexStr0 = Path.GetFileNameWithoutExtension(s).Remove(0, 3);
    var indexStr1 = Path.GetFileNameWithoutExtension(s1).Remove(0, 3);

    if (!int.TryParse(indexStr0, out var index0)) return -1;
    if (!int.TryParse(indexStr1, out var index1)) return 1;

    return index0.CompareTo(index1);
});

renderer.Frames = new Color[frames.Length][][];
for (var f = 0; f < frames.Length; f++)
{
    var image = LoadImage(frames[f]);

    renderer.Frames[f] = new Color[image.Width / downscale][];
    for (var x = 0; x < image.Width / downscale; x++)
    {
        renderer.Frames[f][x] = new Color[image.Height / downscale];
        for (var y = 0; y < image.Height / downscale; y++)
        {
            var ix = x * downscale;
            var iy = y * downscale;
            renderer.Frames[f][x][y] = GetImageColor(image, ix, iy);
        }
    }

    UnloadImage(image);
}

var audio = LoadSound("audio.mp3");
PlaySound(audio);

while (!WindowShouldClose() && renderer.CurrentFrame < frames.Length)
{
    renderer.CurrentFrame++;

    BeginDrawing();
    ClearBackground(Color.RayWhite);

    renderer.Render();

    EndDrawing();
}

if (IsSoundPlaying(audio)) StopSound(audio);

UnloadSound(audio);
CloseAudioDevice();
CloseWindow();