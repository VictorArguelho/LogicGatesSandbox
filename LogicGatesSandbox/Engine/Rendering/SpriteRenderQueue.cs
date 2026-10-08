using LogicGatesSandbox.Engine.Components;
using LogicGatesSandbox.Engine.Resources;
using System;
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
        if (IsOutsideCamera(
            position,
            size
        ))
            return;

        _renderQueue.Add(
            new(
                texture,
                position,
                size,
                rotation,
                color,
                layer
            )
        );
    }

    public static void RenderSprites()
    {
        if (_renderQueue.Count == 0)
            return;

        _renderQueue.Sort(
            (a, b) =>
            {
                int layerComparison =
                    a.Layer.CompareTo(b.Layer);

                if (layerComparison != 0)
                    return layerComparison;

                return a.Texture.Id.CompareTo(
                    b.Texture.Id
                );
            }
        );

        TextureRenderer.BeginFrame();

        Texture currentTexture =
            _renderQueue[0].Texture;

        int batchStart = 0;

        for (
            int i = 1;
            i < _renderQueue.Count;
            i++
        )
        {
            if (
                _renderQueue[i].Texture.Id ==
                currentTexture.Id
            )
            {
                continue;
            }

            TextureRenderer.RenderBatch(
                _renderQueue,
                batchStart,
                i - batchStart,
                currentTexture
            );

            currentTexture =
                _renderQueue[i].Texture;

            batchStart = i;
        }

        TextureRenderer.RenderBatch(
            _renderQueue,
            batchStart,
            _renderQueue.Count - batchStart,
            currentTexture
        );

        _renderQueue.Clear();
    }

    private static bool IsOutsideCamera(
        Vector2 position,
        Vector2 size
    )
    {
        Vector2 screenPosition =
            Camera.WorldToScreenPosition(
                position
            );

        Vector2 screenCenter =
            Camera.GetScreenCenter();

        float cameraRadius =
            Camera.GetViewportRadius();

        float spriteRadius =
            GetSpriteRadius(size);

        float maximumDistance =
            cameraRadius +
            spriteRadius;

        float distanceSquared =
            Vector2.DistanceSquared(
                screenPosition,
                screenCenter
            );

        return distanceSquared >
               maximumDistance *
               maximumDistance;
    }

    private static float GetSpriteRadius(
        Vector2 size
    )
    {
        return MathF.Max(
            size.X,
            size.Y
        ) / 2.0f;
    }
}