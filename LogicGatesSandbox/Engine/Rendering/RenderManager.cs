using System.Collections.Generic;

namespace LogicGatesSandbox.Engine.Rendering;

internal static class RenderManager
{
    private static readonly List<SpriteRenderer> _renderers = [];

    internal static void RegisterRenderer(SpriteRenderer renderer) =>
        _renderers.Add(renderer);

    internal static void Render()
    {
        foreach (var renderer in _renderers)
            renderer.Render();
    }
}