using System.Runtime.InteropServices;

namespace TinityScripting.Components.BuiltIn;

public class TextureRenderer : Component
{
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern bool SetTexture(IntPtr rendererPtr, IntPtr path); 
    
    public void SetTexture(string path)
    {
        Console.WriteLine("Setting texture to: " + path);
        var pathPtr = Marshal.StringToHGlobalAnsi(path);
        
        SetTexture(UnmanagedPtr, pathPtr);
        Marshal.FreeHGlobal(pathPtr);
    }

    internal override ComponentsBuiltInTypes BuiltInType => ComponentsBuiltInTypes.TextureRenderer;
}