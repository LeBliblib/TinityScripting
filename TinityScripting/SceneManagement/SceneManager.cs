using System.Runtime.InteropServices;

namespace TinityScripting.SceneManagement;

public static class SceneManager
{
    [DllImport("BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern IntPtr LoadNewScene(out int instanceId);
    
    public static void LoadScene(SceneAsset? sceneAsset)
    {
        LoadSceneInternal(sceneAsset);
    }
    
    private static void LoadSceneInternal(SceneAsset? sceneAsset)
    {
        Scene.ActiveScene?.Destroy_Internal();

        sceneAsset ??= new EmptyScene();
        
        var scenePtr = LoadNewScene(out var instanceId);
        
        Scene.ActiveScene = new Scene(instanceId, scenePtr);
        sceneAsset.Load();
    }
}