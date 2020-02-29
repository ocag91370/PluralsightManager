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
        private readonly object _Lock = new object();
        private long position;
        private VirtualFileCache _Cache;

        public VirtualFileStream(string EncryptedVideoFilePath)
        {
            this._Cache = new VirtualFileCache(EncryptedVideoFilePath);
        }

        private VirtualFileStream(VirtualFileCache Cache)
        {
            this._Cache = Cache;
        }

        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            if (this.position < 0L || this.position > this._Cache.Length)
            {
                Marshal.WriteIntPtr(pcbRead, new IntPtr(0));
            }
            else
            {
                lock (this._Lock)
                {
                    this._Cache.Read(pv, (int)this.position, cb, pcbRead);
                    this.position += pcbRead.ToInt64();
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
            lock (this._Lock)
            {
                switch (seekOrigin)
                {
                    case SeekOrigin.Begin:
                        this.position = dlibMove;
                        break;
                    case SeekOrigin.Current:
                        this.position += dlibMove;
                        break;
                    case SeekOrigin.End:
                        this.position = this._Cache.Length + dlibMove;
                        break;
                }
                if (!(IntPtr.Zero != plibNewPosition))
                    return;
                Marshal.WriteInt64(plibNewPosition, this.position);
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
            pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG();
            pstatstg.cbSize = this._Cache.Length;
        }

        public void Clone(out IStream ppstm)
        {
            ppstm = (IStream)new VirtualFileStream(this._Cache);
        }

        public void Dispose()
        {
            this._Cache.Dispose();
        }
    }
}
