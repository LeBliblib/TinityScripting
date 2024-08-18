using System.Runtime.InteropServices;

namespace TinityScripting;

[StructLayout(LayoutKind.Sequential)]
public struct Vector2
{
    public float x;
    public float y;
    
    public static readonly Vector2 Right = new Vector2() { x = 1, y = 0 };

    public static Vector2 operator +(Vector2 a, float b)
    {
        return new Vector2() { x = a.x + b, y = a.y + b };
    }
    
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2() { x = a.x + b.x, y = a.y + b.y };
    }
    
    public static Vector2 operator *(Vector2 a, float b)
    {
        return new Vector2() { x = a.x * b, y = a.y * b };
    }
}