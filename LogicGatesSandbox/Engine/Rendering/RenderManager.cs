using System.Collections.Generic;

namespace LogicGatesSandbox.Engine.Rendering;

public static class RenderManager
{
    private static readonly List<SpriteRenderer> _renderers = [];

    public static void RegisterRenderer(SpriteRenderer renderer) =>
        _renderers.Add(renderer);

    public static void Render()
    {
        foreach (var renderer in _renderers)
            renderer.Render();
    }
}