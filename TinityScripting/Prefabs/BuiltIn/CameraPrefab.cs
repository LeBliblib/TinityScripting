using TinityScripting.Components.BuiltIn;
using TinityScripting.Objects;

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