using System.Runtime.InteropServices;

namespace TinityScripting.Inputs;

public class InputsManager
{
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern bool IsKeyDown(int key);
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern bool IsKeyPressed(int key);
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern bool IsKeyUp(int key);
    
    public static bool IsKeyDown(Keycode key)
    {
        return IsKeyDown((int)key);
    }
    
    public static bool IsKeyPressed(Keycode key)
    {
        return IsKeyPressed((int)key);
    }
    
    public static bool IsKeyUp(Keycode key)
    {
        return IsKeyUp((int)key);
    }
}