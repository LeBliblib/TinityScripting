using System.Reflection;
using System.Runtime.InteropServices;
using TinityScripting.SceneManagement;
using TinityScripting.Components;
using TinityScripting.Maths;
using TinityScripting.Objects;

namespace TinityScripting;

public static class Core
{
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void InitGame(IntPtr onGameInitializedPtr);
        
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void SetTargetFrameRate(int frameRate);
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void LimitFrameRate(bool value);    
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void DisplayFps(bool value);
        
    private static SceneAsset? _defaultScene = null;
    
    private delegate void OnGameInitializedDelegate();
    private static GCHandle _onGameInitializedDelegateHandle;
    
    public static void StartGame()
    {
        var onGameInitializedDelegate = new OnGameInitializedDelegate(OnGameInitialized);
        _onGameInitializedDelegateHandle = GCHandle.Alloc(onGameInitializedDelegate);
        
        var ptr = Marshal.GetFunctionPointerForDelegate(onGameInitializedDelegate);
        
        InitGame(ptr);
    }
    
    private static void OnGameInitialized()
    {
        _onGameInitializedDelegateHandle.Free();
        
        DestroyHandler.Initialize();
        SceneManager.ForceSceneLoad(_defaultScene);
    }
    
    public static void SetDefaultScene(SceneAsset sceneAsset)
    {
        _defaultScene = sceneAsset;
    }
    
    public static void SetFrameRate(int frameRate)
    {
        SetTargetFrameRate(frameRate);
    }
    
    public static void UseFrameRateLimit(bool value)
    {
        LimitFrameRate(value);
    }
    
    public static void ShowFps(bool value)
    {
        DisplayFps(value);
    }
}