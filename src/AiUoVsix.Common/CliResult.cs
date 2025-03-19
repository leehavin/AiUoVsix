using System;

namespace AiUoVsix.Common
{
    public class CliResult
    {
        public int ExitCode { get; set; }

        public bool Success => ExitCode == 0;

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset ExitTime { get; set; }

        public TimeSpan RunTime { get; set; }

        public string Output { get; set; }

        public string Error { get; set; }
    }
}
