using System.Runtime.InteropServices;
using TinityScripting.Components;
using TinityScripting.Components.BuiltIn;
using TinityScripting.SceneManagement;

namespace TinityScripting;

public class SceneObject : EngineObject
{
    [DllImport("BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void DestroySceneObject(IntPtr sceneObjectPtr);

    [DllImport("BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern bool AddComponent(IntPtr sceneObjectPtr, int componentType, IntPtr[] eventMethodPtrs,
        int[] eventMethodsType, int eventMethodCount, out IntPtr componentPtr, out int instanceId);
    
    private readonly Dictionary<int, Component> _components = new Dictionary<int, Component>();
    
    internal Scene? Scene { get; set; }
    public Transform? Transform { get; internal set; }
    
    public SceneObject(int instanceId, IntPtr unmanagedPtr) : base(instanceId, unmanagedPtr) { }

    public static SceneObject Create()
    {
        return Scene.CreateSceneObject_Internal();
    }
    
    public void Destroy()
    {
        DestroySceneObject(UnmanagedPtr);
        
        Destroy_Internal();
    }
    
    public T? AddComponent<T>() where T : Component, new()
    {
        if (typeof(T) == typeof(Component) || typeof(T) == typeof(Transform))
        {
            Console.Error.WriteLine("Cannot add Transform. All SceneObjects have Transform by default.");
            return default;
        }
        
        var component = Component.CreateComponent_Internal<T>(UnmanagedPtr, out var eventMethodPtrs, out var eventMethodTypes);
        
        if (!AddComponent(UnmanagedPtr, (int)component.BuiltInType, eventMethodPtrs, 
                eventMethodTypes, eventMethodPtrs.Length, out var componentPtr, out var instanceId))
        {
            throw new Exception("Failed to add component");
        }
        
        component.Setup(instanceId, componentPtr);
        _components.Add(instanceId, component);
        
        return component;
    }

    internal override void Destroy_Internal()
    {
        Transform?.Destroy_Internal();
        foreach (var component in _components.Values)
        {
            component.Destroy_Internal();
        }
        
        Transform = null;
        _components.Clear();
        
        Scene?.RemoveSceneObject(InstanceId);
    }
}