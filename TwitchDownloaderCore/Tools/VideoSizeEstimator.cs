using System;

namespace TwitchDownloaderCore.Tools
{
    public static class VideoSizeEstimator
    {
        public static string StringifyByteCount(long sizeInBytes)
        {
            const long ONE_KIBIBYTE = 1024;
            const long ONE_MEBIBYTE = ONE_KIBIBYTE * 1024;
            const long ONE_GIBIBYTE = ONE_MEBIBYTE * 1024;

            return sizeInBytes switch
            {
                < 1 => "",

                < ONE_KIBIBYTE => $"{sizeInBytes}B",

                < 100 * ONE_KIBIBYTE => $"{(float)sizeInBytes / ONE_KIBIBYTE:F2}KiB",
                < ONE_MEBIBYTE => $"{(float)sizeInBytes / ONE_KIBIBYTE:F1}KiB",

                < 100 * ONE_MEBIBYTE => $"{(float)sizeInBytes / ONE_MEBIBYTE:F2}MiB",
                < ONE_GIBIBYTE => $"{(float)sizeInBytes / ONE_MEBIBYTE:F1}MiB",

                < 100 * ONE_GIBIBYTE => $"{(float)sizeInBytes / ONE_GIBIBYTE:F2}GiB",
                _ => $"{(float)sizeInBytes / ONE_GIBIBYTE:F1}GiB",
            };
        }

        /// <returns>An estimate of the final video download size in bytes.</returns>
        public static long EstimateVideoSize(int bandwidth, TimeSpan startTime, TimeSpan endTime)
        {
            if (bandwidth < 1)
                return 0;
            if (endTime < startTime)
                return 0;

            var totalTime = endTime - startTime;
            return (long)(bandwidth / 8d * totalTime.TotalSeconds);
        }
    }
}