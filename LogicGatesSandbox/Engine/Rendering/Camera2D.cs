using System.Numerics;

namespace LogicGatesSandbox.Engine.Rendering;

public static class Camera2D
{
    public static Vector2 Position { get; set; } = Vector2.Zero;

    public static float Zoom { get; set; } = 100f;

    public static float Width { get; private set; }
    public static float Height { get; private set; }

    public static void Initialize(float width, float height)
    {
        Width = width;
        Height = height;
    }

    public static void Resize(float width, float height)
    {
        Width = width;
        Height = height;
    }

    public static Matrix4x4 GetProjectionMatrix()
    {
        float halfWidth = Width / 2.0f / Zoom;
        float halfHeight = Height / 2.0f / Zoom;

        return Matrix4x4.CreateOrthographicOffCenter(
            -halfWidth,
            halfWidth,
            -halfHeight,
            halfHeight,
            -1.0f,
            1.0f
        );
    }

    public static Matrix4x4 GetViewMatrix()
    {
        return Matrix4x4.CreateTranslation(
            -Position.X,
            -Position.Y,
            0.0f
        );
    }

    public static Matrix4x4 GetViewProjectionMatrix()
    {
        Matrix4x4 view = GetViewMatrix();
        Matrix4x4 projection = GetProjectionMatrix();

        return view * projection;
    }
}