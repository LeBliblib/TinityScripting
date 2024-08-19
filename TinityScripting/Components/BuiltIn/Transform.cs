using System.Runtime.InteropServices;
using TinityScripting.Maths;

namespace TinityScripting.Components.BuiltIn;

public class Transform : Component
{
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void GetPosition(IntPtr transformPtr, out Vector2 position);

    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void SetPosition(IntPtr transformPtr, Vector2 position);

    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern float GetRotation(IntPtr transformPtr);

    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void SetRotation(IntPtr transformPtr, float rotation);

    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void GetScale(IntPtr transformPtr, out Vector2 scale);

    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void SetScale(IntPtr transformPtr, Vector2 scale);

    internal Transform(int instanceId, IntPtr unmanagedPtr) : base(instanceId, unmanagedPtr)
    {
    }

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

    public float Rotation
    {
        get => GetRotation(UnmanagedPtr);
        set => SetRotation(UnmanagedPtr, value);
    }

    public Vector2 Scale
    {
        get
        {
            GetScale(UnmanagedPtr, out var scale);
            return scale;
        }
        set => SetScale(UnmanagedPtr, value);
    }
}