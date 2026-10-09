using LogicGatesSandbox.Engine.Resources;
using System;
using System.Numerics;

namespace LogicGatesSandbox.Engine.Rendering;

public class SpriteRenderer
{
    public Texture Texture { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }
    public Vector4 Color { get; set; }
    public int PixelsPerUnit { get; set; }
    public int Layer { get; set; }

    public SpriteRenderer(
        Texture texture, 
        Vector2 position, 
        Vector2 scale, 
        float rotation,
        Vector4 color, 
        int pixelsPerUnit,
        int layer
    )
    {
        Texture = texture;
        Position = position;
        Scale = scale;
        Rotation = rotation;
        Color = color;
        PixelsPerUnit = pixelsPerUnit;
        Layer = layer;

        RenderManager.RegisterRenderer(this);
    }

    public SpriteRenderer(Texture texture)
    : this(texture, Vector2.Zero, Vector2.One, 0f, Vector4.One, texture.Width, 0)
    {}

    public SpriteRenderer(Texture texture, Vector2 position)
    : this(texture, position, Vector2.One, 0f, Vector4.One, texture.Width, 0)
    { }

    internal void Render() =>
        SpriteRenderQueue.AddTextureToRender(
            Texture,
            Position,
            GetPixelSize(),
            Rotation * MathF.PI / 180.0f,
            Color,
            Layer
        );

    public Vector2 GetPixelSize()
    {
        Vector2 size = new(
            Texture.Width / (float)PixelsPerUnit,
            Texture.Height / (float)PixelsPerUnit
        );

        return size * Scale;
    }
}