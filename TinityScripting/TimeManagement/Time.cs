using System.Runtime.InteropServices;

namespace TinityScripting;

public static class Time
{
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern float GetDeltaTime();

    public static float DeltaTime => GetDeltaTime();
}