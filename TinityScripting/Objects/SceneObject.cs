using System.Runtime.InteropServices;
using TinityScripting.Components;
using TinityScripting.Components.BuiltIn;
using TinityScripting.SceneManagement;

namespace TinityScripting.Objects;

public class SceneObject : EngineObject
{
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void DestroySceneObject(IntPtr sceneObjectPtr);

    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern bool AddComponent(IntPtr sceneObjectPtr, int componentType, IntPtr[] eventMethodPtrs,
        int[] eventMethodsType, int eventMethodCount, out IntPtr componentPtr, out int instanceId);
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern bool RemoveComponent(IntPtr sceneObjectPtr, int componentId);
    
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
    }
    
    public T? AddComponent<T>() where T : Component, new()
    {
        if (typeof(T) == typeof(Component) || typeof(T) == typeof(Transform))
        {
            Console.Error.WriteLine("Cannot add Transform. All SceneObjects have Transform by default.");
            return default;
        }
        
        var component = Component.CreateComponent_Internal<T>(UnmanagedPtr, out var eventMethodPtrs, out var eventMethodTypes);
        component.SceneObject = this;
        
        if (!AddComponent(UnmanagedPtr, (int)component.BuiltInType, eventMethodPtrs, 
                eventMethodTypes, eventMethodPtrs.Length, out var componentPtr, out var instanceId))
        {
            throw new Exception("Failed to add component");
        }
        
        component.Setup(instanceId, componentPtr);
        _components.Add(instanceId, component);

        component.OnAttached_Internal();
        
        return component;
    }

    public bool RemoveComponent(Component component)
    {
        _components.Remove(component.InstanceId);
        component.Destroy_Internal();

        return RemoveComponent(UnmanagedPtr, component.InstanceId);
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