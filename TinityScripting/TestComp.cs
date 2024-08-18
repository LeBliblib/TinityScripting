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
        if (SceneObject == null)
            return;
        
        SceneObject.Transform!.Position += Vector2.Right * 0.05f;
    }
}