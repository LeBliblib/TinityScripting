using TinityScripting.Components.BuiltIn;

namespace TinityScripting.Prefabs.BuiltIn;

public class CameraPrefab : Prefab
{
    public override SceneObject Spawn()
    {
        var obj = SceneObject.Create();
        obj.AddComponent<Camera>();
        
        return obj;
    }
}