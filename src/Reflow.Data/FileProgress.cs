using System;
using System.Threading;

namespace Reflow.Data
{
    public sealed class FileProgress
    {
        private static readonly Lazy<FileProgress> Lazy = new Lazy<FileProgress>(() => new FileProgress());

        public static FileProgress Instance => Lazy.Value;

        private static int _progress = 0;

        private FileProgress()
        {

        }
        public void UpdateProgress()
        {
            Interlocked.Increment(ref _progress);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _progress, 0);
        }

        public int GetProgress()
        {
            return _progress;
        }


    }
}
