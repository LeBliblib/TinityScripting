using System.Runtime.InteropServices;

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
            var method = type.GetMethod(eventMethod.Value);
            if(method == null) continue;
            
            var ptr = Marshal.GetFunctionPointerForDelegate(method.CreateDelegate<EventMethodDelegate>(component));
            EventMethodsCache.Add((int)eventMethod.Key, ptr);
        }
        
        eventMethodPtrs = EventMethodsCache.Values.ToArray();
        eventMethodTypes = EventMethodsCache.Keys.ToArray();
        return component;
    }

    internal override void Destroy_Internal() { }
}