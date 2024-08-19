using System.Runtime.InteropServices;

namespace TinityScripting.SceneManagement;

public static class SceneManager
{
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void RequestSceneLoad(IntPtr requestPtr);
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern IntPtr ForceSceneLoad(out int instanceId);
    
    private delegate void SceneLoadDelegate(IntPtr scenePtr, int sceneId);
    private static GCHandle _sceneLoadDelegateHandle;
    private static bool _sceneLoadRequested = false;
    
    public static void LoadScene(SceneAsset? sceneAsset)
    {
        RequestSceneLoad_Internal(sceneAsset);
    }
    
    internal static void ForceSceneLoad(SceneAsset? sceneAsset)
    {
        var ptr = ForceSceneLoad(out var sceneId);
        
        LoadSceneInternal(ptr, sceneId, sceneAsset);
    }
    
    private static void RequestSceneLoad_Internal(SceneAsset? sceneAsset)
    {
        if (_sceneLoadRequested)
        {
            _sceneLoadDelegateHandle.Free();
        }

        var delegateInstance = new SceneLoadDelegate(SceneLoadDelegate);
        _sceneLoadDelegateHandle = GCHandle.Alloc(delegateInstance);
        
        var ptr = Marshal.GetFunctionPointerForDelegate(delegateInstance);
        
        RequestSceneLoad(ptr);
        _sceneLoadRequested = true;
        
        return;

        void SceneLoadDelegate(IntPtr scenePtr, int sceneId)
        {
            _sceneLoadRequested = false;
            _sceneLoadDelegateHandle.Free();
            
            LoadSceneInternal(scenePtr, sceneId, sceneAsset);
        }
    }
    
    private static void LoadSceneInternal(IntPtr scenePtr, int sceneId, SceneAsset? sceneAsset)
    {
        Scene.ActiveScene?.Destroy_Internal();

        sceneAsset ??= new EmptyScene();
        
        Scene.ActiveScene = new Scene(sceneId, scenePtr);
        sceneAsset.Load();
    }
}