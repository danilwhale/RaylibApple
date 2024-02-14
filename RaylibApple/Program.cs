global using Raylib_cs;
global using static Raylib_cs.Raylib;
using RaylibApple;

internal class Program
{
    private const int Downscale = 12;
    
    private const int ScreenWidth = 480 / Downscale * LogoFramesRenderer.LogoSize;
    private const int ScreenHeight = 360 / Downscale * LogoFramesRenderer.LogoSize;
    private const int Framerate = 30;

    private static LogoFramesRenderer _renderer;
    private static string[] _frameImages;
    private static Sound _audio;

    public static void Main(string[] args)
    {
        InitWindow(ScreenWidth, ScreenHeight, "raylib apple");
        InitAudioDevice();
        SetTargetFPS(Framerate);

        _renderer = new LogoFramesRenderer();

        LoadFrameFiles();
        LoadFrames();

        _audio = LoadSound("audio.mp3");
        PlaySound(_audio);

        while (!WindowShouldClose())
        {
            _renderer.CurrentFrame++;
            if (_renderer.CurrentFrame >= _renderer.Frames.Length)
            {
                break;
            }

            BeginDrawing();
            ClearBackground(Color.RayWhite);

            _renderer.Render();

            EndDrawing();
        }

        UnloadSound(_audio);
        CloseAudioDevice();
        CloseWindow();
    }

    private static void LoadFrameFiles()
    {
        _frameImages = Directory.GetFiles("Frames");
        
        Array.Sort(_frameImages, (s, s1) =>
        {
            var indexStr0 = Path.GetFileNameWithoutExtension(s).Remove(0, 3);
            var indexStr1 = Path.GetFileNameWithoutExtension(s1).Remove(0, 3);

            if (!int.TryParse(indexStr0, out var index0)) return -1;
            
            return !int.TryParse(indexStr1, out var index1) 
                ? 1 
                : index0.CompareTo(index1);
        });
    }

    private static void LoadFrames()
    {
        _renderer.Frames = new Color[_frameImages.Length][][];

        for (var f = 0; f < _frameImages.Length; f++)
        {
            var image = LoadImage(_frameImages[f]);

            _renderer.Frames[f] = new Color[image.Width / Downscale][];
            
            for (var x = 0; x < image.Width / Downscale; x++)
            {
                _renderer.Frames[f][x] = new Color[image.Height / Downscale];
                
                for (var y = 0; y < image.Height / Downscale; y++)
                {
                    var ix = x * Downscale;
                    var iy = y * Downscale;
                    
                    _renderer.Frames[f][x][y] = GetImageColor(image, ix, iy);
                }
            }

            UnloadImage(image);
        }
    }
}