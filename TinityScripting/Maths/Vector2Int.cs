using System.Runtime.InteropServices;

namespace TinityScripting.Maths;

[StructLayout(LayoutKind.Sequential)]
public struct Vector2Int
{
    public int x;
    public int y;
    
    public static readonly Vector2Int Right = new Vector2Int() { x = 1, y = 0 };

    public static Vector2Int operator +(Vector2Int a, int b)
    {
        return new Vector2Int() { x = a.x + b, y = a.y + b };
    }
    
    public static Vector2Int operator +(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int() { x = a.x + b.x, y = a.y + b.y };
    }
    
    public static Vector2Int operator *(Vector2Int a, int b)
    {
        return new Vector2Int() { x = a.x * b, y = a.y * b };
    }
}