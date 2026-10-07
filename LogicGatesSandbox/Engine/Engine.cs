using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace LogicGatesSandbox.Engine;

public static class Engine
{
    private static GL _gl = null!;

    public static void Main()
    {
        WindowOptions options = WindowOptions.Default;

        options.Title = "Logic Gates Sandbox";
        options.WindowState = WindowState.Fullscreen;
        options.VSync = false;
        options.FramesPerSecond = 0;

        using IWindow window = Window.Create(options);


        window.Load += () =>
        {
            WindowIcon.Set(window, "Assets/Icon.png");

            _gl = window.CreateOpenGL();

            _gl.ClearColor(0.05f, 0.15f, 0.3f, 1.0f);

            _gl.Viewport(window.FramebufferSize);
        };

        window.Update += deltaTime =>
        {
        };

        window.Render += deltaTime =>
        {
            _gl.Clear(ClearBufferMask.ColorBufferBit);
        };

        window.FramebufferResize += size =>
        {
            _gl.Viewport(size);
        };

        window.Run();

        _gl.Dispose();
    }
}