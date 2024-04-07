using CommandLine;

namespace TwitchDownloaderCLI.Modes.Arguments
{
    [Verb("ffmpeg", HelpText = "Manage standalone FFmpeg")]
    internal sealed class FfmpegArgs : TwitchDownloaderArgs
    {
        [Option('d', "download", Default = false, Required = false, HelpText = "Downloads FFmpeg as a standalone file.")]
        public bool DownloadFfmpeg { get; set; }

        [Option("find", Default = false, Required = false, HelpText = "Tries to find a valid FFmpeg executable and logs the result.")]
        public bool DetectFfmpeg { get; set; }
    }
}
