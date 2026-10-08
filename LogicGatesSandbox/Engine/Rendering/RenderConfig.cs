using LogicGatesSandbox.Engine.Resources;
using System.Numerics;

namespace LogicGatesSandbox.Engine.Rendering;

public readonly struct RenderConfig (
    Texture texture,
    Vector2 position,
    Vector2 size,
    float rotation,
    Vector4 color,
    int layer
)
{
    public readonly Texture Texture => texture;
    public readonly Vector2 Position => position;
    public readonly Vector2 Size => size;
    public readonly float Rotation => rotation;
    public readonly Vector4 Color => color;
    public readonly int Layer => layer;
}