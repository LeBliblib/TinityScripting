using System.Runtime.InteropServices;
using TinityScripting.SceneManagement;
using TinityScripting.Components;

namespace TinityScripting.Core;

public class Core
{
    [DllImport("BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void InitGame(IntPtr onGameInitializedPtr);
        
    [DllImport("BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void GetPosition(IntPtr transformPtr, out Vector2 position);
        
    private static void Main()
    {
        var ptr = Marshal.GetFunctionPointerForDelegate(OnGameInitialized);
        InitGame(ptr);
    }
    
    private static void OnGameInitialized()
    {
        SceneAsset? defaultScene = null;
        
        SceneManager.LoadScene(defaultScene);
    }
}