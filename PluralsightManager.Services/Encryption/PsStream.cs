// Decompiled with JetBrains decompiler
// Type: DecryptPluralSightVideos.Encryption.PsStream
// Assembly: DecryptPluralSightVideos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1AB3809-1ECE-4CD9-B948-DB32CFDE1E5B
// Assembly location: C:\Travail\Pluralsight\DecryptPluralSightVideos_v1.0\DecryptPluralSightVideos.exe

using System.IO;

namespace DecryptPluralSightVideos.Encryption
{
    public class PsStream : IPsStream
    {
        private readonly Stream fileStream;

        public long Length { get; private set; }

        public PsStream(string filenamePath)
        {
            fileStream = (Stream)File.Open(filenamePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Length = new FileInfo(filenamePath).Length;
        }

        public void Seek(int offset, SeekOrigin begin)
        {
            if (Length <= 0L)
                return;
            fileStream.Seek((long)offset, begin);
        }

        public int Read(byte[] pv, int i, int count)
        {
            return Length <= 0L ? 0 : fileStream.Read(pv, i, count);
        }

        public void Dispose()
        {
            Length = 0L;
            fileStream.Dispose();
        }
    }
}
