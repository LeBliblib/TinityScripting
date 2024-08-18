namespace TinityScripting.Components.BuiltIn;

public class Transform : Component
{ 
    internal Transform(int instanceId, IntPtr unmanagedPtr) : base(instanceId, unmanagedPtr) { }
    internal override ComponentsBuiltInTypes BuiltInType => ComponentsBuiltInTypes.Transform;
}