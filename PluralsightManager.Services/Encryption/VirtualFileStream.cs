// Decompiled with JetBrains decompiler
// Type: DecryptPluralSightVideos.Encryption.VirtualFileStream
// Assembly: DecryptPluralSightVideos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1AB3809-1ECE-4CD9-B948-DB32CFDE1E5B
// Assembly location: C:\Travail\Pluralsight\DecryptPluralSightVideos_v1.0\DecryptPluralSightVideos.exe

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace DecryptPluralSightVideos.Encryption
{
    internal class VirtualFileStream : IStream, IDisposable
    {
        private readonly object _lock = new object();
        private long position;
        private readonly VirtualFileCache _cache;

        public VirtualFileStream(string EncryptedVideoFilePath)
        {
            _cache = new VirtualFileCache(EncryptedVideoFilePath);
        }

        private VirtualFileStream(VirtualFileCache Cache)
        {
            _cache = Cache;
        }

        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            if (position < 0L || position > _cache.Length)
            {
                Marshal.WriteIntPtr(pcbRead, new IntPtr(0));
            }
            else
            {
                lock (_lock)
                {
                    _cache.Read(pv, (int)position, cb, pcbRead);
                    position += pcbRead.ToInt64();
                }
            }
        }

        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            SeekOrigin seekOrigin = (SeekOrigin)dwOrigin;
            lock (_lock)
            {
                switch (seekOrigin)
                {
                    case SeekOrigin.Begin:
                        position = dlibMove;
                        break;
                    case SeekOrigin.Current:
                        position += dlibMove;
                        break;
                    case SeekOrigin.End:
                        position = _cache.Length + dlibMove;
                        break;
                }
                if (!(IntPtr.Zero != plibNewPosition))
                    return;
                Marshal.WriteInt64(plibNewPosition, position);
            }
        }

        public void SetSize(long libNewSize)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        public void Commit(int grfCommitFlags)
        {
            throw new NotImplementedException();
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }

        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
        {
            pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG() { cbSize = _cache.Length };
        }

        public void Clone(out IStream ppstm)
        {
            ppstm = (IStream)new VirtualFileStream(_cache);
        }

        public void Dispose()
        {
            _cache.Dispose();
        }
    }
}
