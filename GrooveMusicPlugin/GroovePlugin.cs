// Uncomment these only if you want to export GetString() or ExecuteBang().
//#define DLLEXPORT_GETSTRING
//#define DLLEXPORT_EXECUTEBANG

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Rainmeter;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Text;

namespace GrooveMusicPlugin
{
    internal class Measure
    {

        enum MeasureType
        {
            Artist,
            Album,
            Title,
            Number,
            Year,
            Genre,
            Cover,
            File,
            Duration,
            Progress,
            Rating,
            Repeat,
            Shuffle,
            State,
            Status,
            Volume
        }

        private MeasureType Type = MeasureType.Title;

        internal Measure()
        {
        }

        // Read api and get/set settings internally
        internal void Reload(API api, ref double maxValue)
        {
            string type = api.ReadString("PlayerType", "");

            switch (type.ToLowerInvariant())
            {
                default:
                    API.Log(API.LogType.Error, "GroovePlugin.dll: PlayerType=" + type + " not valid");
                    break;
            }
        }

        // return values based on internal settings
        internal double Update()
        {

            return 0.0;
        }

        internal string GetString()
        {

            return null;
        }

#if DLLEXPORT_GETSTRING
        internal string GetString()
        {
            return "";
        }
#endif

#if DLLEXPORT_EXECUTEBANG
        internal void ExecuteBang(string args)
        {
        }
#endif
    }

    public static class Plugin
    {
#if DLLEXPORT_GETSTRING
        static IntPtr StringBuffer = IntPtr.Zero;
#endif

        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            GCHandle.FromIntPtr(data).Free();

#if DLLEXPORT_GETSTRING
            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }
#endif
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.Reload(new Rainmeter.API(rm), ref maxValue);
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            return measure.Update();
        }

#if DLLEXPORT_GETSTRING
        [DllExport]
        public static IntPtr GetString(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }

            string stringValue = measure.GetString();
            if (stringValue != null)
            {
                StringBuffer = Marshal.StringToHGlobalUni(stringValue);
            }

            return StringBuffer;
        }
#endif

#if DLLEXPORT_EXECUTEBANG
        [DllExport]
        public static void ExecuteBang(IntPtr data, IntPtr args)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.ExecuteBang(Marshal.PtrToStringUni(args));
        }
#endif
    }

}
