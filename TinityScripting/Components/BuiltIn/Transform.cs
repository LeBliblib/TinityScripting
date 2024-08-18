using System.Runtime.InteropServices;

namespace TinityScripting.Components.BuiltIn;

public class Transform : Component
{ 
    [DllImport("BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void GetPosition(IntPtr transformPtr, out Vector2 position);
    
    [DllImport("BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void SetPosition(IntPtr transformPtr, Vector2 position);
    
    internal Transform(int instanceId, IntPtr unmanagedPtr) : base(instanceId, unmanagedPtr) { }
    internal override ComponentsBuiltInTypes BuiltInType => ComponentsBuiltInTypes.Transform;

    public Vector2 Position
    {
        get
        {
            GetPosition(UnmanagedPtr, out var position);
            return position;
        }
        set => SetPosition(UnmanagedPtr, value);
    }

    public float Rotation { get; set; }
    
    public Vector2 Scale { get; set; }
}