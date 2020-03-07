// Decompiled with JetBrains decompiler
// Type: DecryptPluralSightVideos.Encryption.VirtualFileCache
// Assembly: DecryptPluralSightVideos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1AB3809-1ECE-4CD9-B948-DB32CFDE1E5B
// Assembly location: C:\Travail\Pluralsight\DecryptPluralSightVideos_v1.0\DecryptPluralSightVideos.exe

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DecryptPluralSightVideos.Encryption
{
    internal class VirtualFileCache : IDisposable
    {
        private readonly IPsStream _encryptedVideoFile;

        public long Length
        {
            get
            {
                return _encryptedVideoFile.Length;
            }
        }

        public VirtualFileCache(string encryptedVideoFilePath)
        {
            _encryptedVideoFile = (IPsStream)new PsStream(encryptedVideoFilePath);
        }

        public VirtualFileCache(IPsStream stream)
        {
            _encryptedVideoFile = stream;
        }

        public void Read(byte[] pv, int offset, int count, IntPtr pcbRead)
        {
            if (Length == 0L)
                return;
            _encryptedVideoFile.Seek(offset, SeekOrigin.Begin);
            int length = _encryptedVideoFile.Read(pv, 0, count);
            VideoEncryption.XorBuffer(pv, length, (long)offset);
            if (!(IntPtr.Zero != pcbRead))
                return;
            Marshal.WriteIntPtr(pcbRead, new IntPtr(length));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
