namespace TinityScripting;

public abstract class EngineObject
{
    internal EngineObject() { }
    
    internal EngineObject(int instanceId, IntPtr unmanagedPtr)
    {
        InstanceId = instanceId;
        UnmanagedPtr = unmanagedPtr;
    }
    
    internal void Setup(int instanceId, IntPtr unmanagedPtr)
    {
        InstanceId = instanceId;
        UnmanagedPtr = unmanagedPtr;
    }
    
    public int InstanceId { get; private set; }
    internal IntPtr UnmanagedPtr { get; private set; }

    internal abstract void Destroy_Internal();
}