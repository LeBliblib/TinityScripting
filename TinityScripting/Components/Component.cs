using System.Reflection;
using System.Runtime.InteropServices;
using TinityScripting.Objects;

namespace TinityScripting.Components;

internal enum EventMethodType
{
    Attached = 0,
    Initialized = 1,
    Update = 2,
    Destroyed = 3
}

internal enum ComponentsBuiltInTypes
{
    None = 0,
    Camera = 1,
    TextureRenderer = 2,
    Transform = 3
};

public class Component : EngineObject
{
    private delegate void EventMethodDelegate();
    
    private static readonly Dictionary<EventMethodType, string> EventMethods = new()
    {
        {EventMethodType.Attached, "OnAttached"},
        {EventMethodType.Initialized, "OnInitialized"},
        {EventMethodType.Update, "OnUpdate"},
        {EventMethodType.Destroyed, "OnDestroyed"}
    };

    internal virtual ComponentsBuiltInTypes BuiltInType { get; } = ComponentsBuiltInTypes.None;
    
    public SceneObject? SceneObject { get; internal set; }

    private EventMethodDelegate? _attachMethod;
    
    private readonly List<GCHandle> _eventMethodHandles = new();
    
    protected Component() { }
    
    internal Component(int instanceId, IntPtr unmanagedPtr) : base(instanceId, unmanagedPtr) { }
    
    private static readonly Dictionary<int, IntPtr> EventMethodsCache = new();
    public static T CreateComponent_Internal<T>(IntPtr sceneObjectPtr, out IntPtr[] eventMethodPtrs, out int[] eventMethodTypes) where T : Component, new()
    {
        EventMethodsCache.Clear();
        var component = new T();
        
        var type = typeof(T);
        foreach (var eventMethod in EventMethods)
        {
            var method = type.GetMethod(eventMethod.Value, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) continue;

            if (eventMethod.Key == EventMethodType.Attached)
            {
                // Store it to call later when component will be fully attached
                component._attachMethod = method.CreateDelegate<EventMethodDelegate>(component);
                continue;
            }
            
            var methodDelegate = method.CreateDelegate<EventMethodDelegate>(component);
            component._eventMethodHandles.Add(GCHandle.Alloc(methodDelegate));
            
            var ptr = Marshal.GetFunctionPointerForDelegate(methodDelegate);
            EventMethodsCache.Add((int)eventMethod.Key, ptr);
        }
        
        eventMethodPtrs = EventMethodsCache.Values.ToArray();
        eventMethodTypes = EventMethodsCache.Keys.ToArray();
        return component;
    }

    internal override void Destroy_Internal()
    {
        foreach (var handle in _eventMethodHandles)
        {
            handle.Free();
        }
        
        _eventMethodHandles.Clear();
        SceneObject = null;
    }

    internal void OnAttached_Internal()
    {
        _attachMethod?.Invoke();
    }
}