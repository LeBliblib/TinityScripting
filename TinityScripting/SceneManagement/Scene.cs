using System.Runtime.InteropServices;
using TinityScripting.Components.BuiltIn;

namespace TinityScripting.SceneManagement;

public class Scene : EngineObject
{
    public static Scene? ActiveScene { get; internal set; }
    private readonly Dictionary<int, SceneObject> _sceneObjects = new();
    
    public Scene(int instanceId, IntPtr unmanagedPtr) : base(instanceId, unmanagedPtr) { }
    
    [DllImport("BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern IntPtr CreateSceneObject(out int instanceId, out IntPtr transformPtr, out int transformInstanceId);
    
    private SceneObject CreateSceneObject()
    {
        IntPtr unmanagedPtr = CreateSceneObject(out var instanceId, out var transformPtr, out var transformInstanceId);
        
        var transform = new Transform(transformInstanceId, transformPtr);
        
        var sceneObject = new SceneObject(instanceId, unmanagedPtr)
        {
            Scene = this,
            Transform = transform
        };
        
        transform.SceneObject = sceneObject;

        _sceneObjects.Add(instanceId, sceneObject);
        return sceneObject;
    }

    internal void RemoveSceneObject(int instanceId)
    {
        _sceneObjects.Remove(instanceId);
    }
    
    internal static SceneObject CreateSceneObject_Internal()
    {
        if (ActiveScene == null)
            throw new Exception("No active scene");
        
        return ActiveScene.CreateSceneObject();
    }

    internal override void Destroy_Internal()
    {
        foreach (var sceneObject in _sceneObjects.Values)
        {
            sceneObject.Destroy_Internal();
        }
        
        _sceneObjects.Clear();
    }
}