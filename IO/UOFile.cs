﻿using ClassicUO.IO.Resources;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace ClassicUO.IO
{
    public abstract unsafe class UOFile : DataReader
    {
        private MemoryMappedViewAccessor _accessor;

        public UOFile(string filepath)
        {
            Path = filepath;
        }

        public string Path { get; }
        public UOFileIndex3D[] Entries { get; protected set; }

        protected virtual void Load()
        {
            FileInfo fileInfo = new FileInfo(Path);
            if (!fileInfo.Exists)
                throw new UOFileException(Path + " not exists.");
            long size = fileInfo.Length;
            if (size > 0)
            {
                MemoryMappedFile file = MemoryMappedFile.CreateFromFile(fileInfo.FullName, FileMode.Open);
                _accessor = file.CreateViewAccessor(0, size, MemoryMappedFileAccess.Read);

                byte* ptr = null;
                try
                {
                    _accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref ptr);

                    SetData(ptr, (long)_accessor.SafeMemoryMappedViewHandle.ByteLength);
                }
                catch
                {
                    _accessor.SafeMemoryMappedViewHandle.ReleasePointer();
                    throw new UOFileException("Something goes wrong...");
                }
            }
            else
                throw new UOFileException($"{Path} size must has > 0");
        }

     
        internal void Fill(byte[] buffer,  int count)
        {
            for (int i = 0; i < count; i++) buffer[i] = ReadByte();
        }

        internal T[] ReadArray<T>(int count) where T : struct
        {
            T[] t = ReadArray<T>(Position, count);
            Position += Marshal.SizeOf<T>() * count;
            return t;
        }

        internal T[] ReadArray<T>(long position,  int count) where T : struct
        {
            T[] array = new T[count];
            _accessor.ReadArray(position, array, 0, count);
            return array;
        }

        internal T ReadStruct<T>(long position) where T : struct
        {
            _accessor.Read(position, out T s);
            return s;
        }

        internal (int, int, bool) SeekByEntryIndex(int entryidx)
        {
            if (entryidx < 0 || entryidx >= Entries.Length) return (0, 0, false);

            UOFileIndex3D e = Entries[entryidx];
            if (e.Offset < 0) return (0, 0, false);

            int length = e.Length & 0x7FFFFFFF;
            int extra = e.Extra;

            if ((e.Length & (1 << 31)) != 0)
            {
                Verdata.File.Seek(e.Offset);
                return (length, extra, true);
            }

            if (e.Length < 0) return (0, 0, false);

            Seek(e.Offset);
            return (length, extra, false);
        }
    }

    public class UOFileException : Exception
    {
        public UOFileException(string text) : base(text)
        {
        }
    }
}