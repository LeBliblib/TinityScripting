using TinityScripting.Components.BuiltIn;
using TinityScripting.Objects;

namespace TinityScripting.SceneManagement;

internal class EmptyScene : SceneAsset
{
    public override void Load()
    {
        var obj = SceneObject.Create();
        obj.AddComponent<Camera>();
    }
}