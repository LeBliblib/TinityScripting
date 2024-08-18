using TinityScripting.Components;

namespace TinityScripting;

public class TestComp2 : Component
{
    public TestComp2(int instanceId, IntPtr unmanagedPtr) : base(instanceId, unmanagedPtr)
    {
    }
    
    public void OnAttached()
    {
        Console.WriteLine("OnAttached");
    }
    
    public void OnDestroyed()
    {
        Console.WriteLine("OnDestroyed");
    }
}