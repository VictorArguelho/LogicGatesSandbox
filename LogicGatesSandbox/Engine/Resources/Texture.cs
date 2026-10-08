using System;

namespace LogicGatesSandbox.Engine.Resources;

public readonly struct Texture(uint id, int width, int height) : IDisposable
{
    public readonly uint Id => id;
    public readonly int Width => width;
    public readonly int Height => height;

    public void Dispose() =>
        Engine.GL.DeleteTexture(id);
}