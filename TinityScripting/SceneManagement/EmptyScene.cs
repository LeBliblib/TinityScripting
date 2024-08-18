using TinityScripting.Components.BuiltIn;

namespace TinityScripting.SceneManagement;

internal class EmptyScene : SceneAsset
{
    public override void Load()
    {
        var obj = SceneObject.Create();
        obj.AddComponent<Camera>();
        
        var tR = SceneObject.Create();
        tR.AddComponent<TextureRenderer>()?.SetTexture("crown.png");
    }
}