using System.Collections.Generic;
using System.Numerics;

namespace LogicGatesSandbox.Engine.Rendering;

public static class SpriteRenderQueue
{
    private static readonly List<RenderConfig> _renderQueue = [];

    public static void AddTextureToRender(
        Texture texture,
        Vector2 position,
        Vector2 size,
        float rotation,
        Vector4 color,
        int layer
    )
    {
        _renderQueue.Add(
            new(texture, position, size, rotation, color, layer)
        );
    }

    public static void RenderSprites()
    {
        _renderQueue.Sort(
            (a, b) => a.Layer.CompareTo(b.Layer)
        );

        foreach (var config in _renderQueue)
        {
            TextureRenderer.RenderTexture(
                config.Texture,
                config.Position,
                config.Size,
                config.Rotation,
                config.Color
            );
        }

        _renderQueue.Clear();
    }
}