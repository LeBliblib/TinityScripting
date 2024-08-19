using System.Runtime.InteropServices;
using TinityScripting.SceneManagement;

namespace TinityScripting.Objects;

public static class DestroyHandler
{
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void SetDestroyHandlerCallback(IntPtr callbackPtr);
    
    private delegate void DestroyHandlerDelegate(int instanceId);
    
    public static void Initialize()
    {
        var destroyHandlerDelegate = new DestroyHandlerDelegate(DestroyHandlerCallback);
        var ptr = Marshal.GetFunctionPointerForDelegate(destroyHandlerDelegate);
        
        SetDestroyHandlerCallback(ptr);
    }
    
    private static void DestroyHandlerCallback(int instanceId)
    {
        if (Scene.ActiveScene == null) return;
        if (!Scene.ActiveScene.GetSceneObject(instanceId, out var obj)) return;
        
        obj?.Destroy_Internal();
    }
}