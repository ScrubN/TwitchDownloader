using System;
using SkiaSharp;

namespace TwitchDownloaderCore.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class SKCanvasExtensions
    {
        public static void DrawText(this SKCanvas canvas, ReadOnlySpan<char> text, float x, float y, SKFont font, SKPaint paint, SKTextAlign textAlign = SKTextAlign.Left)
        {
            if (textAlign != SKTextAlign.Left)
            {
                var num = font.MeasureText(text, paint);
                if (textAlign == SKTextAlign.Center)
                    num *= 0.5f;
                x -= num;
            }

            using var text1 = SKTextBlob.Create(text, font);
            if (text1 == null)
                return;

            canvas.DrawText(text1, x, y, paint);
        }
    }
}