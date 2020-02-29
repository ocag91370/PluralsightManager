// Decompiled with JetBrains decompiler
// Type: DecryptPluralSightVideos.Encryption.IPsStream
// Assembly: DecryptPluralSightVideos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1AB3809-1ECE-4CD9-B948-DB32CFDE1E5B
// Assembly location: C:\Travail\Pluralsight\DecryptPluralSightVideos_v1.0\DecryptPluralSightVideos.exe

using System.IO;

namespace DecryptPluralSightVideos.Encryption
{
    public interface IPsStream
    {
        long Length { get; }

        int BlockSize { get; }

        void Seek(int offset, SeekOrigin begin);

        int Read(byte[] pv, int i, int count);

        void Dispose();
    }
}
