using LogicGatesSandbox.Engine.Resources;
using System.Numerics;

namespace LogicGatesSandbox.Engine.Rendering;

internal readonly struct RenderConfig (
    Texture texture,
    Vector2 position,
    Vector2 size,
    float rotation,
    Vector4 color,
    int layer
)
{
    internal readonly Texture Texture => texture;
    internal readonly Vector2 Position => position;
    internal readonly Vector2 Size => size;
    internal readonly float Rotation => rotation;
    internal readonly Vector4 Color => color;
    internal readonly int Layer => layer;
}