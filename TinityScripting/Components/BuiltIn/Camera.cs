using System.Runtime.InteropServices;
using TinityScripting.Maths;

namespace TinityScripting.Components.BuiltIn;

public class Camera : Component
{
    internal override ComponentsBuiltInTypes BuiltInType => ComponentsBuiltInTypes.Camera;
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void SetPriority(IntPtr cameraPtr, int priority);
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern int GetPriority(IntPtr cameraPtr);
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void SetOrthographicSize(IntPtr cameraPtr, float size);
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern float GetOrthographicSize(IntPtr cameraPtr);
    
    [DllImport("Library/BlubEngineCPP_Rider.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void WorldToScreenPoint(IntPtr cameraPtr, Vector2 worldPoint, out Vector2Int screenPoint);
    
    public int Priority
    {
        get => GetPriority(UnmanagedPtr);
        set => SetPriority(UnmanagedPtr, value);
    }
    
    public float OrthographicSize
    {
        get => GetOrthographicSize(UnmanagedPtr);
        set => SetOrthographicSize(UnmanagedPtr, value);
    }
    
    public Vector2Int WorldToScreenPoint(Vector2 worldPoint)
    {
        WorldToScreenPoint(UnmanagedPtr, worldPoint, out var screenPoint);
        return screenPoint;
    }
}