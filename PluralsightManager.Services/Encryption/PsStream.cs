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
        private long _length;

        public long Length
        {
            get
            {
                return this._length;
            }
        }

        public int BlockSize
        {
            get
            {
                return 262144;
            }
        }

        public PsStream(string filenamePath)
        {
            this.fileStream = (Stream)File.Open(filenamePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            this._length = new FileInfo(filenamePath).Length;
        }

        public void Seek(int offset, SeekOrigin begin)
        {
            if (this._length <= 0L)
                return;
            this.fileStream.Seek((long)offset, begin);
        }

        public int Read(byte[] pv, int i, int count)
        {
            return this._length <= 0L ? 0 : this.fileStream.Read(pv, i, count);
        }

        public void Dispose()
        {
            this._length = 0L;
            this.fileStream.Dispose();
        }
    }
}
