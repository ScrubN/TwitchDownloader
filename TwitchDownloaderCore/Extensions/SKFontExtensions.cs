using System;
using SkiaSharp;
using SkiaSharp.HarfBuzz;

namespace TwitchDownloaderCore.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class SKFontExtensions
    {
        // Heavily modified from SkiaSharp.HarfBuzz.CanvasExtensions.DrawShapedText
        public static SKPath GetShapedTextPath(this SKFont font, ReadOnlySpan<char> text, float x, float y, SKTextAlign textAlign = SKTextAlign.Left)
        {
            var returnPath = new SKPath();

            if (text.IsEmpty || text.IsWhiteSpace())
                return returnPath;

            using var shaper = new SKShaper(font.Typeface);
            using var buffer = new HarfBuzzSharp.Buffer();
            buffer.Add(text, SKTextEncoding.Utf16);
            var result = shaper.Shape(buffer, x, y, font);

            var glyphSpan = result.Codepoints.AsSpan();
            var pointSpan = result.Points.AsSpan();

            var xOffset = 0.0f;
            if (textAlign != SKTextAlign.Left)
            {
                var width = result.Width;
                if (textAlign == SKTextAlign.Center)
                    width *= 0.5f;
                xOffset -= width;
            }

            for (var i = 0; i < pointSpan.Length; i++)
            {
                using var glyphPath = font.GetGlyphPath((ushort)glyphSpan[i]);
                if (glyphPath.IsEmpty)
                    continue;

                var point = pointSpan[i];
                glyphPath.Transform(new SKMatrix(
                    1, 0, point.X + xOffset,
                    0, 1, point.Y,
                    0, 0, 1
                ));
                returnPath.AddPath(glyphPath);
            }

            return returnPath;
        }
    }
}