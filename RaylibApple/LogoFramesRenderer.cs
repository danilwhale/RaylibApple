﻿using System.Numerics;

namespace RaylibApple;

public class LogoFramesRenderer
{
    public const int LogoSize = 32;
    public const float SizeFactor = LogoSize / 256.0f;
    public const int TextSize = (int)(50.0f * SizeFactor);
    public const int BorderSize = (int)(16.0f * SizeFactor);
    public const int TextOffsetX = (int)(44.0f * SizeFactor);
    public const int TextOffsetY = (int)(48.0f * SizeFactor);
    
    public int CurrentFrame = 0;
    public Color[][][] Frames = null!;

    public void Render()
    {
        for (var x = 0; x < Frames[CurrentFrame].Length; x++)
        for (var y = 0; y < Frames[CurrentFrame][x].Length; y++)
        {
            var pixel = Frames[CurrentFrame][x][y];
            
            var brightnessValue = (pixel.R + pixel.G + pixel.B) / 3.0f / 255.0f;

            if (brightnessValue is >= 0.1f and <= 0.9f)
            {
                var brightnessByte = (byte)(brightnessValue * 255);

                var foreground = new Color(255 - brightnessByte, 255 - brightnessByte, 255 - brightnessByte, 255);
                var background = new Color(brightnessByte, brightnessByte, brightnessByte, (byte)255);
                
                    RenderLogo(x, y, foreground, background, foreground);
            }
            else
            {
                RenderLogo(x, y, brightnessValue < 0.1f);
            }
        }
    }

    private static void RenderLogo(int x, int y, bool inverted = false)
    {
        var borders = inverted ? Color.RayWhite : Color.Black;
        var background = inverted ? Color.Black : Color.RayWhite;
        var foreground = inverted ? Color.RayWhite : Color.Black;

        RenderLogo(x, y, borders, background, foreground);
    }

    private static void RenderLogo(int x, int y, Color borders, Color background, Color foreground)
    {
        var xo = x * LogoSize;
        var yo = y * LogoSize;

        DrawRectangle(xo, yo, LogoSize, LogoSize, borders);
        DrawRectangle(xo + BorderSize, yo + BorderSize, LogoSize - BorderSize * 2, LogoSize - BorderSize * 2,
            background);
        DrawTextEx(GetFontDefault(), "raylib",
            new Vector2(xo + LogoSize / 2.0f - TextOffsetX, yo + LogoSize / 2.0f + TextOffsetY), TextSize,
            10.0f / TextSize, foreground);
    }
}