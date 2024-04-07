using CommandLine;

namespace TwitchDownloaderCLI.Modes.Arguments
{
    [Verb("ffmpeg", HelpText = "Manage standalone FFmpeg")]
    internal sealed class FfmpegArgs : TwitchDownloaderArgs
    {
        [Option('d', "download", Default = false, Required = false, HelpText = "Downloads FFmpeg as a standalone file.")]
        public bool DownloadFfmpeg { get; set; }
    }
}
