using TinityScripting.Components;

namespace TinityScripting;

public class TestComp : Component
{
    public void OnAttached()
    {
        Console.WriteLine("OnAttached");
    }
    
    public void OnUpdate()
    {
        Console.WriteLine("OnUpdate");
    }
}