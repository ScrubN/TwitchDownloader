using SkiaSharp;
using System.Collections.Generic;

namespace TwitchDownloaderCore.TwitchObjects
{
    public class CommentSection
    {
        public SKBitmap Image { get; set; }
        public List<(Point drawPoint, TwitchEmote emote)> Emotes { get; set; }
        public int CommentIndex { get; set; }
    }

    public record struct Point(int X, int Y)
    {
        public static implicit operator SKPoint(Point p) => new(p.X, p.Y);
        public static explicit operator Point(SKPoint p) => new((int)p.X, (int)p.Y);
    }
}
