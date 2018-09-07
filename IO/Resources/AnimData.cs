﻿using ClassicUO.IO;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ClassicUO.IO.Resources
{
    public static class AnimData
    {
        private static UOFileMul _file;

        public static void Load()
        {
            string path = Path.Combine(FileManager.UoFolderPath, "animdata.mul");

            if (File.Exists(path)) _file = new UOFileMul(path);
        }


        public static AnimDataFrame CalculateCurrentGraphic(ushort graphic)
        {
            IntPtr address = _file.StartAddress;

            if (address != IntPtr.Zero)
            {
                int addr = graphic * 68 + 4 * (graphic / 8 + 1);

                AnimDataFrame pad = Marshal.PtrToStructure<AnimDataFrame>(address + addr);

                //if (pad.FrameCount == 0)
                //{
                //    pad.FrameCount = 1;
                //    pad.FrameData[0] = 0;
                //}

                //if (pad.FrameInterval == 0)
                //    pad.FrameInterval = 1;

                return pad;
            }

            return default;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct AnimDataFrame
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public readonly sbyte[] FrameData;

        public readonly byte Unknown;
        public readonly byte FrameCount;
        public readonly byte FrameInterval;
        public readonly byte FrameStart;
    }
}